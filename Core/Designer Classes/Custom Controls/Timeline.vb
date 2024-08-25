Imports System.ComponentModel
Imports System.Drawing.Drawing2D


''' <summary>
''' The timeline control is used to display and manipulate layers, layer folders, and keyframes
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class Timeline
    Inherits DesignerControlBase

    Private WithEvents Renamer As New TextBox 'The textbox used when renaming layers

    Private FramesPerPage As Integer = 0
    Private DrawFrame As Boolean = False
    Private ScrollDragOffset As Integer
    Private DraggingScroll As String = ""
    Private DraggingLayer As ArtificeLayerBase = Nothing
    Private DraggingKeyFrame As KeyFrame = Nothing
    Private DropTarget As ArtificeLayerBase = Nothing
    Private ReversableLineY As Integer = 0
    Private LayerListItemMargin As Integer = 5
    Private MaxFrames As Integer = 300
    Private MinKeyFrameDrag As Integer = 0
    Private MaxKeyFrameDrag As Integer = 300
    Private VisibleEllipseSize As Integer = 10
    Private HScrollRect As Rectangle
    Private HScrollVisible As Boolean = False
    Private VScrollRect As Rectangle
    Private VScrollPosition As Integer = 0
    Private VScrollVisible As Boolean = False

#Region "Private Class LayerListItem"
    Public Class LayerListItem
        Public Property Depth As Integer
        Public Property VisibleBounds As Rectangle
        Public Property LockBounds As Rectangle
        Public Property IconBounds As Rectangle
        Public Property TextBounds As Rectangle
        Public Property Layer As ArtificeLayerBase
    End Class
#End Region

#Region "Class TimelineHitTestResults"
    ''' <summary>
    ''' A class containg the results of a hit test performed on the timeline
    ''' </summary>
    ''' <remarks>Created by Autodev</remarks>
    Public Class TimelineHitTestResults
        Private MyArea As TimelineArea = TimelineArea.BlankSpace
        Public ReadOnly Property Area As TimelineArea
            Get
                Return MyArea
            End Get
        End Property

        Private MyFrame As Integer = 0
        Public ReadOnly Property Frame As Integer
            Get
                Return MyFrame
            End Get
        End Property

        Private MyLayer As ArtificeLayerBase = Nothing
        Public ReadOnly Property Layer As ArtificeLayerBase
            Get
                Return MyLayer
            End Get
        End Property

        Private MyListItem As LayerListItem = Nothing
        Public ReadOnly Property ListItem As LayerListItem
            Get
                Return MyListItem
            End Get
        End Property


        Protected Friend Sub New(frame As Integer, listItem As LayerListItem, layer As ArtificeLayerBase, area As TimelineArea)
            MyFrame = frame
            MyListItem = listItem
            MyLayer = layer
            MyArea = area
        End Sub
    End Class
#End Region

#Region "Enum TimelineArea"
    Public Enum TimelineArea
        BlankSpace
        HorizontalScroll
        VerticalScroll
        LayerName
        LockToggle
        LayerIcon
        LayerHeader
        GridHeader
        Grid
        VisibleToggle
    End Enum
#End Region

    ''' <summary>
    ''' Contains the calculated width for the layer name header section (the left part of the timeline)
    ''' </summary>
    ''' <remarks></remarks>
    Private MyLayerHeaderWidth As Integer

    ''' <summary>
    ''' Contains the maximum calculated height of each layer name (essentially the "item height" in our list)
    ''' </summary>
    ''' <remarks></remarks>
    Private MyLayerHeaderHeight As Integer

    ''' <summary>
    ''' The width of each frame in the frame grid
    ''' </summary>
    ''' <remarks></remarks>
    Private MyFrameWidth As Integer = 12

    ''' <summary>
    ''' Our internal cached list of visible layers to facilitate rendering and mouse tracking
    ''' </summary>
    ''' <remarks></remarks>
    Private MyLayerList As New List(Of LayerListItem)

    Private MyFrameStart As Integer = 1
    ''' <summary>
    ''' The starting frame when rendering the grid
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FrameStart As Integer
        Get
            Return MyFrameStart
        End Get
    End Property

#Region "Constructor"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Not DesignMode Then
            'Set this control up with a double-buffer:
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.UserPaint, True)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            SetStyle(ControlStyles.ResizeRedraw, True)

            AllowDrop = True

            'Add a handler to the Singleton Designer's property changed event
            AddHandler Designer.PropertyChanged, AddressOf DesignerPropertyChanged

            Renamer.Visible = False
            Renamer.BorderStyle = BorderStyle.None
            Renamer.BackColor = SystemColors.ButtonFace

            Controls.Add(Renamer)
        End If
    End Sub
#End Region

#Region "CalculateLayout"
    ''' <summary>
    ''' Builds a list of visible layers in the order they appear vertically to facilitate mouse tracking and painting
    ''' </summary>
    ''' <param name="g"></param>
    ''' <param name="layers"></param>
    ''' <param name="depth"></param>
    ''' <remarks></remarks>
    Private Sub CalculateLayout(g As Graphics, layers As ArtificeObject(), ByRef yStart As Integer, Optional parentIndex As Integer = -1, Optional depth As Integer = 0)
        Dim sz As SizeF
        Dim sf As New StringFormat(StringFormatFlags.NoClip Or StringFormatFlags.FitBlackBox Or StringFormatFlags.NoWrap)


        For lIndex As Integer = layers.Count - 1 To 0 Step -1
            'For Each l As ArtificeLayerBase In layers
            Dim l As ArtificeLayerBase = layers(lIndex)
            sz = g.MeasureString(l.Name, Font, 200, sf)

            'Dim iconsWidth As Integer = LayerListItemMargin + VisibleEllipseSize + 2 + My.Resources.Locked.Width + 2 + l.Icon.Width + 2
            Dim xStart As Single = LayerListItemMargin + depth * 15

            Dim lli As New LayerListItem

            lli.Layer = l
            lli.Depth = depth
            lli.VisibleBounds = New Rectangle(xStart, yStart + (MyLayerHeaderHeight - VisibleEllipseSize) \ 2, VisibleEllipseSize, VisibleEllipseSize)

            xStart += VisibleEllipseSize + 2

            lli.LockBounds = New Rectangle(xStart, yStart + (MyLayerHeaderHeight - My.Resources.Locked.Height) \ 2, My.Resources.Locked.Width, My.Resources.Locked.Height)

            xStart += lli.LockBounds.Width + 2

            lli.IconBounds = New Rectangle(xStart, yStart + (MyLayerHeaderHeight - l.Icon.Height) \ 2, l.Icon.Width, l.Icon.Height)

            xStart += lli.IconBounds.Width + 2

            lli.TextBounds = New Rectangle(xStart, yStart, Math.Round(sz.Width, MidpointRounding.AwayFromZero) + 10, MyLayerHeaderHeight)

            xStart += sz.Width + LayerListItemMargin

            MyLayerHeaderWidth = Math.Max(MyLayerHeaderWidth, xStart)

            MyLayerHeaderHeight = Math.Max(MyLayerHeaderHeight, sz.Height + 8)

            'We build our list backwards to show the "topmost" layers first:
            'MyLayerList.Insert(parentIndex + 1, lli)
            MyLayerList.Add(lli)

            yStart += MyLayerHeaderHeight

            If l.GetType() Is GetType(LayerGroup) Then
                If CType(l, LayerGroup).Expanded Then CalculateLayout(g, l.Children, yStart, MyLayerList.IndexOf(lli), depth + 1)
            End If
        Next
    End Sub
#End Region

#Region "CalculateScrollers"
    ''' <summary>
    ''' Determines the visibility, size, and position of the scrollbars
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateScrollers()
        'Determine HScroll Visibility:
        ' (if my starting frame plus the number of frames visible is less than the maximum amount of frames
        '  I can scroll to, then show the HScroll):
        HScrollVisible = True

        If HScrollVisible Then
            Dim framesToScroll = MaxFrames - FramesPerPage
            Dim sbSize As Integer = ClientRectangle.Width - MyLayerHeaderWidth - framesToScroll

            HScrollRect = New Rectangle(MyLayerHeaderWidth + (FrameStart - 1), ClientRectangle.Height - 16, Math.Max(20, sbSize), 14)
        End If

        Dim lastY As Integer = MyLayerHeaderHeight

        If VScrollVisible Then
            lastY = VScrollRect.Top
        End If

        VScrollVisible = ClientRectangle.Height - MyLayerHeaderHeight - 16 < MyLayerList.Count * MyLayerHeaderHeight
        If VScrollVisible Then
            Dim layersPerScreen As Integer = (ClientRectangle.Height - MyLayerHeaderHeight) / MyLayerHeaderHeight - 1
            Dim layersToScroll As Integer = MyLayerList.Count - layersPerScreen
            Dim sbSize As Integer = ClientRectangle.Height - MyLayerHeaderHeight - layersToScroll - 16
            VScrollRect = New Rectangle(ClientRectangle.Width - 16, Math.Max(MyLayerHeaderHeight, Math.Min(lastY, ClientRectangle.Height - 16 - VScrollRect.Height)), 14, sbSize)
        End If
    End Sub
#End Region

#Region "DesignerPropertyChanged"
    ''' <summary>
    ''' Responds to property changes by the designer or any ArtificeObject in the current project
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DesignerPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        If Designer.Exporting Then Return

        If (sender Is Designer.SelectedScene AndAlso e.PropertyName = "Children") OrElse sender.GetType().IsSubclassOf(GetType(ArtificeLayerBase)) Then
            If e.PropertyName = "Name" OrElse e.PropertyName = "Expanded" OrElse e.PropertyName = "Children" Then RecalculateLayout()
            Refresh()
            Return
        End If

        Select Case e.PropertyName
            Case "SelectedScene"
                RecalculateLayout()
                Refresh()
            Case "Frame"
                If Designer.SelectedScene.Frame < FrameStart _
                    OrElse Designer.SelectedScene.Frame > FrameStart + FramesPerPage Then
                    MyFrameStart = Designer.SelectedScene.Frame

                    If HScrollVisible Then
                        HScrollRect.X = MyLayerHeaderWidth + (FrameStart - 1) '* MyFrameWidth
                    End If
                End If

                Refresh()
            Case "SelectedLayer", "SelectedKeyFrame"
                Refresh()
        End Select
    End Sub
#End Region

#Region "DrawLayer"
    ''' <summary>
    ''' Paints the specified layer at the vertical location
    ''' </summary>
    ''' <param name="yStart"></param>
    ''' <param name="li"></param>
    ''' <param name="sf"></param>
    ''' <param name="g"></param>
    ''' <remarks></remarks>
    Private Sub DrawLayer(yStart As Integer, li As LayerListItem, sf As StringFormat, g As Graphics)
        yStart = li.TextBounds.Top

        'Are we the selected layer?
        If li.Layer Is Designer.SelectedLayer OrElse Designer.SelectedObject Is Me Then
            Dim highlightRect As New RectangleF(0, yStart, ClientRectangle.Width, MyLayerHeaderHeight)
            Dim br As New LinearGradientBrush(highlightRect, Color.FromArgb(128, 230, 230, 255), Color.FromArgb(128, 180, 180, 230), 85.0F)
            Dim p As New Pen(Color.FromArgb(128, 0, 0, 164))
            g.FillRectangle(br, highlightRect)
            g.DrawRectangle(p, highlightRect.X, highlightRect.Y, highlightRect.Width(), highlightRect.Height())
            p.Dispose()
            br.Dispose()
        End If


        'Draw visible idicator ellipse 
        g.FillEllipse(IIf(li.Layer.Visible, Brushes.LightGreen, Brushes.DarkRed), li.VisibleBounds)
        g.DrawEllipse(IIf(li.Layer.Visible, Pens.DarkGreen, Pens.DarkRed), li.VisibleBounds)
        'g.DrawImage(IIf(li.Layer.Visible, My.Resources.Visible, My.Resources.Hidden), xStart, yStart + (MyLayerHeaderHeight - My.Resources.Visible.Height) \ 2)

        'Draw locked icon:
        g.DrawImage(IIf(li.Layer.Locked, My.Resources.Locked, My.Resources.Unlocked), li.LockBounds.Location)

        'Draw our icon:
        g.DrawImage(li.Layer.Icon, li.IconBounds.Location)

        'Write out the name:
        g.DrawString(li.Layer.Name, Font, IIf(li.Layer.Visible, Brushes.Black, Brushes.DarkGray), li.TextBounds, sf)



        'If we're not a group, draw out our keyframes:
        If Not li.Layer.Is(Of LayerGroup)() Then
            Dim fx As Integer = MyLayerHeaderWidth
            Dim frameCount As Integer = FrameStart

            While fx < ClientRectangle.Width
                'Remember the "GetKeyFrame" function returns the keyframe for this frame,
                'or the nearest earlier keyframe:
                Dim kf As KeyFrame = li.Layer.As(Of Layer)().GetKeyFrame(frameCount)

                If kf IsNot Nothing Then
                    'Are we in the actual frame that this keyframe is defined in?
                    If kf.Frame = frameCount Then
                        'Draw the solid box that marks a keyframe:
                        Dim br As SolidBrush = IIf(Designer.SelectedContainer Is kf OrElse (Designer.SelectedContainer IsNot Nothing AndAlso Designer.SelectedContainer.GetAncestor(Of KeyFrame)() Is Me), Brushes.Orange, Brushes.DarkGreen)
                        Dim p As Pen = IIf(Designer.SelectedContainer Is kf OrElse (Designer.SelectedContainer IsNot Nothing AndAlso Designer.SelectedContainer.GetAncestor(Of KeyFrame)() Is Me), Pens.Orange, Pens.DarkGreen)

                        g.FillRectangle(br, New Rectangle(fx + 2, yStart + 2, MyFrameWidth - 4, MyLayerHeaderHeight - 4))
                    ElseIf kf.Next IsNot Nothing Then
                        'If we're between keyframes, draw a line between them
                        '(to indicate that tweening is happening).
                        'The line will appear half-way down the height of the frame grid box
                        g.DrawLine(Pens.DarkGreen, CSng(fx), CSng(yStart + (MyLayerHeaderHeight / 2)), CSng(fx + 15), CSng(yStart + (MyLayerHeaderHeight / 2)))
                    End If
                End If

                fx += MyFrameWidth
                frameCount += 1
            End While
        End If
    End Sub
#End Region

#Region "HitTest"
    ''' <summary>
    ''' Returns information about the timeline at the given coordinates
    ''' </summary>
    ''' <param name="location">The location to check at</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HitTest(location As Point) As TimelineHitTestResults
        Return HitTest(location.X, location.Y)
    End Function

    ''' <summary>
    ''' Returns information about the timeline at the given coordinates
    ''' </summary>
    ''' <param name="x">The x-coordinate of the location to check at</param>
    ''' <param name="y">The y-coordinate of the location to check at</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HitTest(x As Integer, y As Integer) As TimelineHitTestResults
        If HScrollVisible AndAlso HScrollRect.Contains(x, y) Then
            Return New TimelineHitTestResults(0, Nothing, Nothing, TimelineArea.HorizontalScroll)
        End If

        If VScrollVisible AndAlso VScrollRect.Contains(x, y) Then
            Return New TimelineHitTestResults(0, Nothing, Nothing, TimelineArea.VerticalScroll)
        End If

        Dim originalY As Integer = y

        If VScrollVisible Then
            y += (VScrollRect.Top - MyLayerHeaderHeight) * MyLayerHeaderHeight
        End If

        Dim frame As Integer = 0
        Dim layer As ArtificeLayerBase = Nothing
        Dim area As TimelineArea = TimelineArea.BlankSpace
        Dim layerIndex As Integer = (y - MyLayerHeaderHeight) \ MyLayerHeaderHeight
        Dim listItem As LayerListItem = Nothing

        'Are we on the left side of the control (where the layer headers are)?
        If x < MyLayerHeaderWidth Then
            area = TimelineArea.LayerHeader

            'Are we actually in the space where the layers are?
            If layerIndex >= 0 AndAlso layerIndex < MyLayerList.Count() Then
                listItem = MyLayerList(layerIndex)
                layer = listItem.Layer

                If MyLayerList(layerIndex).VisibleBounds.Contains(x, y) Then area = TimelineArea.VisibleToggle

                If MyLayerList(layerIndex).LockBounds.Contains(x, y) Then area = TimelineArea.LockToggle

                If MyLayerList(layerIndex).IconBounds.Contains(x, y) Then area = TimelineArea.LayerIcon

                If MyLayerList(layerIndex).TextBounds.Contains(x, y) Then area = TimelineArea.LayerName
            End If
        ElseIf originalY < MyLayerHeaderHeight Then
            area = TimelineArea.GridHeader

            'What frame are we clicking on?
            frame = ((x - MyLayerHeaderWidth) \ MyFrameWidth) + FrameStart
        Else
            area = TimelineArea.Grid

            'What frame are we clicking on?
            frame = ((x - MyLayerHeaderWidth) \ MyFrameWidth) + FrameStart

            'Are we actually in the space where the layers are?
            If layerIndex >= 0 AndAlso layerIndex < MyLayerList.Count() Then
                listItem = MyLayerList(layerIndex)
                layer = listItem.Layer
            End If
        End If

        Return New TimelineHitTestResults(frame, listItem, layer, area)
    End Function
#End Region

#Region "OnMouseDoubleClick"
    Protected Overrides Sub OnMouseDoubleClick(e As MouseEventArgs)
        Dim thr As TimelineHitTestResults = HitTest(e.Location)

        If thr.Area = TimelineArea.LayerIcon AndAlso thr.Layer.GetType() Is GetType(LayerGroup) Then
            'If we've double-clicked on a folder icon then toggle the folder's expanded property:
            thr.Layer.As(Of LayerGroup)().Expanded = Not thr.Layer.As(Of LayerGroup)().Expanded
        ElseIf thr.Area = TimelineArea.LayerName Then
            'If we've double-clicked on a layer name, start the rename process:
            StartLayerRename(thr.Layer)
        End If
    End Sub
#End Region

#Region "OnMouseDown"
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        'Perform a hit test at the current coordinates:
        Dim thr As TimelineHitTestResults = HitTest(e.Location)

        'If we were renaming a layer, hide the textbox and commit the changes:
        StopLayerRename(False)

        If Designer.SelectedScene Is Nothing Then Return

        'If we've clicked on a frame, make it the currently-selected frame for the currently-selected scene:
        If thr.Frame > 0 Then
            Designer.SelectedScene.Frame = thr.Frame
        End If

        'Respond to the appropriate area that was hit on:
        If thr.Layer IsNot Nothing Then
            Select Case thr.Area
                Case TimelineArea.LayerHeader, TimelineArea.LayerName, TimelineArea.LayerIcon
                    Designer.SelectedLayer = thr.Layer

                    Designer.SetSelection(thr.Layer)

                Case TimelineArea.LockToggle
                    If e.Button = Windows.Forms.MouseButtons.Left Then thr.Layer.Locked = Not thr.Layer.Locked

                Case TimelineArea.VisibleToggle
                    If e.Button = Windows.Forms.MouseButtons.Left Then thr.Layer.Visible = Not thr.Layer.Visible

                Case TimelineArea.Grid
                    'Are we dragging a keyframe?
                    If thr.Layer.Is(Of Layer)() Then
                        Dim kf As KeyFrame = thr.Layer.As(Of Layer).GetKeyFrame(thr.Frame)
                        If kf IsNot Nothing AndAlso kf.Frame = thr.Frame Then
                            'Constrain this keyframe to be between the previous keyframe and the next keyframe:
                            MinKeyFrameDrag = 1

                            If kf.Previous IsNot Nothing Then
                                MinKeyFrameDrag = kf.Previous.As(Of KeyFrame).Frame + 1
                            End If

                            MaxKeyFrameDrag = MaxFrames

                            If kf.Next IsNot Nothing Then
                                MaxKeyFrameDrag = kf.Next.As(Of KeyFrame).Frame - 1
                            End If

                            DraggingKeyFrame = kf
                        End If
                    End If

                    Designer.SelectedLayer = thr.Layer
            End Select
        ElseIf thr.Area = TimelineArea.HorizontalScroll Then
            ScrollDragOffset = e.Location.X - HScrollRect.X
            DraggingScroll = "H"
        ElseIf thr.Area = TimelineArea.VerticalScroll Then
            ScrollDragOffset = e.Location.Y - VScrollRect.Y
            DraggingScroll = "V"
        End If

    End Sub
#End Region

#Region "OnMouseMove"
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        Dim thr As TimelineHitTestResults = HitTest(e.Location)

        If DraggingScroll = "H" Then
            HScrollRect.X = Math.Max(MyLayerHeaderWidth, Math.Min(e.Location.X - ScrollDragOffset, ClientRectangle.Right - 16 - HScrollRect.Width))
            MyFrameStart = (HScrollRect.X - MyLayerHeaderWidth) + 1
            Refresh()
        ElseIf DraggingScroll = "V" Then
            VScrollRect.Y = Math.Max(MyLayerHeaderHeight, Math.Min(e.Location.Y - ScrollDragOffset, ClientRectangle.Height - 16 - VScrollRect.Height))
            RealignRenamer()
            Refresh()
        ElseIf DraggingKeyFrame IsNot Nothing Then
            Dim oldFrame As Integer = DraggingKeyFrame.Frame

            DraggingKeyFrame.Frame = Math.Max(MinKeyFrameDrag, Math.Min(thr.Frame, MaxKeyFrameDrag))

            If oldFrame <> DraggingKeyFrame.Frame Then
                Refresh()
            End If

        ElseIf DraggingLayer IsNot Nothing Then


            If thr.Layer IsNot Nothing AndAlso thr.Layer IsNot DraggingLayer Then
                Dim y As Integer = MyLayerHeaderHeight

                Dim itemY As Integer = thr.ListItem.TextBounds.Top - IIf(VScrollVisible, (VScrollRect.Top - MyLayerHeaderHeight) * MyLayerHeaderHeight, 0) ' MyLayerList.IndexOf(thr.ListItem) * MyLayerHeaderHeight + MyLayerHeaderHeight
                Dim itemTop As Integer = itemY + MyLayerHeaderHeight


                If e.Y < itemY + 3 Then
                    y = itemY
                    DropTarget = Nothing
                ElseIf e.Y > itemTop - 3 Then
                    y = itemTop
                    DropTarget = Nothing
                Else
                    If thr.Layer.Is(Of LayerGroup)() Then
                        y = itemY
                        DropTarget = thr.Layer
                    Else
                        DropTarget = Nothing
                    End If
                End If

                If y <> ReversableLineY Then
                    If ReversableLineY > 0 Then
                        If DrawFrame Then
                            ControlPaint.DrawReversibleFrame(New Rectangle(PointToScreen(New Point(0, ReversableLineY)), New Size(MyLayerHeaderWidth, MyLayerHeaderHeight)), Color.Black, FrameStyle.Thick)
                            DrawFrame = False
                        Else
                            ControlPaint.DrawReversibleLine(PointToScreen(New Point(0, ReversableLineY)), PointToScreen(New Point(MyLayerHeaderWidth, ReversableLineY)), Color.Black)
                        End If
                        ReversableLineY = 0
                    End If

                    If DropTarget IsNot Nothing Then
                        ReversableLineY = y
                        ControlPaint.DrawReversibleFrame(New Rectangle(PointToScreen(New Point(0, ReversableLineY)), New Size(MyLayerHeaderWidth, MyLayerHeaderHeight)), Color.Black, FrameStyle.Thick)
                        DrawFrame = True
                    Else
                        ReversableLineY = y
                        ControlPaint.DrawReversibleLine(PointToScreen(New Point(0, ReversableLineY)), PointToScreen(New Point(MyLayerHeaderWidth, ReversableLineY)), Color.Black)
                    End If
                End If
            End If
        Else
            If Designer.SelectedScene Is Nothing Then Return

            If e.Button And MouseButtons.Left = MouseButtons.Left AndAlso (thr.Area = TimelineArea.Grid OrElse thr.Area = TimelineArea.GridHeader) Then
                Designer.SelectedScene.Frame = thr.Frame
            ElseIf (e.Button And Windows.Forms.MouseButtons.Left) = Windows.Forms.MouseButtons.Left AndAlso (thr.Area = TimelineArea.LayerHeader OrElse thr.Area = TimelineArea.LayerName OrElse thr.Area = TimelineArea.LayerIcon) AndAlso thr.Layer IsNot Nothing Then
                DraggingLayer = thr.Layer
                Capture = True
            End If
        End If
    End Sub
#End Region

#Region "OnMouseUp"
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        'Are we dragging a scroller?
        If DraggingScroll <> "" Then
            DraggingScroll = ""
            'Are we dragging a layer?
        ElseIf DraggingKeyFrame IsNot Nothing Then
            DraggingKeyFrame = Nothing
        ElseIf DraggingLayer IsNot Nothing Then
            Dim thr As TimelineHitTestResults = HitTest(e.Location)

            'Do the drop
            If thr.Layer IsNot Nothing AndAlso thr.Layer IsNot DraggingLayer Then
                Dim y As Integer = MyLayerHeaderHeight

                Dim itemY As Integer = MyLayerList.IndexOf(thr.ListItem) * MyLayerHeaderHeight + MyLayerHeaderHeight
                Dim itemTop As Integer = itemY + MyLayerHeaderHeight


                If e.Y < itemY + 3 Then
                    thr.Layer.AddAfter(DraggingLayer)
                ElseIf e.Y > itemTop - 3 Then
                    thr.Layer.AddBefore(DraggingLayer)
                ElseIf DropTarget IsNot Nothing Then
                    DropTarget.AddChild(DraggingLayer)
                    ControlPaint.DrawReversibleFrame(New Rectangle(PointToScreen(New Point(0, ReversableLineY)), New Size(MyLayerHeaderWidth, MyLayerHeaderHeight)), Color.Black, FrameStyle.Thick)
                    RecalculateLayout()
                    Refresh()
                End If

                ControlPaint.DrawReversibleLine(PointToScreen(New Point(0, ReversableLineY)), PointToScreen(New Point(MyLayerHeaderWidth, ReversableLineY)), Color.Black)
                ReversableLineY = -1
            End If

            DraggingLayer = Nothing
            Capture = False
            Cursor = Cursors.Default
        End If
    End Sub
#End Region

#Region "OnPaint"
    ''' <summary>
    ''' Handles painting of our Timeline control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        'We like text centered (for the most part), so we'll create an instance of StringFormat to reuse everywhere:
        Dim sf As New StringFormat(StringFormatFlags.NoWrap Or StringFormatFlags.FitBlackBox Or StringFormatFlags.NoClip)

        e.Graphics.CompositingQuality = CompositingQuality.HighQuality
        e.Graphics.InterpolationMode = InterpolationMode.High

        sf.LineAlignment = StringAlignment.Center
        sf.Alignment = StringAlignment.Near

        If Designer.Project Is Nothing Then
            e.Graphics.DrawString("No project selected. Choose ""New Project"" or ""Open Project"" from the file menu", Font, Brushes.Black, ClientRectangle, sf)
            Return
        End If

        'Draw the shaded backgrounds for the header areas, and lines at their edges:
        e.Graphics.FillRectangle(SystemBrushes.ButtonFace, New Rectangle(0, 0, MyLayerHeaderWidth, ClientRectangle.Height))
        e.Graphics.FillRectangle(SystemBrushes.ButtonFace, New Rectangle(0, 0, ClientRectangle.Width, MyLayerHeaderHeight))
        e.Graphics.DrawLine(SystemPens.ButtonShadow, MyLayerHeaderWidth, MyLayerHeaderHeight, MyLayerHeaderWidth, ClientRectangle.Height)
        e.Graphics.DrawLine(SystemPens.ButtonShadow, 0, MyLayerHeaderHeight, ClientRectangle.Width, MyLayerHeaderHeight)


        If Designer.SelectedScene IsNot Nothing Then
            Dim yStart As Integer = MyLayerHeaderHeight
            Dim lliIndex As Integer = 0 + IIf(VScrollVisible, VScrollRect.Top - MyLayerHeaderHeight, 0)

            Dim gs = e.Graphics.Save()
            e.Graphics.TranslateTransform(0, -MyLayerHeaderHeight * lliIndex)

            'Draw each layer in our cached list.
            'We built this list in CalculateLayout and it only has the visible layers in it (making this fast)
            While yStart < ClientRectangle.Bottom AndAlso lliIndex < MyLayerList.Count
                DrawLayer(yStart, MyLayerList(lliIndex), sf, e.Graphics)
                lliIndex += 1
                yStart += MyLayerHeaderHeight
            End While

            e.Graphics.Restore(gs)
        End If

        sf.LineAlignment = StringAlignment.Far

        'Draw Grid:
        Dim fx As Integer = MyLayerHeaderWidth
        Dim frameCount As Integer = FrameStart

        While fx < ClientRectangle.Width
            'Draw the vertical line for this frame column:
            e.Graphics.DrawLine(SystemPens.ButtonShadow, fx, MyLayerHeaderHeight - 2, fx, ClientRectangle.Height)

            'Only show the frame number for every 1/2 of the FramesPerSecond value of this project:
            If frameCount = 1 OrElse Designer.Project.FramesPerSecond = 1 OrElse frameCount Mod (Designer.Project.FramesPerSecond \ 2) = 0 Then
                Dim sz As SizeF = e.Graphics.MeasureString(frameCount, Font)
                e.Graphics.DrawString(frameCount, Font, Brushes.Black, New RectangleF(fx + 7.5 - (sz.Width() / 2), 0, sz.Width, MyLayerHeaderHeight - 2), sf)
            End If

            'Draw the column hilight for the selected frame (if there is one):
            If Designer.SelectedScene IsNot Nothing AndAlso Designer.SelectedScene.Frame = frameCount Then
                Dim highlightRect As New RectangleF(fx, 0, MyFrameWidth, ClientRectangle.Height)
                Dim br As New LinearGradientBrush(highlightRect, Color.FromArgb(128, 230, 230, 255), Color.FromArgb(128, 180, 180, 230), 85.0F)
                Dim p As New Pen(Color.FromArgb(128, 0, 0, 164))
                e.Graphics.FillRectangle(br, highlightRect)
                e.Graphics.DrawRectangle(p, highlightRect.X, highlightRect.Y, highlightRect.Width(), highlightRect.Height())
                p.Dispose()
                br.Dispose()
            End If

            fx += MyFrameWidth
            frameCount += 1
        End While

        'Give our timeline control a border:
        ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Bump)

        'Paint our scroll boxes:
        If HScrollVisible Then
            e.Graphics.FillRectangle(SystemBrushes.ControlDark, HScrollRect)
        End If

        If VScrollVisible Then
            e.Graphics.FillRectangle(SystemBrushes.ControlDark, VScrollRect)
        End If
    End Sub
#End Region

#Region "OnPaintBackground"
    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        pevent.Graphics.FillRectangle(Brushes.White, pevent.ClipRectangle)
    End Sub
#End Region

#Region "OnSizeChanged"
    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        RecalculateLayout()

        MyBase.OnSizeChanged(e)
    End Sub
#End Region

#Region "RealignRenamer"
    ''' <summary>
    ''' Ensures that if the Renamer text box is visible, it's correctly aligned to the layer being renamed
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RealignRenamer()
        If Renamer.Visible AndAlso Renamer.Tag IsNot Nothing Then
            For Each lli As LayerListItem In MyLayerList
                If lli.Layer Is Renamer.Tag Then
                    Renamer.Top = lli.TextBounds.Top + ((MyLayerHeaderHeight - Renamer.Height) / 2) - IIf(VScrollVisible, (VScrollRect.Top - MyLayerHeaderHeight) * MyLayerHeaderHeight, 0)
                    Return
                End If
            Next
        End If
    End Sub
#End Region

#Region "RecalculateLayout"
    ''' <summary>
    ''' Determines the sizes of the various sections of the TimeLine
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RecalculateLayout()
        MaxFrames = 300

        MyLayerHeaderWidth = 80
        MyLayerHeaderHeight = 22

        MyLayerList.Clear()

        If Designer.SelectedScene IsNot Nothing Then
            MaxFrames = Designer.SelectedScene.TotalFrames + 300

            Dim g As Graphics = CreateGraphics()
            Dim yStart As Integer = MyLayerHeaderHeight

            CalculateLayout(g, Designer.SelectedScene.Children, yStart)

            g.Dispose()
        End If

        FramesPerPage = (ClientRectangle.Width - MyLayerHeaderWidth - 16) / MyFrameWidth

        CalculateScrollers()
    End Sub
#End Region

#Region "Renamer_KeyDown"
    Private Sub Renamer_KeyDown(sender As Object, e As KeyEventArgs) Handles Renamer.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                'If the user hits Escape while editing a layer name, don't commit the changes:
                StopLayerRename(True)
                e.Handled = True

            Case Keys.Enter
                StopLayerRename(False)
                e.Handled = True
        End Select
    End Sub
#End Region

#Region "Renamer_LostFocus"
    Private Sub Renamer_LostFocus(sender As Object, e As EventArgs) Handles Renamer.LostFocus
        'If the users tabs out (or clicks somewhere outside of Renamer, commit the changes and hide Renamer:
        StopLayerRename(False)
    End Sub
#End Region

#Region "StartLayerRename"
    ''' <summary>
    ''' Shows the Renamer control for the currently-selected layer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StartLayerRename()
        If Designer.SelectedLayer Is Nothing Then Return

        StartLayerRename(Designer.SelectedLayer)
    End Sub

    ''' <summary>
    ''' Shows the Renamer control for the given layer
    ''' </summary>
    ''' <param name="l"></param>
    ''' <remarks></remarks>
    Public Sub StartLayerRename(l As ArtificeLayerBase)
        For Each lli As LayerListItem In MyLayerList
            If lli.Layer Is l Then
                Renamer.Top = lli.TextBounds.Top + ((MyLayerHeaderHeight - Renamer.Height) / 2) - IIf(VScrollVisible, (VScrollRect.Top - MyLayerHeaderHeight) * MyLayerHeaderHeight, 0)
                Renamer.Left = lli.TextBounds.Left
                Renamer.Width = lli.TextBounds.Width
                Renamer.Text = l.Name
                Renamer.Tag = l
                Renamer.Show()
                Renamer.SelectAll()
                Renamer.Focus()
                Exit For
            End If
        Next
    End Sub
#End Region

#Region "StopLayerRename"
    ''' <summary>
    ''' Hides the Renamer textbox
    ''' </summary>
    ''' <param name="cancel">False if you want the changes to the layer name committed, True otherwise</param>
    ''' <remarks></remarks>
    Public Sub StopLayerRename(cancel As Boolean)
        If Renamer.Visible Then
            If Not cancel AndAlso Renamer.Tag IsNot Nothing Then CType(Renamer.Tag, ArtificeLayerBase).Name = Renamer.Text.Trim()

            Renamer.Tag = Nothing

            Renamer.Visible = False
        End If
    End Sub
#End Region

#Region "Timeline_MouseWheel"
    Private Sub Timeline_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        'Adjust the vertical scroll on mouse wheel, if it's visible:
        If VScrollVisible Then
            Dim newValue As Integer = VScrollRect.Y - (e.Delta / 120)

            VScrollRect.Y = Math.Max(MyLayerHeaderHeight, Math.Min(newValue, ClientRectangle.Height - 16 - VScrollRect.Height))

            'If renamer was visible, it'll need to be realigned to its layer (since it moved):
            RealignRenamer()

            Refresh()
        End If
    End Sub
#End Region


End Class

Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Public Class SceneSurface
    Inherits DesignerControlBase

    Private PanDragOffset As Point ' The offset from center that the scene has been panned while zoomed in

    Private StageBounds As New Rectangle ' The size of the stage the scene is being drawn on

    'Members related to the sizing grips and resizing operations:
    Private SizingBoxes(8) As RectangleF
    Private SizingOriginalValues As New Dictionary(Of ArtificeTransformable, PointF)
    Private SizingOriginalPositions As New Dictionary(Of ArtificeTransformable, PointF)
    Private SizingBoxDrag As Integer = -1
    Private SizingDragStart As New Point
    Private SizingBounds As RectangleF

    'Members related to object rotation:
    Private OriginalRotation As Single
    Private RotationObjectOrigin As PointF
    Private RotationStartPoint As PointF

    Public Event AutoFitChanged As EventHandler
    Public Event PanOffsetChanged As EventHandler
    Public Event ZoomChanged As EventHandler

#Region "Properties"
    Private MyAutoFit As Boolean = True
    Public Property AutoFit As Boolean
        Get
            Return MyAutoFit
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = value <> MyAutoFit

            MyAutoFit = value

            If changed Then
                CalculateStageBounds()
                Refresh()

                RaiseEvent AutoFitChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    Private MyPanOffset As Point
    Public Property PanOffset As Point
        Get
            Return MyPanOffset
        End Get
        Set(value As Point)
            'Constrain the offset. If we're in AutoFit mode, then there's no panning; always keep it 0, 0:
            If AutoFit Then
                value = Point.Empty
            Else
                'Constrain panning to the edges of the stage:
                Dim newX As Integer = Math.Max(Math.Min(value.X, Math.Abs(ClientRectangle.Width - StageBounds.Width * Zoom) / 2), Math.Abs(ClientRectangle.Width - StageBounds.Width * Zoom) / -2)
                Dim newY As Integer = Math.Max(Math.Min(value.Y, Math.Abs(ClientRectangle.Height - StageBounds.Height * Zoom) / 2), Math.Abs(ClientRectangle.Height - StageBounds.Height * Zoom) / -2)

                'If the current stage height (at the zoom scale) is smaller than the client height, center us vertically:
                If StageBounds.Height * Zoom <= ClientRectangle.Height Then newY = 0

                'If the current stage width (at the zoom scale) is smaller than the client width, center us horizontally:
                If StageBounds.Width * Zoom <= ClientRectangle.Width Then newX = 0

                'Reset value to the new constraint points:
                value = New Point(newX, newY)
            End If

            Dim changed As Boolean = Not MyPanOffset.Equals(value)

            MyPanOffset = value

            If changed Then
                Refresh()
                RaiseEvent PanOffsetChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    Private MyZoom As Single = 1.0
    Public Property Zoom As Single
        Get
            Return MyZoom
        End Get
        Set(value As Single)
            value = Math.Max(0.1, Math.Min(8.0, value))

            Dim changed As Boolean = MyZoom <> value

            MyZoom = value

            If changed Then
                MyAutoFit = False
                CalculateStageBounds()
                Refresh()

                RaiseEvent ZoomChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Not DesignMode Then
            Dim ms As New IO.MemoryStream(My.Resources.Rotate)
    
            SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            SetStyle(ControlStyles.UserPaint, True)
            SetStyle(ControlStyles.ResizeRedraw, True)
            SetStyle(ControlStyles.UserMouse, True)

            AddHandler Designer.PropertyChanged, AddressOf DesignerPropertyChanged
        End If
    End Sub
#End Region

#Region "AfterRender"
    ''' <summary>
    ''' Performs any clean-up actions to the RenderContext after this rendering is complete
    ''' </summary>
    ''' <param name="rc"></param>
    ''' <remarks></remarks>
    Public Sub AfterRender(rc As RenderContext)
        'Pop our graphics state and restore the previous:
        rc.PopGraphicsState()
    End Sub
#End Region

#Region "BeforeRender"
    ''' <summary>
    ''' Performs an initalization and transformation to the RenderContext before rendering occurrs
    ''' </summary>
    ''' <param name="rc"></param>
    ''' <remarks></remarks>
    Public Sub BeforeRender(rc As RenderContext)
        'Push our graphics state:
        rc.PushGraphicsState()

        'Translate the origin of this scene to the center of our rendering surface (including our panning offset when the zoomed surface is bigger than the control):
        rc.Graphics.TranslateTransform((ClientRectangle.Width / 2) + PanOffset.X, (ClientRectangle.Height / 2) + PanOffset.Y)
        rc.Graphics.ScaleTransform(MyZoom, MyZoom)
    End Sub
#End Region

#Region "CalculateStageBounds"
    ''' <summary>
    ''' Calculates the bounds of the stage and the zoom level when AutoFit is set to True.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculateStageBounds()
        If Designer.Project Is Nothing Then
            Return
        End If

        StageBounds = New Rectangle(-Designer.Project.StageWidth / 2, -Designer.Project.StageHeight / 2, Designer.Project.StageWidth, Designer.Project.StageHeight)

        If AutoFit Then
            'Reset pan to 0:
            MyPanOffset = Point.Empty

            If ClientRectangle.Width > ClientRectangle.Height Then
                MyZoom = ClientRectangle.Width / StageBounds.Width

                If StageBounds.Height * MyZoom > ClientRectangle.Height Then
                    MyZoom = ClientRectangle.Height / StageBounds.Height
                End If
            Else
                MyZoom = ClientRectangle.Height / StageBounds.Height

                If StageBounds.Width * MyZoom > ClientRectangle.Width Then
                    MyZoom = ClientRectangle.Width / StageBounds.Width
                End If
            End If
        End If
    End Sub
#End Region

#Region "DesignerPropertyChanged"
    ''' <summary>
    ''' Handles PropertyChanged events from the Designer singleton
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DesignerPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        Select Case e.PropertyName
            Case "Project"
                'If the project has changed, we need to recalculate the bounds:
                CalculateStageBounds()
            Case "SelectedTool"                
                Cursor = Designer.SelectedTool.Cursor
            Case "SelectedKeyFrame", "SelectedLayer"
                'Do Nothing
            Case Else
                'If any other property has changed and we're not animating or exporting then render the current frame:
                'NOTE: this could probably be optimized to prevent re-rendering when non-visual properties (like Name) are changed:
                If Not Designer.Animating AndAlso Not Designer.Exporting Then Refresh()
        End Select
    End Sub
#End Region

#Region "IsInputKey"
    Protected Overrides Function IsInputKey(keyData As Keys) As Boolean
        'Just say "yes" to everything:
        Return True
    End Function
#End Region

#Region "OnKeyDown"
    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        If Designer.SelectedScene Is Nothing OrElse Designer.SelectedLayer Is Nothing OrElse Not Designer.SelectedLayer.Is(Of Layer)() Then Return

        'Pass this event on to the currently-selected tool:
        Designer.SelectedTool.OnKeyDown(Me, e)
    End Sub
#End Region

#Region "OnKeyUp"
    Protected Overrides Sub OnKeyUp(e As KeyEventArgs)
        If Designer.SelectedScene Is Nothing OrElse Designer.SelectedLayer Is Nothing OrElse Not Designer.SelectedLayer.Is(Of Layer)() Then Return

        'Pass this event on to the currently-selected tool:
        Designer.SelectedTool.OnKeyUp(Me, e)
    End Sub
#End Region

#Region "OnMouseClick"
    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        If Designer.SelectedScene Is Nothing OrElse Designer.SelectedLayer Is Nothing OrElse Not Designer.SelectedLayer.Is(Of Layer)() Then Return

        Dim tme As New ToolMouseEvent(Me, e)

        'Pass this event on to the currently-selected tool:
        Designer.SelectedTool.OnMouseClick(tme)
    End Sub
#End Region

#Region "OnMouseDoubleClick"
    Protected Overrides Sub OnMouseDoubleClick(e As MouseEventArgs)
        If Designer.SelectedScene Is Nothing OrElse Designer.SelectedLayer Is Nothing OrElse Not Designer.SelectedLayer.Is(Of Layer)() Then Return

        Dim tme As New ToolMouseEvent(Me, e)

        'Pass this event on to the currently-selected tool:
        Designer.SelectedTool.OnMouseDoubleClick(tme)
    End Sub
#End Region

#Region "OnMouseDown"
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        If Designer.SelectedScene Is Nothing Then Return

        Dim tme As New ToolMouseEvent(Me, e)

        If Designer.SelectedObjects.Any() AndAlso Not Designer.PointEditMode Then
            SizingBoxDrag = 0
            For Each sbr In SizingBoxes
                If sbr.Contains(tme.ControlLocation) Then
                    SizingDragStart = tme.ControlLocation

                    SizingOriginalValues.Clear()
                    SizingOriginalPositions.Clear()

                    For Each vo As ArtificeObject In Designer.SelectedObjects
                        If vo.Is(Of ArtificeTransformable)() Then
                            SizingOriginalValues.Add(vo, New PointF(vo.As(Of ArtificeTransformable).ScaleX.Value, vo.As(Of ArtificeTransformable).ScaleY.Value))
                            SizingOriginalPositions.Add(vo, New PointF(vo.As(Of ArtificeTransformable).X.Value, vo.As(Of ArtificeTransformable).Y.Value))
                        End If
                    Next

                    If SizingBoxDrag = 8 Then
                        Dim g As Graphics = CreateGraphics()
                        Dim rc As New RenderContext(Designer.SelectedScene.Frame, g)

                        BeforeRender(rc)

                        RotationStartPoint = tme.ControlLocation
                        RotationObjectOrigin = Designer.SelectedObject.As(Of ArtificeMoveable)().TransformedLocation
                        OriginalRotation = Designer.SelectedObject.As(Of ArtificeTransformable)().Rotation.Delta

                        g.Dispose()
                    End If

                    Return
                End If
                SizingBoxDrag += 1
            Next

            SizingBoxDrag = -1
        End If

        If tme.AltKeyDown Then
            PanDragOffset = New Point(e.X - PanOffset.X, e.Y - PanOffset.Y)
        ElseIf Designer.SelectedLayer IsNot Nothing AndAlso Designer.SelectedLayer.Is(Of Layer)() Then
            'Pass this event on to the currently-selected tool:
            Designer.SelectedTool.OnMouseDown(tme)
        End If
    End Sub
#End Region

#Region "OnMouseMove"
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        If Designer.SelectedScene Is Nothing Then Return

        Dim tme As New ToolMouseEvent(Me, e)

        If SizingBoxDrag >= 0 Then
            Dim delta As New Point(tme.ControlLocation.X - SizingDragStart.X, tme.ControlLocation.Y - SizingDragStart.Y)
            Dim percentY As Single = delta.Y / SizingBounds.Height
            Dim percentX As Single = delta.X / SizingBounds.Height

            Dim ac As New AnimationContext(Designer.SelectedScene.Frame)
            Designer.Animating = True

            Select Case SizingBoxDrag
                Case 0 ' Top Left        
                    If tme.ShiftKeyDown Then
                        percentX = Math.Max(percentX, percentY)
                        percentY = Math.Max(percentX, percentY)
                    End If

                    For Each vo As ArtificeObject In Designer.SelectedObjects
                        If vo.Is(Of ArtificeTransformable)() Then
                            'Expand about the center point:
                            vo.As(Of ArtificeTransformable)().ScaleY.Value = SizingOriginalValues(vo).Y - percentY * 2
                            vo.As(Of ArtificeTransformable)().ScaleX.Value = SizingOriginalValues(vo).X - percentX * 2
                            vo.Animate(ac)
                        End If
                    Next
                Case 1 'Top
                    For Each vo As ArtificeObject In Designer.SelectedObjects
                        If vo.Is(Of ArtificeTransformable)() Then
                            'Expand about the center point:
                            vo.As(Of ArtificeTransformable)().ScaleY.Value = SizingOriginalValues(vo).Y - percentY * 2
                            vo.Animate(ac)
                        End If
                    Next
                Case 2 'Top Right
                    If tme.ShiftKeyDown Then
                        percentX = Math.Max(percentX, percentY)
                        percentY = Math.Max(percentX, percentY)
                    End If

                    For Each vo As ArtificeObject In Designer.SelectedObjects
                        If vo.Is(Of ArtificeTransformable)() Then
                            'Expand about the center point:
                            vo.As(Of ArtificeTransformable)().ScaleY.Value = SizingOriginalValues(vo).Y - percentY * 2
                            vo.As(Of ArtificeTransformable)().ScaleX.Value = SizingOriginalValues(vo).X + percentX * 2
                            vo.Animate(ac)
                        End If
                    Next
                Case 3 'Right
                    For Each vo As ArtificeObject In Designer.SelectedObjects
                        If vo.Is(Of ArtificeTransformable)() Then
                            'Expand about the center point:
                            vo.As(Of ArtificeTransformable)().ScaleX.Value = SizingOriginalValues(vo).X + percentX * 2
                            vo.Animate(ac)
                        End If
                    Next
                Case 4 'Bottom Right
                    If tme.ShiftKeyDown Then
                        percentX = Math.Max(percentX, percentY)
                        percentY = Math.Max(percentX, percentY)
                    End If

                    For Each vo As ArtificeObject In Designer.SelectedObjects
                        If vo.Is(Of ArtificeTransformable)() Then
                            'Expand about the center point:
                            vo.As(Of ArtificeTransformable)().ScaleY.Value = SizingOriginalValues(vo).Y + percentY * 2
                            vo.As(Of ArtificeTransformable)().ScaleX.Value = SizingOriginalValues(vo).X + percentX * 2
                            vo.Animate(ac)
                        End If
                    Next
                Case 5 'Bottom
                    For Each vo As ArtificeObject In Designer.SelectedObjects
                        If vo.Is(Of ArtificeTransformable)() Then
                            'Expand about the center point:
                            vo.As(Of ArtificeTransformable)().ScaleY.Value = SizingOriginalValues(vo).Y - percentY * 2
                            vo.Animate(ac)
                        End If
                    Next
                Case 6 'Bottom Left
                    If tme.ShiftKeyDown Then
                        percentX = Math.Max(percentX, percentY)
                        percentY = Math.Max(percentX, percentY)
                    End If

                    For Each vo As ArtificeObject In Designer.SelectedObjects
                        If vo.Is(Of ArtificeTransformable)() Then
                            'Expand about the center point:
                            vo.As(Of ArtificeTransformable)().ScaleY.Value = SizingOriginalValues(vo).Y + percentY * 2
                            vo.As(Of ArtificeTransformable)().ScaleX.Value = SizingOriginalValues(vo).X - percentX * 2
                            vo.Animate(ac)
                        End If
                    Next
                Case 7 'Left                
                    For Each vo As ArtificeObject In Designer.SelectedObjects
                        If vo.Is(Of ArtificeTransformable)() Then
                            'Expand about the center point:
                            vo.As(Of ArtificeTransformable)().ScaleX.Value = SizingOriginalValues(vo).X - percentX * 2
                            vo.Animate(ac)
                        End If
                    Next
                Case 8 'Rotate
                    ' Here we calculate the angle between the line segment from our starting point (P2) to our selected object's (0, 0) location (P1)
                    ' and the line segment at our new position (P3) to the object's location
                    ' Formula from http://stackoverflow.com/questions/1211212/how-to-calculate-an-angle-from-three-points
                    ' (see the answer further down where they differentiate between positive and negative angles)
                    ' arccos((P12^2 + P13^2 - P23^2) / (2 * P12 * P13))
                    ' Where P12 = sqrt((P1x - P2x)^2 + (P1y - P2y)^2)

                    Dim dx As Single = e.Location.X - RotationObjectOrigin.X
                    Dim dy As Single = e.Location.Y - RotationObjectOrigin.Y
                    Dim a As Double = Math.Atan2(dy, dx)

                    Dim dpx As Single = RotationStartPoint.X - RotationObjectOrigin.X
                    Dim dpy As Single = RotationStartPoint.Y - RotationObjectOrigin.Y
                    Dim b As Double = Math.Atan2(dpy, dpx)

                    Dim diff As Double = a - b

                    Designer.SelectedObject.As(Of ArtificeTransformable)().Rotation.Value = OriginalRotation + (diff * 180 / Math.PI)
                    Designer.SelectedObject.Animate(New AnimationContext(Designer.SelectedScene.Frame))
            End Select

            Designer.Animating = False

            Refresh()

            Return
        End If

        If tme.AltKeyDown Then
            If e.Button = MouseButtons.Left Then
                PanOffset = New Point(e.X - PanDragOffset.X, e.Y - PanDragOffset.Y)
            End If

            Cursor = Cursors.SizeAll
        ElseIf Designer.SelectedLayer IsNot Nothing AndAlso Designer.SelectedLayer.Is(Of Layer)() Then
            Cursor = Designer.SelectedTool.Cursor

            CType(My.Application.OpenForms(1), MainWindow).StatusBarPosition.Text = tme.Location.X & ", " & tme.Location.Y ' & " (" & e.X & ", " & e.Y & ")"

            'Pass this event on to the currently-selected tool:
            Designer.SelectedTool.OnMouseMove(tme)
        End If
    End Sub
#End Region

#Region "OnMouseUp"
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        If Designer.SelectedScene Is Nothing OrElse Designer.SelectedLayer Is Nothing OrElse Not Designer.SelectedLayer.Is(Of Layer)() Then Return

        Dim tme As New ToolMouseEvent(Me, e)

        If SizingBoxDrag >= 0 Then

            If SizingBoxDrag = 8 Then
                Designer.AddUndo(New PropertyChangeUndo(Designer.SelectedObject.As(Of ArtificeTransformable)().Rotation, "Value", OriginalRotation, Designer.SelectedObject.As(Of ArtificeTransformable)().Rotation.Value))
            End If

            SizingBoxDrag = -1
            Return
        End If

        'Pass this event on to the currently-selected tool:
        Designer.SelectedTool.OnMouseUp(tme)
    End Sub
#End Region

#Region "OnPaint"
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        'Adjust our quality settings based on the options:
        If Designer.HighQualityRender Then
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality
        Else
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed
            e.Graphics.CompositingQuality = CompositingQuality.HighSpeed
            e.Graphics.InterpolationMode = InterpolationMode.Low
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed
            e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixel
        End If

        'If no scene is selected, there's no point in drawing anything!
        If Designer.SelectedScene IsNot Nothing Then
            'Build our render context:
            Dim rc As New RenderContext(Designer.SelectedScene.Frame, e.Graphics)

            'Pre-render initialization & transformation:
            BeforeRender(rc)

            'Fill our stage bounds with the stage background color:
            Dim br As New SolidBrush(Designer.SelectedScene.StageColor.Color)

            e.Graphics.FillRectangle(br, StageBounds)

            br.Dispose()

            'If we're actually playing, don't draw items outside the bounds of the stage.
            'When we aren't playing we need to see items that we've moved off the edge.
            If Designer.Playing OrElse Designer.Exporting Then
                e.Graphics.SetClip(StageBounds)
            ElseIf Designer.SelectedContainer IsNot Nothing AndAlso Not Designer.SelectedContainer.Is(Of KeyFrame)() Then
                'Are we drilled down into another container (like a Group)? Then cover everything else up...
                Dim coverBrush As New SolidBrush(Color.FromArgb(128, 128, 128, 128))

                e.Graphics.FillRectangle(coverBrush, StageBounds)

                coverBrush.Dispose()

                '...and tell everything to render at 50% opacity. The selected container (Group) will reset the opacity to 1.0 when it draws its children:
                rc.OpacityStack.Push(0.5)
            End If

            'Make the scene render itself:
            Designer.SelectedScene.Render(rc)

            'Draw the little circle to show the center of this scene if no object is selected and we aren't playing:
            If Not Designer.Playing Then
                Dim p As New Pen(Color.FromArgb(128, 100, 100, 255))
                If Designer.SelectedObject Is Nothing Then e.Graphics.DrawEllipse(p, New Rectangle(-5, -5, 10, 10))
                e.Graphics.DrawLine(p, -StageBounds.Width \ 2, 0, StageBounds.Width \ 2, 0)
                e.Graphics.DrawLine(p, 0, -StageBounds.Height \ 2, 0, StageBounds.Height \ 2)
                p.Dispose()
            End If

            'Post-render cleanup:
            AfterRender(rc)

            'If we have ArtificeMovable's selected, that means we get to draw their selection bounds at this point
            ' (after everything else has rendered so that the sizing grips and selection borders appear
            ' on top of everything):
            If Designer.SelectedObjects.Any AndAlso Designer.SelectedObjects.First.Is(Of ArtificeMoveable)() Then
                'If we're not in point edit mode, draw the sizing & rotation grips:
                If Not Designer.PointEditMode Then
                    Dim selBounds As RectangleF = Designer.SelectedObjects.First.As(Of ArtificeMoveable)().TransformedBounds

                    'Build our selection bounds as the union of all selected objects' transformed bounds:
                    For Each vm As ArtificeMoveable In Designer.SelectedObjects
                        selBounds = RectangleF.Union(selBounds, vm.TransformedBounds)
                    Next

                    If SizingBoxDrag < 0 Then SizingBounds = selBounds

                    'Create the 9 sizing boxes (8 cardinal points and 1 rotation grip):
                    SizingBoxes(0) = New RectangleF(selBounds.Left - 5, selBounds.Top - 5, 10, 10)
                    SizingBoxes(1) = New RectangleF(selBounds.Left + (selBounds.Width / 2) - 5, selBounds.Top - 5, 10, 10)
                    SizingBoxes(2) = New RectangleF(selBounds.Right - 5, selBounds.Top - 5, 10, 10)
                    SizingBoxes(3) = New RectangleF(selBounds.Right - 5, selBounds.Top + (selBounds.Height / 2) - 5, 10, 10)
                    SizingBoxes(4) = New RectangleF(selBounds.Right - 5, selBounds.Bottom - 5, 10, 10)
                    SizingBoxes(5) = New RectangleF(selBounds.Left + (selBounds.Width / 2) - 5, selBounds.Bottom - 5, 10, 10)
                    SizingBoxes(6) = New RectangleF(selBounds.Left - 5, selBounds.Bottom - 5, 10, 10)
                    SizingBoxes(7) = New RectangleF(selBounds.Left - 5, selBounds.Top + (selBounds.Height / 2) - 5, 10, 10)

                    'Sizing Box 8 is the rotation grip at the bottom right:
                    SizingBoxes(8) = New RectangleF(selBounds.Right + 5, selBounds.Bottom + 5, 12, 12)

                    'Draw the rectangle
                    e.Graphics.DrawRectangle(Pens.Red, selBounds.X, selBounds.Y, selBounds.Width, selBounds.Height)

                    'Draw rotation grip (only for single-selected, ArtificeTransformable objects):
                    If Designer.SelectedObjects.Count = 1 AndAlso Designer.SelectedObject.Is(Of ArtificeTransformable)() Then
                        Dim rotatePen As New Pen(Color.Red)

                        'Put some arrows at the ends:
                        rotatePen.StartCap = LineCap.ArrowAnchor
                        rotatePen.EndCap = LineCap.ArrowAnchor

                        'e.Graphics.DrawCurve(rotatePen, New Point() {New Point(SizingBoxes(8).Left, SizingBoxes(8).Center.Y), New Point(SizingBoxes(8).Center.X, SizingBoxes(8).Bottom), New Point(SizingBoxes(8).Right, SizingBoxes(8).Center.Y), New Point(SizingBoxes(8).Center.X, SizingBoxes(8).Top)})
                        e.Graphics.DrawArc(rotatePen, SizingBoxes(8), -90, 300)

                        'Always dispose pens & brushes to avoid memory leaks!
                        rotatePen.Dispose()
                    End If

                    'Draw the sizing grips (except for #8, our rotation grip):
                    For sbi As Integer = 0 To 7
                        e.Graphics.FillRectangle(Brushes.Red, SizingBoxes(sbi))
                    Next

                    'Draw the little ellipse at the center of our selection:
                    e.Graphics.DrawEllipse(Pens.Red, selBounds.Center.X - 5, selBounds.Center.Y - 5, 10, 10)
                Else
                    'If we are in PointEditMode, draw grips for every point:
                    For Each vm As ArtificeMoveable In Designer.SelectedObjects
                        If vm.Is(Of VectorObject)() Then
                            For vpi As Integer = 0 To vm.Children.Count - 1
                                Dim vp As VectorPoint = vm.Children(vpi)

                                'Control points are drawn as circles, regular points as squares:
                                If vp.ControlPoint Then
                                    Dim dest As VectorPoint

                                    If vm.Children(vpi - 1).As(Of VectorPoint)().ControlPoint Then
                                        dest = vm.Children(vpi + 1)
                                    Else
                                        dest = vm.Children(vpi - 1)
                                    End If

                                    e.Graphics.DrawLine(Pens.Red, vp.TransformedBounds.Center().X, vp.TransformedBounds.Center().Y, dest.TransformedBounds.Center().X, dest.TransformedBounds.Center().Y)

                                    If Designer.SelectedPoint Is vp Then
                                        e.Graphics.FillEllipse(Brushes.Red, vp.TransformedBounds)
                                    Else
                                        e.Graphics.DrawEllipse(Pens.Red, vp.TransformedBounds)
                                    End If
                                Else
                                    If Designer.SelectedPoint Is vp Then
                                        e.Graphics.FillRectangle(Brushes.Red, vp.TransformedBounds)
                                    Else
                                        e.Graphics.DrawRectangle(Pens.Red, vp.TransformedBounds.X, vp.TransformedBounds.Y, vp.TransformedBounds.Width, vp.TransformedBounds.Height)
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If
            End If
        End If

        'Add a nice border around our control:
        ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Bump)
    End Sub
#End Region

#Region "OnPaintBackground"
    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        'Non-stage areas will be painted a default system control color:
        pevent.Graphics.FillRectangle(SystemBrushes.ControlDarkDark, pevent.ClipRectangle)
    End Sub
#End Region

#Region "OnSizeChanged"
    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)

        'Recalculate our bounds:
        CalculateStageBounds()
    End Sub
#End Region

#Region "SceneSurface_MouseWheel"
    Private Sub SceneSurface_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        'Adjust the zoom by the delta property of the mouse wheel:
        Zoom += (e.Delta / 120) * 0.1
    End Sub
#End Region

End Class

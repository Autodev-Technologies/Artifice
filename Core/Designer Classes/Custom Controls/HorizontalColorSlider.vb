Imports System.Drawing.Drawing2D

''' <summary>
''' A horizontal color slider, designed for alpha channel adjustment and (future) gradient color stop positions
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class HorizontalColorSlider

    Public Event MultipleStopsChanged As EventHandler
    Public Event OwnerDrawChanged As EventHandler
    Public Event DrawSurface(sender As Object, e As PaintEventArgs)
    Public Event StopPositionChanged(sender As Object, e As StopPositionChangedEventArgs)

    Private MarkerPath As GraphicsPath ' The path containing the color stop marker shape (the little arrow)
    Private MyColorBar As Bitmap ' The in-memory bitmap that contains the color bar

#Region "Properties"
    Private MyColorStops As New List(Of ColorStop)
    ''' <summary>
    ''' The list of all color stops on this gradient
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ColorStops As List(Of ColorStop)
        Get
            Return MyColorStops
        End Get
    End Property

    Private MyOwnerDraw As Boolean = False
    ''' <summary>
    ''' Determines whether this control is drawn automatically or by an external control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OwnerDraw As Boolean
        Get
            Return MyOwnerDraw
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = value <> MyOwnerDraw

            MyOwnerDraw = value

            If changed Then
                RaiseEvent OwnerDrawChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    Private MyMultipleStops As Boolean = True
    ''' <summary>
    ''' True if this control can display multiple color stops. If False, then only one color stop is allowed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MultipleStops As Boolean
        Get
            Return MultipleStops
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = value <> MyMultipleStops

            MyMultipleStops = value

            If changed Then
                If Not value Then
                    MyColorStops.Clear()
                    AddStop(0.5)
                End If

                RaiseEvent MultipleStopsChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    ''' <summary>
    ''' The stop position of the first (or only) color stop in this control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StopPosition As Single
        Get
            If Not ColorStops.Any Then Return 0.0

            Return ColorStops.First.Position
        End Get
        Set(value As Single)
            value = Math.Max(0.0, Math.Min(value, 1.0))

            If Not ColorStops.Any Then Return

            Dim changed As Boolean = value <> ColorStops.First.Position

            ColorStops.First.Position = value

            If changed Then
                RaiseEvent StopPositionChanged(Me, New StopPositionChangedEventArgs(ColorStops.First))
                Refresh()
            End If
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Creates a double-buffered control:
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.UserMouse, True)

        'Create an arrow shape to use as the stop marker:
        MarkerPath = New GraphicsPath

        MarkerPath.AddLine(7, 0, 3, -4)
        MarkerPath.AddLine(3, -4, 1, -4)
        MarkerPath.AddLine(0, -3, 0, 3)
        MarkerPath.AddLine(1, 4, 3, 4)
        MarkerPath.AddLine(3, 4, 7, 0)
    End Sub
#End Region

#Region "AddStop"
    ''' <summary>
    ''' Adds a color stop to this control at the given value
    ''' </summary>
    ''' <param name="value">The percentage value (betwen 0.0 and 1.0) of the location of this stop</param>
    ''' <remarks></remarks>
    Public Sub AddStop(value As Single)
        If Not MultipleStops AndAlso ColorStops.Any Then Return

        Dim s As New ColorStop()

        s.Color = Color.Black
        s.Position = value

        MyColorStops.Add(s)

        Invalidate()
    End Sub
#End Region

#Region "CreateBitmap"
    ''' <summary>
    ''' Creates the in-memory bitmap, sized to fit this control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateBitmap()
        If MyColorBar IsNot Nothing Then MyColorBar.Dispose()

        MyColorBar = New Bitmap(ClientRectangle.Width - 14, ClientRectangle.Height - 18 - 4)

        RedrawBitmap()
    End Sub
#End Region

#Region "OnMouseDown"
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If MultipleStops Then
                'TODO: add a color stop, make it the one you are dragging
            Else
                If ColorStops.Any Then
                    ColorStops.First.Position = (e.X - 7) / MyColorBar.Width
                    RaiseEvent StopPositionChanged(Me, New StopPositionChangedEventArgs(ColorStops.First))
                    Invalidate()
                End If
            End If
        End If
    End Sub
#End Region

#Region "OnMouseMove"
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If MultipleStops Then
                'TODO: drag the currently-selected color stop
            Else
                If ColorStops.Any Then
                    ColorStops.First.Position = (e.X - 7) / MyColorBar.Width
                    RaiseEvent StopPositionChanged(Me, New StopPositionChangedEventArgs(ColorStops.First))
                    Invalidate()
                End If
            End If
        End If
    End Sub
#End Region

#Region "OnPaint"
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        'Draw our in-memory buffer image:
        e.Graphics.DrawImage(MyColorBar, 7, (Height - MyColorBar.Height) \ 2)

        'Draw a nice border around the buffer image:
        ControlPaint.DrawBorder3D(e.Graphics, New Rectangle(5, (Height - MyColorBar.Height) \ 2 - 2, ClientRectangle.Width - 10, MyColorBar.Height + 4), Border3DStyle.Flat)

        'Draw our arrows for each color stop:
        Dim markerPen As New Pen(Color.FromArgb(116, 116, 116))

        For Each s As ColorStop In ColorStops
            Dim state = e.Graphics.Save()

            'Translate to the appropriate spot and rotate to point it down:
            e.Graphics.TranslateTransform(MyColorBar.Width * s.Position + 7, 0)
            e.Graphics.RotateTransform(90)

            e.Graphics.FillPath(Brushes.White, MarkerPath)
            e.Graphics.DrawPath(markerPen, MarkerPath)

            'Transform from the previous point location to the bottom of the control
            'and flip it on the x axis to make it point up (we flip it on the x because
            'we rotated it 90 degrees above)
            e.Graphics.TranslateTransform(Height - 1, 0)
            e.Graphics.ScaleTransform(-1.0, 1.0)

            e.Graphics.FillPath(Brushes.White, MarkerPath)
            e.Graphics.DrawPath(markerPen, MarkerPath)

            'Restore the original transform:
            e.Graphics.Restore(state)
        Next

        'Always dispose of pens and brushes to avoid memory leaks!
        markerPen.Dispose()
    End Sub
#End Region

#Region "OnPaintBackground"
    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        'Make this control's background the same as its parent's:
        Dim br As New SolidBrush(Parent.BackColor)
        pevent.Graphics.FillRectangle(br, pevent.ClipRectangle)
        br.Dispose()
    End Sub
#End Region

#Region "OnSizeChanged"
    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        'Recreate our buffer to match the new size of this control:
        CreateBitmap()
    End Sub
#End Region

#Region "RedrawBitmap"
    ''' <summary>
    ''' Redraw the color bar. In the future it will automatically draw the gradient.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RedrawBitmap()
        If MyColorBar Is Nothing Then Return

        Dim g As Graphics = Graphics.FromImage(MyColorBar)
        Dim br As New TextureBrush(My.Resources.Checkerboard)

        g.FillRectangle(br, 0, 0, MyColorBar.Width, MyColorBar.Height)

        br.Dispose()

        RaiseEvent DrawSurface(Me, New PaintEventArgs(g, New Rectangle(0, 0, MyColorBar.Width, MyColorBar.Height)))

        g.Dispose()

        Invalidate()
    End Sub
#End Region

End Class

''' <summary>
''' A single color stop in a color slider
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class ColorStop
    Public Color As Color

    Private MyPosition As Single = 0.5
    ''' <summary>
    ''' The position of this color stop, as a percentage between 0.0 and 1.0
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Position As Single
        Get
            Return MyPosition
        End Get
        Set(value As Single)
            value = Math.Max(0.0, Math.Min(1.0, value))

            MyPosition = value
        End Set
    End Property

End Class

''' <summary>
''' The event argument when a color stop position's is changed in a HorizontalColorSLider
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class StopPositionChangedEventArgs
    Private MyColorStop As ColorStop
    Public ReadOnly Property ColorStop As ColorStop
        Get
            Return MyColorStop
        End Get
    End Property

    Public Sub New(cs As ColorStop)
        MyColorStop = cs
    End Sub
End Class
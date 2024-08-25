''' <summary>
''' A control that maps out the x/y components of the shared AdvancedColor object
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class ColorSurface

    Private MySurface As Bitmap

#Region "Properties"
    Private WithEvents MyColor As AdvancedColor = AdvancedColor.FromHSV(180, 0, 0)
    ''' <summary>
    ''' The shared instance of AdvancedColor that this control is manipulating
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Color As AdvancedColor
        Get
            Return MyColor
        End Get
        Set(value As AdvancedColor)
            MyColor = value

            DrawSurface()
        End Set
    End Property

    Private MyZProperty As String
    ''' <summary>
    ''' The component that isn't being rendered on this control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ZProperty As String
        Get
            Return MyZProperty
        End Get
        Set(value As String)
            Dim changed As Boolean = MyZProperty <> value

            MyZProperty = value

            If changed Then
                DrawSurface()
            End If
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.        
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.UserMouse, True)
    End Sub
#End Region

#Region "CreateSurface"
    ''' <summary>
    ''' Creates the internal surface bitmap to the appropriate size for this control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSurface()
        If MySurface IsNot Nothing Then MySurface.Dispose()

        MySurface = New Bitmap(ClientRectangle.Width - 4, ClientRectangle.Height - 4)

        DrawSurface()
    End Sub
#End Region

#Region "DrawSurface"
    ''' <summary>
    ''' Redraws the x,y color map on our internal bitmap
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DrawSurface()
        If MySurface Is Nothing OrElse Color Is Nothing Then Return

        Dim sw As New Stopwatch

        sw.Start()

        Dim surfaceData As System.Drawing.Imaging.BitmapData = Nothing
        Dim surfaceBytes((MySurface.Width * MySurface.Height * 3) - 1) As Byte '3 bytes per pixel, RGB

        surfaceData = MySurface.LockBits(New Rectangle(0, 0, MySurface.Width, MySurface.Height), Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format24bppRgb)

        System.Runtime.InteropServices.Marshal.Copy(surfaceData.Scan0, surfaceBytes, 0, surfaceBytes.Length)

        Dim c As Color
        Dim prevA, prevB As Integer

        For rangeY As Integer = 0 To MySurface.Height - 1
            Dim y As Integer = MySurface.Height - 1 - rangeY

            For x As Integer = 0 To MySurface.Width - 1
                Dim i As Integer = (rangeY * MySurface.Width * 3) + (x * 3)

                Select Case ZProperty
                    Case "H"
                        Dim s As Integer = CInt(x * 100.0 / MySurface.Width)
                        Dim v As Integer = CInt(y * 100.0 / MySurface.Height)

                        If prevA <> s OrElse prevB <> v Then c = AdvancedColor.FromHSV(Color.H, s, v).Color

                        prevA = s
                        prevB = v
                    Case "S"
                        Dim h As Integer = CInt(x * 360.0 / MySurface.Width)
                        Dim v As Integer = CInt(y * 100.0 / MySurface.Height)

                        If prevA <> h OrElse prevB <> v Then c = AdvancedColor.FromHSV(h, Color.S, v).Color

                        prevA = h
                        prevB = v
                    Case "V"
                        Dim h As Integer = CInt(x * 360.0 / MySurface.Width)
                        Dim s As Integer = CInt(y * 100.0 / MySurface.Height)

                        If prevA <> h OrElse prevB <> s Then c = AdvancedColor.FromHSV(h, s, Color.V).Color

                        prevA = h
                        prevB = s
                    Case "R"
                        c = System.Drawing.Color.FromArgb(Me.Color.R, x * 255 / MySurface.Width, y * 255 / MySurface.Height)

                    Case "G"
                        c = System.Drawing.Color.FromArgb(x * 255 / MySurface.Width, Me.Color.G, y * 255 / MySurface.Height)

                    Case "B"
                        c = System.Drawing.Color.FromArgb(x * 255 / MySurface.Width, y * 255 / MySurface.Height, Me.Color.B)

                End Select

                surfaceBytes(i) = c.B
                surfaceBytes(i + 1) = c.G
                surfaceBytes(i + 2) = c.R
            Next
        Next

        System.Runtime.InteropServices.Marshal.Copy(surfaceBytes, 0, surfaceData.Scan0, surfaceBytes.Length)

        MySurface.UnlockBits(surfaceData)

        sw.Stop()

        Console.WriteLine(sw.ElapsedMilliseconds)

        Invalidate()
    End Sub
#End Region

#Region "MyColor_PropertyChanged"
    ''' <summary>
    ''' Handles any property changes of our AdvancedColor object
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MyColor_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles MyColor.PropertyChanged
        'If the property changing is our current Z Property (i.e. the vertical slider), then we need to draw the surface
        If e.PropertyName = ZProperty Then
            DrawSurface()
        End If
    End Sub
#End Region

#Region "OnMouseDown"
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            SetPosition(e.Location)
        End If
    End Sub
#End Region

#Region "OnMouseMove"
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            SetPosition(e.Location)
        End If
    End Sub
#End Region

#Region "OnPaint"
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        e.Graphics.DrawImage(MySurface, 2, 2)

        ControlPaint.DrawBorder3D(e.Graphics, 0, 0, ClientRectangle.Width, ClientRectangle.Height, Border3DStyle.Flat)

        If Color IsNot Nothing Then

            Dim x, y As Integer

            Select Case ZProperty
                Case "H"
                    x = Color.S / 100 * MySurface.Width
                    y = Color.V / 100 * MySurface.Height

                Case "S"
                    x = Color.H / 360 * MySurface.Width
                    y = Color.V / 100 * MySurface.Height

                Case "V"
                    x = Color.H / 360 * MySurface.Width
                    y = Color.S / 100 * MySurface.Height

                Case "R"
                    x = Color.G / 255 * MySurface.Width
                    y = Color.B / 255 * MySurface.Height

                Case "G"
                    x = Color.R / 255 * MySurface.Width
                    y = Color.B / 255 * MySurface.Height

                Case "B"
                    x = Color.R / 255 * MySurface.Width
                    y = Color.G / 255 * MySurface.Height

            End Select

            y = MySurface.Height - y - 2

            'Change the colour of our ellipse based on the brightness of the selected color:
            e.Graphics.DrawEllipse(IIf(Color.V < 65, Pens.White, Pens.Black), x - 8, y - 8, 16, 16)
        End If
    End Sub
#End Region

#Region "OnSizeChanged"
    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        'Recreate our internal surface bitmap to match the new size of the control
        CreateSurface()
    End Sub
#End Region

#Region "SetPosition"
    ''' <summary>
    ''' Updates the AdvancedColor components based on the given location
    ''' </summary>
    ''' <param name="location"></param>
    ''' <remarks></remarks>
    Private Sub SetPosition(location As Point)
        Dim x As Integer = location.X - 2
        Dim y As Integer = MySurface.Height - location.Y - 2

        Select Case ZProperty
            Case "H"
                Color.S = x / MySurface.Width * 100
                Color.V = y / MySurface.Width * 100

                Color.UpdateFromHSV()

            Case "S"
                Color.H = x / MySurface.Width * 360
                Color.V = y / MySurface.Width * 100

                Color.UpdateFromHSV()

            Case "V"
                Color.H = x / MySurface.Width * 360
                Color.S = y / MySurface.Width * 100

                Color.UpdateFromHSV()

            Case "R"
                Color.G = x / MySurface.Width * 255
                Color.B = y / MySurface.Width * 255

                Color.UpdateFromRGB()

            Case "G"
                Color.R = x / MySurface.Width * 255
                Color.B = y / MySurface.Width * 255

                Color.UpdateFromRGB()

            Case "B"
                Color.R = x / MySurface.Width * 255
                Color.G = y / MySurface.Width * 255

                Color.UpdateFromRGB()
        End Select

        Refresh()
    End Sub
#End Region

End Class

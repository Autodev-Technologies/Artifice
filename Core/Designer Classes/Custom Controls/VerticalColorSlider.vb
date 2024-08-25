Imports System.Drawing.Drawing2D
Imports System.Reflection

''' <summary>
''' A vertical color control to adjust the "Z" component of the AdvancedColorPicker dialog
''' </summary>
''' <remarks></remarks>
Public Class VerticalColorSlider
    Public Event SliderValueChanged As EventHandler

    Private MyColorBar As Bitmap
    Private MarkerPath As GraphicsPath
    Private MyZPropertyInfo As PropertyInfo

#Region "Public Class RGB"
    ''' <summary>
    ''' A simplified color structure for use by VerticalColorSlider and AdvancedColor
    ''' </summary>
    ''' <remarks>Created by Autodev</remarks>
    Public Class RGB
        Private MyR, MyG, MyB As Integer

        Public Function GetColor() As Color
            Return Color.FromArgb(MyR, MyG, MyB)
        End Function

        Public Sub New(r As Integer, b As Integer, g As Integer)
            MyR = r
            MyG = g
            MyB = b
        End Sub
    End Class
#End Region

#Region "Properties"
    Private WithEvents MyAdvancedColor As AdvancedColor = Global.Artifice.AdvancedColor.FromHSV(180, 0, 0)
    Public Property AdvancedColor As AdvancedColor
        Get
            Return MyAdvancedColor
        End Get
        Set(value As AdvancedColor)
            MyAdvancedColor = value
        End Set
    End Property

    Private MyZProperty As String = "H"
    Public Property ZProperty As String
        Get
            Return MyZProperty
        End Get
        Set(value As String)
            If value = "" Then value = "H"
            Dim changed As Boolean = MyZProperty <> value
            MyZProperty = value

            If changed OrElse MyZPropertyInfo Is Nothing Then
                MyZPropertyInfo = GetType(AdvancedColor).GetProperty(value)

                RedrawBitmap()
            End If
        End Set
    End Property

    Private MySliderValue As Decimal = 0.5
    Public Property SliderValue As Decimal
        Get
            Return MySliderValue
        End Get
        Set(value As Decimal)
            value = Math.Max(0.0, Math.Min(1.0, value))

            Dim changed As Boolean = value <> MySliderValue

            MySliderValue = value

            If changed Then
                Dim adjustedValue As Integer

                Select Case ZProperty
                    Case "H"
                        adjustedValue = 360 - 360 * value
                    Case "S", "V"
                        adjustedValue = 100 - 100 * value
                    Case Else
                        adjustedValue = 255 - 255 * value
                End Select

                MyZPropertyInfo.SetValue(MyAdvancedColor, adjustedValue)

                Select Case ZProperty
                    Case "H", "S", "V"
                        AdvancedColor.UpdateFromHSV()
                    Case Else
                        AdvancedColor.UpdateFromRGB()
                End Select

                RaiseEvent SliderValueChanged(Me, EventArgs.Empty)
                Refresh()
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

        MarkerPath = New GraphicsPath

        MarkerPath.AddLine(7, 0, 3, -4)
        MarkerPath.AddLine(3, -4, 1, -4)
        MarkerPath.AddLine(0, -3, 0, 3)
        MarkerPath.AddLine(1, 4, 3, 4)
        MarkerPath.AddLine(3, 4, 7, 0)
    End Sub
#End Region

#Region "CreateBitmap"
    ''' <summary>
    ''' Creates the in-memory bitmap to that holds the range of the z property values
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateBitmap()
        If MyColorBar IsNot Nothing Then MyColorBar.Dispose()

        MyColorBar = New Bitmap(ClientRectangle.Width - 18 - 4, ClientRectangle.Height - 14)

        RedrawBitmap()
    End Sub
#End Region

#Region "HSVtoRGB"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="H"></param>
    ''' <param name="S"></param>
    ''' <param name="V"></param>
    ''' <returns></returns>
    ''' <remarks>From http://stackoverflow.com/questions/4123998/algorithm-to-switch-between-rgb-and-hsb-color-values </remarks>
    Public Shared Function HSVtoRGB(ByVal H As Integer, ByVal S As Integer, ByVal V As Integer) As RGB
        ''# Scale the Saturation and Value components to be between 0 and 1
        Dim hue As Decimal = H
        Dim sat As Decimal = S / 100D
        Dim val As Decimal = V / 100D

        Dim r As Decimal
        Dim g As Decimal
        Dim b As Decimal

        If sat = 0 Then
            ''# If the saturation is 0, then all colors are the same.
            ''# (This is some flavor of gray.)
            r = val
            g = val
            b = val
        Else
            ''# Calculate the appropriate sector of a 6-part color wheel
            Dim sectorPos As Decimal = hue / 60D
            Dim sectorNumber As Integer = CInt(Math.Floor(sectorPos))

            ''# Get the fractional part of the sector
            ''# (that is, how many degrees into the sector you are)
            Dim fractionalSector As Decimal = sectorPos - sectorNumber

            ''# Calculate values for the three axes of the color
            Dim p As Decimal = val * (1 - sat)
            Dim q As Decimal = val * (1 - (sat * fractionalSector))
            Dim t As Decimal = val * (1 - (sat * (1 - fractionalSector)))

            ''# Assign the fractional colors to red, green, and blue
            ''# components based on the sector the angle is in
            Select Case sectorNumber
                Case 0, 6
                    r = val
                    g = t
                    b = p
                Case 1
                    r = q
                    g = val
                    b = p
                Case 2
                    r = p
                    g = val
                    b = t
                Case 3
                    r = p
                    g = q
                    b = val
                Case 4
                    r = t
                    g = p
                    b = val
                Case 5
                    r = val
                    g = p
                    b = q
            End Select
        End If

        ''# Scale the red, green, and blue values to be between 0 and 255
        r *= 255
        g *= 255
        b *= 255

        ''# Return a color in the new color space
        Return New RGB(CInt(Math.Round(r, MidpointRounding.AwayFromZero)), _
                       CInt(Math.Round(g, MidpointRounding.AwayFromZero)), _
                       CInt(Math.Round(b, MidpointRounding.AwayFromZero)))
    End Function
#End Region

#Region "MyAdvancedColor_PropertyChanged"
    Private Sub MyAdvancedColor_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles MyAdvancedColor.PropertyChanged
        If e.PropertyName = ZProperty Then
            Select Case ZProperty
                Case "H"
                    SliderValue = (360 - CInt(MyZPropertyInfo.GetValue(AdvancedColor))) / 360.0
                Case "S", "V"
                    SliderValue = (100 - CInt(MyZPropertyInfo.GetValue(AdvancedColor))) / 100.0
                Case Else
                    SliderValue = (255 - CInt(MyZPropertyInfo.GetValue(AdvancedColor))) / 255.0
            End Select '

            Invalidate()
        End If

        If ZProperty <> "H" Then
            RedrawBitmap()
        End If
    End Sub
#End Region

#Region "OnMouseDown"
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            SliderValue = (e.Y - 7) / MyColorBar.Height
        End If
    End Sub
#End Region

#Region "OnMouseMove"
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            SliderValue = (e.Y - 7) / MyColorBar.Height
        End If
    End Sub
#End Region

#Region "OnPaint"
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        e.Graphics.DrawImage(MyColorBar, (Width - MyColorBar.Width) \ 2, 7)
        ControlPaint.DrawBorder3D(e.Graphics, New Rectangle((Width - MyColorBar.Width) \ 2 - 2, 5, MyColorBar.Width + 4, ClientRectangle.Height - 10), Border3DStyle.Flat)

        'Draw the slider:
        Dim yPos As Integer = SliderValue * MyColorBar.Height + 7

        Dim markerPen As New Pen(Color.FromArgb(116, 116, 116))

        e.Graphics.TranslateTransform(0, yPos)

        e.Graphics.FillPath(Brushes.White, MarkerPath)
        e.Graphics.DrawPath(markerPen, MarkerPath)

        e.Graphics.TranslateTransform(ClientRectangle.Width - 1, 0)
        e.Graphics.ScaleTransform(-1.0, 1.0)

        e.Graphics.FillPath(Brushes.White, MarkerPath)
        e.Graphics.DrawPath(markerPen, MarkerPath)

        markerPen.Dispose()
    End Sub
#End Region

#Region "OnPaintBackground"
    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        'Make the background the same as the parent's (a fake transparent appearance):
        Dim br As New SolidBrush(Parent.BackColor)
        pevent.Graphics.FillRectangle(br, pevent.ClipRectangle)
        br.Dispose()
    End Sub
#End Region

#Region "OnSizeChanged"
    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        'Recreate our in-memory bitmap to match the size of the control:
        CreateBitmap()
    End Sub
#End Region

#Region "RedrawBitmap"
    ''' <summary>
    ''' Draws the range of values for the Z property into the in-memory bitmap:
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RedrawBitmap()
        If MyColorBar Is Nothing Then Return

        Dim g As Graphics = Graphics.FromImage(MyColorBar)
        Dim p As Pen = Nothing
        Dim c As Color

        For y As Integer = 0 To MyColorBar.Height - 1
            Select Case ZProperty
                Case "H"
                    Dim hueValue As Integer = CInt(y / MyColorBar.Height * 360.0)

                    c = HSVtoRGB(hueValue, 100, 100).GetColor

                Case "S"
                    Dim s As Integer = CInt((MyColorBar.Height - y) / MyColorBar.Height * 100)

                    c = HSVtoRGB(AdvancedColor.H, s, AdvancedColor.V).GetColor
                Case "V"
                    Dim v As Integer = CInt((MyColorBar.Height - y) / MyColorBar.Height * 100)

                    c = HSVtoRGB(AdvancedColor.H, AdvancedColor.S, v).GetColor

                Case "R"
                    c = Drawing.Color.FromArgb(CInt((MyColorBar.Height - y) / MyColorBar.Height * 255), AdvancedColor.G, AdvancedColor.B)
                Case "G"
                    c = Drawing.Color.FromArgb(AdvancedColor.R, CInt((MyColorBar.Height - y) / MyColorBar.Height * 255), AdvancedColor.B)
                Case "B"
                    c = Drawing.Color.FromArgb(AdvancedColor.R, AdvancedColor.G, CInt((MyColorBar.Height - y) / MyColorBar.Height * 255))
            End Select

            If p Is Nothing OrElse Not p.Color.Equals(c) Then
                If p IsNot Nothing Then p.Dispose()

                p = New Pen(c, 1.0)
            End If

            g.DrawLine(p, 0, y, MyColorBar.Width, y)
        Next

        g.Dispose()

        Invalidate()
    End Sub
#End Region

End Class

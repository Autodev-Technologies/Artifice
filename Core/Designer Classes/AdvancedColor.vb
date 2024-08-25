Imports System.Reflection

''' <summary>
''' A color object that handles RGB and HSV components for use by the AdvancedColorPicker control
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class AdvancedColor
    Inherits PropertyChanger

    Private MyR, MyB, MyG, MyH, MyS, MyV As Integer
    Private MyA As Integer = 255

#Region "Properties"
    Public Property A As Integer
        Get
            Return MyA
        End Get
        Set(value As Integer)
            Dim changed As Boolean = value <> MyA

            MyA = value

            If changed Then OnPropertyChanged("A")
        End Set
    End Property

    Private MyAnchorProperty As String = "H"
    Public Property AnchorProperty As String
        Get
            Return MyAnchorProperty
        End Get
        Set(value As String)
            Dim changed As Boolean = value <> MyAnchorProperty

            MyAnchorProperty = value

            If changed Then OnPropertyChanged("AnchorProperty")
        End Set
    End Property

    Public Property B As Integer
        Get
            Return MyB
        End Get
        Set(value As Integer)
            value = Math.Max(0, Math.Min(value, 255))
            Dim changed As Boolean = value <> MyB

            MyB = value

            If changed Then                
                OnPropertyChanged("B")
            End If

        End Set
    End Property

    Public ReadOnly Property Color As Color
        Get
            Return System.Drawing.Color.FromArgb(MyA, MyR, MyG, MyB)
        End Get
    End Property

    Public Property G As Integer
        Get
            Return MyG
        End Get
        Set(value As Integer)
            value = Math.Max(0, Math.Min(value, 255))
            Dim changed As Boolean = value <> MyG

            MyG = value

            If changed Then                
                OnPropertyChanged("G")
            End If

        End Set
    End Property

    Public Property H As Integer
        Get
            Return MyH
        End Get
        Set(value As Integer)
            value = Math.Max(0, Math.Min(value, 360))
            Dim changed As Boolean = value <> MyH

            MyH = value

            If changed Then                
                OnPropertyChanged("H")
            End If

        End Set
    End Property

    Public Property R As Integer
        Get
            Return MyR
        End Get
        Set(value As Integer)
            value = Math.Max(0, Math.Min(value, 255))
            Dim changed As Boolean = value <> MyR

            MyR = value

            If changed Then                
                OnPropertyChanged("R")
            End If
        End Set
    End Property

    Public Property S As Integer
        Get
            Return MyS
        End Get
        Set(value As Integer)
            value = Math.Max(0, Math.Min(value, 100))
            Dim changed As Boolean = value <> MyS

            MyS = value

            If changed Then                
                OnPropertyChanged("S")
            End If
        End Set
    End Property

    Public Property V As Integer
        Get
            Return MyV
        End Get
        Set(value As Integer)
            value = Math.Max(0, Math.Min(value, 100))
            Dim changed As Boolean = value <> MyV

            MyV = value

            If changed Then                
                OnPropertyChanged("V")
            End If
        End Set
    End Property

#End Region

#Region "FromColor"
    ''' <summary>
    ''' Returns a new instance of AdvancedColor from the given System.Drawing.Color object
    ''' </summary>
    ''' <param name="c">The color to initialize this AdvancedColor with</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FromColor(c As Color) As AdvancedColor
        Dim ac As New AdvancedColor

        ac.MyA = c.A
        ac.RGBtoHSV(c.R, c.G, c.B)

        Return ac
    End Function
#End Region

#Region "FromHSV"
    ''' <summary>
    ''' Returns a new instance of AdvancedColor with the given H, S, and V values
    ''' </summary>
    ''' <param name="h">The hue of the new color</param>
    ''' <param name="s">The saturation of the new color</param>
    ''' <param name="v">The luminosity of the new color</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FromHSV(h As Integer, s As Integer, v As Integer) As AdvancedColor
        Dim ac As New AdvancedColor

        ac.HSVtoRGB(h, s, v)

        Return ac
    End Function
#End Region

#Region "FromRGB"
    ''' <summary>
    ''' Returns a new instance of AdvancedColor with the given R, G, and B values
    ''' </summary>
    ''' <param name="r"></param>
    ''' <param name="g"></param>
    ''' <param name="b"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FromRGB(r As Integer, g As Integer, b As Integer) As AdvancedColor
        Dim ac As New AdvancedColor

        ac.RGBtoHSV(r, g, b)

        Return ac
    End Function
#End Region

#Region "HSVtoRGB"
    ''' <summary>
    ''' Converts the HSV values to RGB values, storing all internally
    ''' </summary>
    ''' <param name="H"></param>
    ''' <param name="S"></param>
    ''' <param name="V"></param>
    ''' <remarks>From http://stackoverflow.com/questions/4123998/algorithm-to-switch-between-rgb-and-hsb-color-values </remarks>
    Private Sub HSVtoRGB(ByVal H As Integer, ByVal S As Integer, ByVal V As Integer)
        MyH = H
        MyS = S
        MyV = V

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
        MyR = CInt(Math.Round(r, MidpointRounding.AwayFromZero))
        MyG = CInt(Math.Round(g, MidpointRounding.AwayFromZero))
        MyB = CInt(Math.Round(b, MidpointRounding.AwayFromZero))

        OnPropertyChanged("R")
        OnPropertyChanged("G")
        OnPropertyChanged("B")
    End Sub
#End Region

#Region "RGBtoHSV"
    ''' <summary>
    ''' Converts the RGB values (from the given System.Drawing.Color) to HSV values, storing all internally
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Private Sub RGBtoHSV(c As Color) 'As HSV
        A = c.A
        RGBtoHSV(c.R, c.G, c.B)
    End Sub

    ''' <summary>
    ''' Converts the given RGB values to HSV values, storing all internally
    ''' </summary>
    ''' <param name="R"></param>
    ''' <param name="G"></param>
    ''' <param name="B"></param>
    ''' <remarks>'From http://stackoverflow.com/questions/4123998/algorithm-to-switch-between-rgb-and-hsb-color-values </remarks>
    Private Sub RGBtoHSV(ByVal R As Integer, ByVal G As Integer, ByVal B As Integer) 'As HSV
        MyR = R
        MyG = G
        MyB = B

        ''# Normalize the RGB values by scaling them to be between 0 and 1
        Dim red As Decimal = R / 255D
        Dim green As Decimal = G / 255D
        Dim blue As Decimal = B / 255D

        Dim minValue As Decimal = Math.Min(red, Math.Min(green, blue))
        Dim maxValue As Decimal = Math.Max(red, Math.Max(green, blue))
        Dim delta As Decimal = maxValue - minValue

        Dim h As Decimal
        Dim s As Decimal
        Dim v As Decimal = maxValue

        ''# Calculate the hue (in degrees of a circle, between 0 and 360)
        Select Case maxValue
            Case red
                If green >= blue Then
                    If delta = 0 Then
                        h = 0
                    Else
                        h = 60 * (green - blue) / delta
                    End If
                ElseIf green < blue Then
                    h = 60 * (green - blue) / delta + 360
                End If
            Case green
                h = 60 * (blue - red) / delta + 120
            Case blue
                h = 60 * (red - green) / delta + 240
        End Select

        ''# Calculate the saturation (between 0 and 1)
        If maxValue = 0 Then
            s = 0
        Else
            s = 1D - (minValue / maxValue)
        End If

        ''# Scale the saturation and value to a percentage between 0 and 100
        s *= 100
        v *= 100

        ''# Return a color in the new color space
        MyH = CInt(Math.Round(h, MidpointRounding.AwayFromZero))
        MyS = CInt(Math.Round(s, MidpointRounding.AwayFromZero))
        MyV = CInt(Math.Round(v, MidpointRounding.AwayFromZero))

        OnPropertyChanged("H")
        OnPropertyChanged("S")
        OnPropertyChanged("V")
    End Sub
#End Region

#Region "ToHexString"
    ''' <summary>
    ''' Returns this object as an HTML-friendly hex color string
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ToHexString() As String
        Return R.ToString("x2") & G.ToString("x2") & B.ToString("x2")
    End Function
#End Region

#Region "Update"
    ''' <summary>
    ''' Sets this color to the given System.Drawing.Color
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Public Sub Update(c As Color)
        A = c.A
        UpdateRGB(c.R, c.G, c.B)
    End Sub
#End Region

#Region "UpdateRGB"
    ''' <summary>
    ''' Sets this color to the given R, G, and B values
    ''' </summary>
    ''' <param name="r">The red component</param>
    ''' <param name="g">The green component</param>
    ''' <param name="b">The blue component</param>
    ''' <remarks></remarks>
    Public Sub UpdateRGB(r As Integer, g As Integer, b As Integer)
        RGBtoHSV(r, g, b)
    End Sub

    ''' <summary>
    ''' Updates the HSV values based on the current RGB values
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateFromRGB()
        RGBtoHSV(R, G, B)
    End Sub
#End Region

#Region "UpdateFromHSV"
    Public Sub UpdateFromHSV()
        HSVtoRGB(H, S, V)
    End Sub
#End Region

End Class

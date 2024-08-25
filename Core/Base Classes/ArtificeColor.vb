Imports System.Drawing
Imports System.ComponentModel

''' <summary>
''' Represents an alpha-blended colour amount
''' </summary>
''' <remarks>Created by Autodev</remarks>
<TypeConverter(GetType(ExpandableObjectConverter))>
Public Class ArtificeColor
    Inherits PropertyChanger

#Region "Properties"
    Private MyColor As Color = Color.Black
    ''' <summary>
    ''' A System.Drawing.Color instance of this color
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Public Property Color As Color
        Get
            Return MyColor
        End Get
        Set(value As Color)
            Dim changed As Boolean = value <> MyColor

            MyColor = value

            If changed Then
                OnPropertyChanged("Color")
            End If
        End Set
    End Property

    ''' <summary>
    ''' The alpha channel of this color
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Alpha As Integer
        Get
            Return MyColor.A
        End Get
        Set(value As Integer)
            value = Math.Max(0, Math.Min(255, value))

            Dim changed As Boolean = value <> Alpha

            MyColor = Color.FromArgb(value, MyColor.R, MyColor.G, MyColor.B)

            If changed Then
                OnPropertyChanged("Alpha")
                OnPropertyChanged("Color")
            End If
        End Set
    End Property

    ''' <summary>
    ''' The red channel of this color
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Red As Integer
        Get
            Return MyColor.R
        End Get
        Set(value As Integer)
            value = Math.Max(0, Math.Min(255, value))

            Dim changed As Boolean = value <> Alpha

            MyColor = Color.FromArgb(MyColor.A, value, MyColor.G, MyColor.B)

            If changed Then
                OnPropertyChanged("Red")
                OnPropertyChanged("Color")
            End If
        End Set
    End Property

    ''' <summary>
    ''' The green channel of this color
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Green As Integer
        Get
            Return MyColor.G
        End Get
        Set(value As Integer)
            value = Math.Max(0, Math.Min(255, value))

            Dim changed As Boolean = value <> Green

            MyColor = Color.FromArgb(MyColor.A, MyColor.R, value, MyColor.B)

            If changed Then
                OnPropertyChanged("Green")
                OnPropertyChanged("Color")
            End If
        End Set
    End Property

    ''' <summary>
    ''' The blue channel of this color
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Blue As Integer
        Get
            Return MyColor.B
        End Get
        Set(value As Integer)
            value = Math.Max(0, Math.Min(255, value))
            Dim changed As Boolean = value <> Blue

            MyColor = Color.FromArgb(MyColor.A, MyColor.R, MyColor.G, value)

            If changed Then
                OnPropertyChanged("Blue")
                OnPropertyChanged("Color")
            End If
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Creates a new instance of ArtificeColor using the given System.Drawing.Color
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Public Sub New(c As Color)
        Color = c
    End Sub

    ''' <summary>
    ''' Creates a new instance of ArtificeColor using the given color components
    ''' </summary>
    ''' <param name="a">The alpha channel value</param>
    ''' <param name="r">The red channel value</param>
    ''' <param name="g">The green channel value</param>
    ''' <param name="b">The blue channel value</param>
    ''' <remarks></remarks>
    Public Sub New(a As Integer, r As Integer, g As Integer, b As Integer)
        Color = System.Drawing.Color.FromArgb(Constrain(a), Constrain(r), Constrain(g), Constrain(b))
    End Sub

    Public Sub New(r As Integer, g As Integer, b As Integer)
        Color = System.Drawing.Color.FromArgb(Constrain(r), Constrain(g), Constrain(b))
    End Sub

    Public Sub New(argb As Integer)
        Color = System.Drawing.Color.FromArgb(argb)
    End Sub
#End Region

#Region "Constrain"
    ''' <summary>
    ''' Ensures that the given value is between 0 and 255, inclusively
    ''' </summary>
    ''' <param name="v"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Constrain(v As Integer) As Integer
        Return Math.Max(0, Math.Min(255, v))
    End Function
#End Region

#Region "Equals"
    ''' <summary>
    ''' Tests for equality between this ArtificeColor and the given object
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(obj As Object) As Boolean
        If obj Is Nothing Then Return False

        'Are we testing against a color? 
        If obj.GetType() Is GetType(Color) Then Return MyColor.Equals(obj)

        If obj.GetType() Is GetType(ArtificeColor) Then Return MyColor.Equals(CType(obj, ArtificeColor).MyColor)

        Return False
    End Function
#End Region

#Region "GetBrush"
    ''' <summary>
    ''' Returns as new SolidBrush for this ArtificeColor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBrush() As Brush
        Return New SolidBrush(Color)
    End Function

    ''' <summary>
    ''' Returns a new SolidBrush for this ArtificeColor, with the given opacity multiplier
    ''' </summary>
    ''' <param name="opacity">A value between 0 and 1 to multiply this ArtificeColor's opacity by</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBrush(opacity As Single) As Brush
        Return New SolidBrush(System.Drawing.Color.FromArgb(Constrain(Color.A * opacity), Color.R, Color.G, Color.B))
    End Function
#End Region

#Region "Parse"
    ''' <summary>
    ''' Parses the given string and returns an instance of ArtificeColor
    ''' </summary>
    ''' <param name="s">The string to parse</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Parse(s As String) As ArtificeColor
        Dim vc As New ArtificeColor(Color.FromArgb(CInt(s)))

        Return vc
    End Function
#End Region

#Region "ToRGBString"
    ''' <summary>
    ''' Converts this ArtificeColor to an SVG-friendly rgb(r,g,b) string
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToRGBString() As String
        Return "rgb(" & Red & ", " & Green & ", " & Blue & ")"
    End Function
#End Region

#Region "ToString"
    Public Overrides Function ToString() As String
        Return "(" & Alpha & ", " & Red & ", " & Green & ", " & Blue & ")"
    End Function
#End Region

End Class

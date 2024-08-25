Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Drawing.Design

''' <summary>
''' Represents a visual text object
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class Text
    Inherits ArtificeTransformable

    Private MyPath As GraphicsPath = Nothing

#Region "Properties"

    Private WithEvents MyFillColor As New LerpableColor(Color.Blue)
    <Category("Appearance"), DisplayName("Fill Color")> _
    Public ReadOnly Property FillColor As LerpableColor
        Get
            Return MyFillColor
        End Get
    End Property

    <Browsable(False)> _
    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Text
        End Get
    End Property

    Private WithEvents MyLineColor As New LerpableColor(Color.Black)
    ', Editor(GetType(Lerpable(Of Color)), GetType(LerpableEditor))> _
    <Category("Appearance"), DisplayName("Line Color")> _
    Public ReadOnly Property LineColor As LerpableColor
        Get
            Return MyLineColor
        End Get
    End Property

    Private MyLineWidth As New LerpableSingle(1.0)
    <Category("Appearance"), DisplayName("Line Width")> _
    Public Property LineWidth As LerpableSingle
        Get
            Return MyLineWidth
        End Get
        Set(value As LerpableSingle)
            Dim changed As Boolean = Not value.Equals(MyLineWidth)

            MyLineWidth = value

            If changed Then
                OnPropertyChanged("LineWidth")
            End If
        End Set
    End Property

    Private MyFontFamily As FontFamily = New FontFamily("Verdana")
    <Category("Font"), DisplayName("Font Family"), TypeConverter(GetType(FontFamilyTypeConverter)), Editor(GetType(FontFamilyEditor), GetType(UITypeEditor))> _
    Public Property FontFamily As FontFamily
        Get
            Return MyFontFamily
        End Get
        Set(value As FontFamily)
            Dim changed As Boolean = Not value.Equals(MyFontFamily)

            MyFontFamily = value

            If changed Then
                RecreatePath()
                OnPropertyChanged("FontFamily")
            End If
        End Set
    End Property

    Private MyStyle As FontStyle = FontStyle.Regular
    <Category("Font")> _
    Public Property Style As FontStyle
        Get
            Return MyStyle
        End Get
        Set(value As FontStyle)
            Dim changed As Boolean = Not value.Equals(MyStyle)

            MyStyle = value

            If changed Then
                RecreatePath()

                OnPropertyChanged("Style")
            End If

        End Set
    End Property

    Private MySize As New LerpableSingle(55.0F)
    <Category("Font")> _
    Public Property Size As LerpableSingle
        Get
            Return MySize
        End Get
        Set(value As LerpableSingle)
            Dim changed As Boolean = Not value.Equals(MySize)

            MySize = value

            If changed Then
                RecreatePath()

                OnPropertyChanged("Size")
            End If

        End Set
    End Property

    Private MyText As String = "Text Goes Here"
    Public Property Text As String
        Get
            Return MyText
        End Get
        Set(value As String)
            Dim changed As Boolean = MyText <> value

            MyText = value

            If changed Then
                RecreatePath()
                OnPropertyChanged("Text")
            End If
        End Set
    End Property

#End Region

#Region "Constructor"
    Public Sub New()
        RecreatePath()
    End Sub
#End Region

#Region "ConvertToVectorObject"
    ''' <summary>
    ''' Converts this text object to an instance of VectorObject
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConvertToVectorObject() As VectorObject
        Dim vo As New VectorObject

        vo.LineColor.Value = LineColor.Value
        vo.FillColor.Value = FillColor.Value
        vo.LineWidth = LineWidth

        vo.SetPath(MyPath)

        'Start an undo batch, as we're doing a couple of things here:
        Designer.StartUndoBatch()

        'Add the undo of the new object:
        Designer.AddUndo(New ObjectAddedUndo(vo))

        'Add the new vector object after me:
        AddAfter(vo)

        'Add the undo of my removal:
        Designer.AddUndo(New ObjectRemovedUndo(Me))

        'Remove me from my parent:
        Remove()

        'Close the undo batch:
        Designer.EndUndoBatch()

        Return vo
    End Function
#End Region

#Region "MyFillColor_PropertyChanged"
    ''' <summary>
    ''' Internally ensures that changes to the FillColor property are bubbled up the hierarchy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MyFillColor_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles MyFillColor.PropertyChanged
        OnPropertyChanged("FillColor")
    End Sub
#End Region

#Region "MyLineColor_PropertyChanged"
    ''' <summary>
    ''' Internally ensures that changes to the LineColor property are bubbled up the hierarchy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MyLineColor_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles MyLineColor.PropertyChanged
        OnPropertyChanged("LineColor")
    End Sub
#End Region

#Region "OnAnimate"
    Protected Overrides Sub OnAnimate(ac As AnimationContext)
        MyBase.OnAnimate(ac)

        RecreatePath()
    End Sub
#End Region

#Region "OnAfterLoad"
    Protected Overrides Sub OnAfterLoad()
        MyBase.OnAfterLoad()

        RecreatePath()
    End Sub
#End Region

#Region "OnClone"
    Protected Overrides Sub OnClone(clone As ArtificeObject)
        MyBase.OnClone(clone)

        clone.As(Of Text).Text = Text
        clone.As(Of Text).FontFamily = FontFamily
        clone.As(Of Text).Style = Style
    End Sub
#End Region

#Region "OnGetBounds"
    Protected Overrides Function OnGetBounds() As RectangleF
        If MyPath Is Nothing Then MyBase.OnGetBounds()

        Return MyPath.GetBounds()
    End Function
#End Region

#Region "OnHitTest"
    Protected Overrides Function OnHitTest(x As Single, y As Single, rc As RenderContext, ByRef transformedLocation As Point) As ArtificeMoveable
        If MyPath Is Nothing Then Return Nothing

        Dim lastPoint As New Point(transformedLocation)

        Dim points(1) As Point
        points(0) = New Point(x, y)

        rc.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, points)

        transformedLocation.X = points(0).X
        transformedLocation.Y = points(0).Y

        Dim p = New Pen(LineColor.Delta.Color, LineWidth.Delta)

        If MyPath.IsVisible(points(0)) OrElse MyPath.IsOutlineVisible(points(0), p) Then
            p.Dispose()
            Return Me
        Else
            p.Dispose()
        End If

        transformedLocation.X = lastPoint.X
        transformedLocation.Y = lastPoint.Y

        Return MyBase.OnHitTest(x, y, rc, transformedLocation)
    End Function
#End Region

#Region "OnLoad"
    Protected Overrides Sub OnLoad(el As XElement)
        MyBase.OnLoad(el)

        MyText = el.@Text
        MyFontFamily = New FontFamily(el.@FontFamily)
        MySize = New LerpableSingle(CSng(el.@Size))
        MyStyle = CInt(el.@Style)

        RecreatePath()
    End Sub
#End Region

#Region "OnRender"
    Protected Overrides Sub OnRender(rc As RenderContext)
        If MyPath Is Nothing Then Return

        'Adjust the opacity of our colours based on the current opacity stack:
        LineColor.Delta.Alpha = LineColor.Delta.Alpha * rc.OpacityStack.Peek
        FillColor.Delta.Alpha = FillColor.Delta.Alpha * rc.OpacityStack.Peek

        Dim p As New Pen(LineColor.Delta.Color, LineWidth.Delta)
        Dim br As New SolidBrush(FillColor.Delta.Color)

        rc.Graphics.FillPath(br, MyPath)
        rc.Graphics.DrawPath(p, MyPath)

        br.Dispose()
        p.Dispose()
    End Sub
#End Region

#Region "OnSave"
    Protected Overrides Sub OnSave(el As XElement)
        MyBase.OnSave(el)

        el.@Text = Text
        el.@FontFamily = FontFamily.Name
        el.@Size = Size.ToString()
        el.@Style = CInt(Style).ToString()
    End Sub
#End Region

#Region "OnSaveAsSVG"
    Protected Overrides Function OnSaveAsSVG() As XElement()
        Dim results As New List(Of XElement)
        Dim path As XElement = <text id=<%= "v" & Id.ToString() %>/>
        Dim bezierCount As Integer = 0

        path.@fill = FillColor.Delta.ToRGBString()

        If FillColor.Delta.Alpha <> 255 Then path.@<fill-opacity> = (FillColor.Delta.Alpha / 255).ToString()

        path.@stroke = LineColor.Delta.ToRGBString()

        If LineColor.Delta.Alpha <> 255 Then path.@<stroke-opacity> = (LineColor.Delta.Alpha / 255).ToString()

        path.@<stroke-width> = LineWidth.Delta.ToString()
        path.@<font-size> = Size.Delta.ToString()
        path.@<font-family> = MyFontFamily.Name
        path.@x = X.Delta.ToString()
        path.@y = Y.Delta.ToString()


        path.@transform = "translate(" & X.Delta.ToString & " " & Y.Delta.ToString & ") "

        If ScaleX.Delta <> 1.0 OrElse ScaleY.Delta <> 1.0 Then
            path.@transform &= "scale(" & ScaleX.Delta.ToString & " " & ScaleY.Delta.ToString() & ") "
        End If

        If Rotation.Delta <> 0.0 Then
            path.@transform &= "rotate(" & Rotation.Delta.ToString() & ")"
        End If

        path.Value = Text

        results.Add(path)

        Return results.ToArray()
    End Function
#End Region

#Region "RecreatePath"
    ''' <summary>
    ''' Internally creates the path object using the current text and values
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RecreatePath()
        Dim sf As New StringFormat()

        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        If MyPath IsNot Nothing Then MyPath.Dispose()

        MyPath = New GraphicsPath()

        MyPath.AddString(Text, FontFamily, CInt(Style), Size.Delta, New Point(0, 0), sf)
    End Sub
#End Region

End Class

Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Text

''' <summary>
''' A single vector graphic object object
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class VectorObject
    Inherits ArtificeTransformable

    Private MyPath As GraphicsPath = Nothing ' The internal GraphicsPath object that is used to render this VectorObject

#Region "Properties"

    Private WithEvents MyFillColor As New LerpableColor(Color.Blue)
    <Category("Appearance"), DisplayName("Fill Color"), Description("The color this object is filled with")> _
    Public ReadOnly Property FillColor As LerpableColor
        Get
            Return MyFillColor
        End Get
    End Property

    Private WithEvents MyLineColor As New LerpableColor(Color.Black)    
    <Category("Appearance"), DisplayName("Line Color"), Description("The color of the border surrounding this object")> _
    Public ReadOnly Property LineColor As LerpableColor
        Get
            Return MyLineColor
        End Get
    End Property

    Private MyLineWidth As New LerpableSingle(1.0)
    <Category("Appearance"), DisplayName("Line Width"), Description("The width of the border around this object")> _
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

    <Browsable(False)> _
    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.VectorObject
        End Get
    End Property

    ''' <summary>
    ''' The Path instance
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)> _
    Public ReadOnly Property Path As GraphicsPath
        Get
            Return MyPath
        End Get
    End Property

#End Region

#Region "BuildPath"
    ''' <summary>
    ''' Constructs the internal GraphicsPath from the child VectorPoints of this object
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BuildPath()
        Dim points As New List(Of PointF)
        Dim types As New List(Of Byte)

        For Each vp As VectorPoint In Children
            points.Add(New PointF(vp.X.Delta, vp.Y.Delta))
            types.Add(vp.PointType)
        Next

        If MyPath IsNot Nothing Then
            MyPath.Dispose()
            MyPath = Nothing
        End If

        If points.Count > 0 Then
            MyPath = New GraphicsPath(points.ToArray(), types.ToArray())
        End If
    End Sub
#End Region

#Region "MyFillColor_PropertyChanged"
    Private Sub MyFillColor_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles MyFillColor.PropertyChanged
        OnPropertyChanged("FillColor")
    End Sub
#End Region

#Region "MyLineColor_PropertyChanged"
    Private Sub MyLineColor_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles MyLineColor.PropertyChanged
        OnPropertyChanged("LineColor")
    End Sub
#End Region

#Region "OnAfterAnimate"
    Protected Overrides Sub OnAfterAnimate(ac As AnimationContext)
        MyBase.OnAfterAnimate(ac)

        BuildPath()
    End Sub
#End Region

#Region "OnAfterLoad"
    Protected Overrides Sub OnAfterLoad()
        BuildPath()
    End Sub
#End Region

#Region "OnChildRemoved"
    Protected Overrides Sub OnChildRemoved()
        BuildPath()
    End Sub
#End Region

#Region "OnHitTest"
    Protected Overrides Function OnHitTest(x As Single, y As Single, rc As RenderContext, ByRef transformedLocation As Point) As ArtificeMoveable
        If MyPath Is Nothing Then Return Nothing

        'Determine if the coordinates (when transformed to local space) are inside my path:

        Dim lastPoint As New Point(transformedLocation)

        Dim points(1) As Point
        points(0) = New Point(x, y)

        rc.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, points)

        transformedLocation.X = points(0).X
        transformedLocation.Y = points(0).Y

        If Designer.PointEditMode Then
            For Each vp As VectorPoint In Children
                If (New Rectangle(vp.X.Delta - 5, vp.Y.Delta - 5, 10, 10)).Contains(points(0)) Then Return vp
            Next
        End If

        Dim p = New Pen(LineColor.Delta.Color, LineWidth.Delta)

        If MyPath.IsVisible(points(0)) OrElse MyPath.IsOutlineVisible(points(0), p) Then
            p.Dispose()
            Return Me
        Else
            p.Dispose()
        End If

        transformedLocation.X = lastPoint.X
        transformedLocation.Y = lastPoint.Y

        Return Nothing ' MyBase.OnHitTest(x, y, rc, transformedLocation)
    End Function
#End Region

#Region "OnClone"
    Protected Overrides Sub OnClone(clone As ArtificeObject)
        MyBase.OnClone(clone)

        'Make my clone rebuild its path
        clone.As(Of VectorObject)().BuildPath()
    End Sub
#End Region

#Region "OnGetBounds"
    Protected Overrides Function OnGetBounds() As RectangleF
        If MyPath Is Nothing Then Return New RectangleF(-5, -5, 10, 10)

        Return MyPath.GetBounds()
    End Function
#End Region

#Region "OnRender"
    Protected Overrides Sub OnRender(rc As RenderContext)
        If MyPath Is Nothing Then Return

        'Adjust the opacity of our colours based on the current opacity stack:
        Dim oldLineColorAlpha As Integer = LineColor.Delta.Alpha
        Dim oldFillColorAlpha As Integer = FillColor.Delta.Alpha

        LineColor.Delta.Alpha = LineColor.Delta.Alpha * rc.OpacityStack.Peek
        FillColor.Delta.Alpha = FillColor.Delta.Alpha * rc.OpacityStack.Peek

        Dim p As New Pen(LineColor.Delta.Color, LineWidth.Delta)
        Dim br As New SolidBrush(FillColor.Delta.Color)

        rc.Graphics.FillPath(br, MyPath)
        rc.Graphics.DrawPath(p, MyPath)

        br.Dispose()
        p.Dispose()

        LineColor.Delta.Alpha = oldLineColorAlpha
        FillColor.Delta.Alpha = oldFillColorAlpha

        MyBase.OnRender(rc)
    End Sub
#End Region

#Region "OnSaveAsSVG"
    Protected Overrides Function OnSaveAsSVG() As XElement()
        Dim results As New List(Of XElement)
        Dim path As XElement = <path id=<%= "v" & Id.ToString() %>/>
        Dim d As New StringBuilder()
        Dim bezierCount As Integer = 0

        path.@fill = FillColor.Delta.ToRGBString()

        If FillColor.Delta.Alpha <> 255 Then path.@<fill-opacity> = (FillColor.Delta.Alpha / 255).ToString()

        path.@stroke = LineColor.Delta.ToRGBString()

        If LineColor.Delta.Alpha <> 255 Then path.@<stroke-opacity> = (LineColor.Delta.Alpha / 255).ToString()

        path.@<stroke-width> = LineWidth.Delta.ToString()

        path.@transform = "translate(" & X.Delta.ToString & " " & Y.Delta.ToString & ") "

        If ScaleX.Delta <> 1.0 OrElse ScaleY.Delta <> 1.0 Then
            path.@transform &= "scale(" & ScaleX.Delta.ToString & " " & ScaleY.Delta.ToString() & ") "
        End If

        If Rotation.Delta <> 0.0 Then
            path.@transform &= "rotate(" & Rotation.Delta.ToString() & ")"
        End If

        For p As Integer = 0 To Children.Count - 1
            Dim point As VectorPoint = Children(p).As(Of VectorPoint)()

            If p = 0 Then
                d.Append("M ")
            ElseIf (CType(point.PointType, PathPointType) And PathPointType.Bezier3) = PathPointType.Bezier3 Then
                If bezierCount = 0 Then
                    d.Append("C ")
                End If

                bezierCount += 1

                If bezierCount = 3 Then
                    bezierCount = 0
                End If
            ElseIf (CType(point.PointType, PathPointType) And PathPointType.Line) = PathPointType.Line Then
                d.Append("L ")
            End If

            d.Append(point.X.Delta.ToString())
            d.Append(" ")
            d.Append(point.Y.Delta.ToString())
            d.Append(" ")
        Next

        d.Append("L ")
        d.Append(Children(0).As(Of VectorPoint)().X.Delta.ToString())
        d.Append(" ")
        d.Append(Children(0).As(Of VectorPoint)().Y.Delta.ToString())

        path.@d = d.ToString()

        results.Add(path)

        Return results.ToArray()
    End Function
#End Region

#Region "SetPath"
    ''' <summary>
    ''' Sets the child VectorPoints of this object to the points of the given path:
    ''' </summary>
    ''' <param name="gp"></param>
    ''' <remarks></remarks>
    Public Sub SetPath(gp As GraphicsPath)
        ChildrenList.Clear()

        Dim bezierCount As Integer = 0

        For p As Integer = 0 To gp.PathPoints.Count - 1
            Dim vp As New VectorPoint()

            vp.X.Value = gp.PathPoints(p).X
            vp.Y.Value = gp.PathPoints(p).Y
            vp.PointType = gp.PathTypes(p)

            If vp.PointType = 3 Then
                'This logic just determines which points are control points, 
                'so we can quickly know how to draw them in PointEditMode:
                If p = 1 Then
                    bezierCount = 1
                    vp.ControlPoint = True
                ElseIf bezierCount = 1 Then
                    vp.ControlPoint = True
                ElseIf bezierCount = 2 Then
                    vp.ControlPoint = True
                ElseIf bezierCount = 3 Then
                    bezierCount = 0
                End If

                bezierCount += 1
            End If


            AddChild(vp)
        Next

        BuildPath()
    End Sub
#End Region

#Region "VectorObject_PropertyChanged"
    Private Sub VectorObject_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged
        'If any of my children have changed, rebuild my path:
        If sender.GetType() Is GetType(VectorPoint) Then
            BuildPath()
        End If
    End Sub
#End Region

End Class

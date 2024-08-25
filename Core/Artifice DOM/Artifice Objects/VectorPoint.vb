Imports System.ComponentModel

''' <summary>
''' A single point in a VectorObject
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class VectorPoint
    Inherits ArtificeMoveable

#Region "properties"
    <Browsable(False)> _
    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Point
        End Get
    End Property

    ''' <summary>
    ''' The GraphicsPath PointType value
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)> _
    Public Property PointType As Byte

    ''' <summary>
    ''' True if this point is a Bezier curve control point, False otherwise
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ControlPoint As Boolean = False
#End Region

#Region "OnAfterRender"
    Protected Overrides Sub OnAfterRender(rc As RenderContext)
        'Calculate and cache my transformed bounds:
        Dim points() As Point = New Point() {New Point(0, 0)}

        rc.Graphics.TransformPoints(Drawing2D.CoordinateSpace.Device, Drawing2D.CoordinateSpace.World, points)

        MyTransformedBounds = New RectangleF(points(0).X - 5, points(0).Y - 5, 10, 10)
    End Sub
#End Region

#Region "OnClone"
    Protected Overrides Sub OnClone(clone As ArtificeObject)
        MyBase.OnClone(clone)

        CType(clone, VectorPoint).PointType = PointType
    End Sub
#End Region

#Region "OnLoad"
    Protected Overrides Sub OnLoad(el As XElement)
        MyBase.OnLoad(el)

        PointType = Byte.Parse(el.@PointType)
    End Sub
#End Region

#Region "OnSave"
    Protected Overrides Sub OnSave(el As XElement)
        MyBase.OnSave(el)

        el.@PointType = PointType.ToString()
    End Sub
#End Region

End Class

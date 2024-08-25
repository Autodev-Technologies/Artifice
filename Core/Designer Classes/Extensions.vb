Imports ClipperLib
Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices

''' <summary>
''' Extenion methods for various classes
''' </summary>
''' <remarks>Created by Autodev</remarks>
Module Extensions

#Region "Public Class Polygon"
    Public Class Polygon
        Inherits List(Of IntPoint)

        Overloads Sub Add(x As Integer, y As Integer)
            MyBase.Add(New IntPoint(x, y))
        End Sub

        Public Function ToPointArray() As Point()
            Dim pta As New List(Of Point)

            For Each pt In Me
                pta.Add(New Point(pt.X, pt.Y))
            Next

            Return pta.ToArray()
        End Function

        Public Function GetBaseList() As List(Of IntPoint)
            Return Me
        End Function
    End Class
#End Region

#Region "Public Class Polygons"
    Public Class Polygons
        Inherits List(Of Polygon)

        Public Function BaseList() As List(Of List(Of IntPoint))
            Dim l As New List(Of List(Of IntPoint))

            l.AddRange((From p In Me Select p.GetBaseList))

            Return l
        End Function
    End Class
#End Region

#Region "AddPath"
    <Extension()> _
    Public Sub AddPath(cl As Clipper, path As GraphicsPath, polyType As ClipperLib.PolyType)
        Dim pathIterator As New GraphicsPathIterator(path)
        Dim polygon As New List(Of ClipperLib.IntPoint)
        Dim points(pathIterator.Count - 1) As PointF
        Dim types(pathIterator.Count - 1) As Byte

        path.Flatten()

        pathIterator.Enumerate(points, types)

        For Each pt In path.PathPoints()
            polygon.Add(New IntPoint(pt.X, pt.Y))
        Next

        cl.AddPolygon(polygon, polyType)
    End Sub
#End Region

#Region "Center"
    ''' <summary>
    ''' Returns the center point of the given rectangle
    ''' </summary>
    ''' <param name="r"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> _
    Public Function Center(r As Rectangle) As Point
        Return New Point(r.Left + r.Width / 2, r.Top + r.Height / 2)
    End Function

    ''' <summary>
    ''' Returns the center point of the given rectangle
    ''' </summary>
    ''' <param name="r"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> _
    Public Function Center(r As RectangleF) As PointF
        Return New PointF(r.Left + r.Width / 2, r.Top + r.Height / 2)
    End Function
#End Region

#Region "Combine"
    <Extension()> _
    Public Function Combine(gp1 As GraphicsPath, gp2 As GraphicsPath, method As ClipperLib.ClipType) As GraphicsPath
        Dim c As New Clipper()
        Dim solution As New List(Of List(Of IntPoint))
        Dim result As New GraphicsPath

        gp1.Flatten()
        gp2.Flatten()

        c.AddPolygons(gp1.ToPolygons().BaseList(), PolyType.ptSubject)
        c.AddPolygons(gp2.ToPolygons().BaseList(), PolyType.ptClip)


        If c.Execute(method, solution) Then
            For Each p In solution
                result.AddPolygon(ToPointArray(p))
            Next
        End If

        Return result
    End Function
#End Region

#Region "ToPointArray"
    Private Function ToPointArray(a As List(Of IntPoint)) As Point()
        Dim pta As New List(Of Point)

        For Each pt In a
            pta.Add(New Point(pt.X, pt.Y))
        Next

        Return pta.ToArray()
    End Function
#End Region

#Region "ToPolygons"
    <Extension()> _
    Public Function ToPolygons(gp1 As GraphicsPath) As Polygons
        gp1.Flatten()

        Dim iter As New GraphicsPathIterator(gp1)
        Dim p As New Polygons
        Dim points(iter.Count - 1) As PointF
        Dim types(iter.Count - 1) As Byte
        Dim i As Integer = 0

        iter.Rewind()

        iter.Enumerate(points, types)

        While i < iter.Count
            Dim pg As New Polygon

            p.Add(pg)

            Do
                Dim pt As New IntPoint(points(i).X, points(i).Y)
                pg.Add(pt)
                i += 1
            Loop While i < iter.Count AndAlso types(i) <> 0


        End While

        Return p
    End Function
#End Region

#Region "Union"
    ''' <summary>
    ''' Unions two GraphicsPaths together and returns a new GraphicsPath as a result
    ''' </summary>
    ''' <param name="dest"></param>
    ''' <param name="src"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> _
    Public Function Union(dest As GraphicsPath, src As GraphicsPath) As GraphicsPath
        Dim cl As New Clipper()
        Dim solution As New List(Of List(Of IntPoint))
        Dim result As New GraphicsPath

        cl.AddPath(dest, PolyType.ptSubject)
        cl.AddPath(src, PolyType.ptClip)

        If cl.Execute(ClipType.ctUnion, solution, PolyFillType.pftEvenOdd, PolyFillType.pftEvenOdd) Then

            For Each poly As List(Of IntPoint) In solution
                Dim points As New List(Of Point)

                For Each pt As IntPoint In poly
                    points.Add(New Point(pt.X, pt.Y))
                Next

                result.AddPolygon(points.ToArray())
            Next

        End If

        Return result
    End Function
#End Region

End Module

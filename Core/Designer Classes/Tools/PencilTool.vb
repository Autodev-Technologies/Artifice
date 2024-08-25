Imports System.Drawing.Drawing2D

''' <summary>
''' The class for drawing freehand lines in the designer
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class PencilTool
    Inherits ToolBase

    Private Adding As Boolean = False ' Tracks whether we are creating a new VectorObject, or unioning this ellipse to a pre-existing one
    Private Drawing As Boolean = False 'Whether or not we are currently drawing
    Private PreviousPoint As Point ' The last point that was drawn
    Private NewObject As VectorObject ' The VectorObject we are creating (or adding to)
    Private DrawingPath As GraphicsPath ' The path used to track our drawing
    Private OriginalPath As GraphicsPath ' The original GraphicsPath of the VectorObject we are adding to (if applicable)
    Private MyPoints As New List(Of Point)

#Region "Properties"
    Public Overrides ReadOnly Property Description As String
        Get
            Return "Draw freehand lines."
        End Get
    End Property

    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Pencil
        End Get
    End Property

    Public Overrides ReadOnly Property Index As Integer
        Get
            Return 5
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "Pencil"
        End Get
    End Property
#End Region

#Region "OnMouseDown"
    Public Overrides Sub OnMouseDown(e As ToolMouseEvent)
        If e.Mouse.Button = MouseButtons.Left Then
            Drawing = True
            Designer.PointEditMode = False
            PreviousPoint = New Point(0, 0)

            'Are we unioning to an existing object:
            If e.ControlKeyDown AndAlso Designer.SelectedObject.Is(Of VectorObject)() Then
                Adding = True
                OriginalPath = New GraphicsPath()
                OriginalPath.AddPath(Designer.SelectedObject.As(Of VectorObject).Path(), True)
                NewObject = Designer.SelectedObject
                PreviousPoint = New Point(e.Location.X - NewObject.X.Delta, e.Location.Y - NewObject.Y.Delta)
            Else
                Adding = False
                NewObject = CreateNewObject(e.Location)
            End If

            MyPoints.Add(PreviousPoint)

            NewObject.FillColor.Value = New ArtificeColor(Color.Transparent)

            DrawingPath = New GraphicsPath
        End If
    End Sub
#End Region

#Region "OnMouseMove"
    Public Overrides Sub OnMouseMove(e As ToolMouseEvent)
        If e.Mouse.Button = MouseButtons.Left Then
            Designer.Animating = True

            Dim thisPoint As New Point(e.Location.X - NewObject.X.Delta, e.Location.Y - NewObject.Y.Delta)

            MyPoints.Add(thisPoint)

            DrawingPath = New GraphicsPath()

            DrawingPath.AddCurve(MyPoints.ToArray())

            NewObject.SetPath(DrawingPath)

            e.Surface.Refresh()

            Return

            ''The below code is deprecated and was from when I originally
            ''built the drawing out of connected line segments from each of the points
            ''the mouse passed through when drawing. Ultimately
            ''I tried to write a smoonthing algorithm to reduce
            ''the points to a small set of bezier curves. It had
            ''half-decent performance, but then I discovered the
            ''Graphics.AddCurve method which did exactly what I wanted to do
            ''so I abandoned this attempt:

            ''If we're adding (unioning) to an existing object, translate the x, y coordinates
            ''relative to the coordiates of the existing object:
            'If Adding Then
            '    thisPoint.X -= NewObject.X.Delta
            '    thisPoint.Y -= NewObject.Y.Delta
            'End If

            ''Add the ellipse to our new path:
            'DrawingPath.AddCurve(New Point() {PreviousPoint, thisPoint})
            ''DrawingPath.AddLine(PreviousPoint, thisPoint)

            'Dim gp As New GraphicsPath()

            'gp.AddPath(DrawingPath, False)

            ''If we're adding then perform the union:
            'If Adding Then

            '    gp = gp.Union(OriginalPath)
            'End If

            ''Set the new object's path to the resulting GraphicsPath:
            'NewObject.SetPath(gp)

            'PreviousPoint = thisPoint

            'Designer.Animating = False

            'e.Surface.Refresh()
        End If
    End Sub
#End Region

#Region "OnMouseUp"
    Public Overrides Sub OnMouseUp(e As ToolMouseEvent)
        If Drawing Then
            Drawing = False

            Designer.PointEditMode = True
            Designer.SelectedTool = New ArrowTool
            Return

            ''The below code is deprecated and was from when I originally
            ''built the drawing out of connected line segments from each of the points
            ''the mouse passed through when drawing. Ultimately
            ''I tried to write a smoonthing algorithm to reduce
            ''the points to a small set of bezier curves. It had
            ''half-decent performance, but then I discovered the
            ''Graphics.AddCurve method which did exactly what I wanted to do
            ''so I abandoned this attempt:

            'Dim gp As New GraphicsPath()

            'gp.AddPath(DrawingPath, False)

            ''If we're adding then perform the union:
            'If Adding Then
            '    Return 'gp = gp.Union(OriginalPath)
            'End If


            ''TODO: Smoothing algorithm?
            'Dim firstPoint, lastPoint As PointF
            'Dim signX As Integer
            'Dim signY As Integer
            'Dim smoothPath As New GraphicsPath
            'Dim firstPointIndex As Integer = 0
            'Dim lastPointIndex As Integer = 0
            'Dim controlPoint1, controlPoint2 As PointF

            'If DrawingPath.PathPoints.Count() < 2 Then
            '    'Do nothing?
            '    smoothPath.AddPath(gp, False)
            'Else
            '    If gp.PathPoints.Count() = 2 Then
            '        firstPoint = DrawingPath.PathPoints.First
            '        lastPoint = DrawingPath.PathPoints.Last
            '    Else
            '        Dim resetSign As Boolean = True

            '        firstPoint = gp.PathPoints(0)

            '        For p As Integer = 1 To gp.PathPoints.Count - 1
            '            Dim currPoint As PointF = gp.PathPoints(p)

            '            If resetSign Then
            '                lastPoint = currPoint
            '                lastPointIndex = p

            '                signX = Math.Sign(lastPoint.X - firstPoint.X)
            '                signY = Math.Sign(lastPoint.Y - firstPoint.Y)

            '                resetSign = False
            '            ElseIf (Math.Abs(currPoint.X - lastPoint.X) > 3 AndAlso Math.Sign(currPoint.X - lastPoint.X) <> signX) _
            '                OrElse (Math.Abs(currPoint.Y - lastPoint.Y) > 3 AndAlso Math.Sign(currPoint.Y - lastPoint.Y) <> signY) Then

            '                If lastPointIndex > firstPointIndex + 1 Then
            '                    controlPoint1 = DrawingPath.PathPoints(firstPointIndex + (lastPointIndex - firstPointIndex) * 0.25)
            '                    controlPoint2 = DrawingPath.PathPoints(firstPointIndex + (lastPointIndex - firstPointIndex) * 0.75)
            '                Else
            '                    controlPoint1 = New PointF(firstPoint.X + (lastPoint.X - firstPoint.X) * 0.25, firstPoint.Y + (lastPoint.Y - firstPoint.Y) * 0.25)
            '                    controlPoint2 = New PointF(lastPoint.X - (lastPoint.X - firstPoint.X) * 0.25, lastPoint.Y - (lastPoint.Y - firstPoint.Y) * 0.25)
            '                End If

            '                'Add bezier:
            '                smoothPath.AddBezier(firstPoint, controlPoint1, controlPoint2, lastPoint)

            '                firstPointIndex = p - 1
            '                lastPointIndex = p
            '                firstPoint = lastPoint
            '                lastPoint = currPoint
            '                signX = Math.Sign(lastPoint.X - firstPoint.X)
            '                signY = Math.Sign(lastPoint.Y - firstPoint.Y)
            '            Else
            '                lastPoint = currPoint
            '                lastPointIndex = p
            '            End If
            '        Next
            '    End If

            '    If lastPointIndex > firstPointIndex + 1 Then
            '        Dim midPoint As PointF = gp.PathPoints(firstPointIndex + (lastPointIndex - firstPointIndex) \ 2)

            '        controlPoint1 = New PointF(firstPoint.X + (midPoint.X - firstPoint.X) * 0.25, firstPoint.Y + (midPoint.Y - firstPoint.Y) * 0.25)
            '        controlPoint2 = New PointF(lastPoint.X - (lastPoint.X - midPoint.X) * 0.25, lastPoint.Y - (lastPoint.Y - midPoint.Y) * 0.25)
            '    Else
            '        controlPoint1 = New PointF(firstPoint.X + (lastPoint.X - firstPoint.X) * 0.25, firstPoint.Y + (lastPoint.Y - firstPoint.Y) * 0.25)
            '        controlPoint2 = New PointF(lastPoint.X - (lastPoint.X - firstPoint.X) * 0.25, lastPoint.Y - (lastPoint.Y - firstPoint.Y) * 0.25)
            '    End If

            '    'Add bezier:
            '    smoothPath.AddBezier(firstPoint, controlPoint1, controlPoint2, lastPoint)
            'End If


            ''Set the new object's path to the resulting GraphicsPath:
            'NewObject.SetPath(smoothPath)


            'Drawing = False

            'Designer.PointEditMode = True
            'Designer.SelectedTool = New ArrowTool
        End If
    End Sub
#End Region

End Class

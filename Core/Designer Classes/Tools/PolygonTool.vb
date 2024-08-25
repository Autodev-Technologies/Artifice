Imports System.Drawing.Drawing2D

''' <summary>
''' The tool for adding polygon shapes to the surface
''' </summary>
''' <remarks></remarks>
Public Class PolygonTool
    Inherits ToolBase

    Private Adding As Boolean = False ' Tracks whether we are creating a new VectorObject, or unioning this ellipse to a pre-existing one
    Private Drawing As Boolean = False ' Tracks whether we're currently drawing the polygon yet or not
    Private PreviousPoint As Point
    Private NewObject As VectorObject
    Private DrawingPath As GraphicsPath
    Private OriginalPath As GraphicsPath

#Region "Properties"
    Public Overrides ReadOnly Property Description As String
        Get
            Return "Creates a polygon object. Click to place points, Shift+Click to close figure. Ctrl+Click to union to currently-selected shape."
        End Get
    End Property

    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Polygon
        End Get
    End Property

    Public Overrides ReadOnly Property Index As Integer
        Get
            Return 6
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "Polygon"
        End Get
    End Property
#End Region

#Region "OnKeyDown"
    Public Overrides Sub OnKeyDown(surface As SceneSurface, e As KeyEventArgs)
        'Cancel this polygon:
        If e.KeyCode = Keys.Escape Then
            Drawing = False

            If Adding Then
                'If we're adding to an existing object, just roll back the path:
                NewObject.SetPath(OriginalPath)
            Else
                'If it's a new object, it's already been added to the undo buffer, so we'll just undo it:
                Designer.Undo()
            End If

            'Clear the currently-selected object:
            Designer.SelectedObject = Nothing
        End If
    End Sub
#End Region

#Region "OnMouseDown"
    Public Overrides Sub OnMouseDown(e As ToolMouseEvent)
        If e.Mouse.Button = MouseButtons.Left Then
            'Setting Animating = True prevents controls from refreshing with every change of an object's property:
            Designer.Animating = True

            'Is this the first click 
            If Not Drawing Then
                Drawing = True
                PreviousPoint = New Point(0, 0)

                'If we're adding to the currently-selected VectorObject:
                If e.ControlKeyDown Then
                    Adding = True
                    OriginalPath = New GraphicsPath()
                    OriginalPath.AddPath(Designer.SelectedObject.As(Of VectorObject).Path(), True)
                    NewObject = Designer.SelectedObject
                Else
                    Adding = False
                    NewObject = CreateNewObject(e.Location)
                End If

                DrawingPath = New GraphicsPath

                Designer.PointEditMode = True

                e.Surface.Refresh()
                'Shift+click closes this figure"
            ElseIf e.ShiftKeyDown Then
                Designer.Animating = True

                'Dim thisPoint As New Point(e.Location.X - NewObject.X.Delta, e.Location.Y - NewObject.Y.Delta)
                Dim thisPoint As PointF = DrawingPath.PathPoints.First

                'Rather than use the AddPolygon function I'd like to allow the user to 
                'change the curviness of the line segments. Instead I'll add a bezier curve 
                'that works out to be a straight line.
                'I do this by making the control points each 25% the length
                'of the new line segement, and colinear.
                DrawingPath.AddBezier(PreviousPoint, _
                                         New Point(PreviousPoint.X + (thisPoint.X - PreviousPoint.X) * 0.25, PreviousPoint.Y + (thisPoint.Y - PreviousPoint.Y) * 0.25), _
                                         New Point(thisPoint.X + (PreviousPoint.X - thisPoint.X) * 0.25, thisPoint.Y + (PreviousPoint.Y - thisPoint.Y) * 0.25), _
                                         thisPoint)

                DrawingPath.CloseFigure()

                Dim gp As New GraphicsPath

                gp.AddPath(DrawingPath, True)

                gp.CloseAllFigures()

                If Adding Then
                    gp = gp.Union(OriginalPath)
                End If

                NewObject.SetPath(gp)

                If Adding Then
                    Designer.AddUndo(New VectorPathChangeUndo(NewObject, OriginalPath))
                End If

                Drawing = False

                Designer.Animating = False

                'Set the selected tool back to Arrow:
                Designer.SelectedTool = New ArrowTool()

                e.Surface.Refresh()
            Else
                Dim thisPoint As New Point(e.Location.X - NewObject.X.Delta, e.Location.Y - NewObject.Y.Delta)

                'Rather than use the AddPolygon function I'd like to allow the user to 
                'change the curviness of the line segments. Instead I'll add a bezier curve 
                'that works out to be a straight line.
                'I do this by make the control points each 25% the length
                'of the new line segement, and colinear.
                DrawingPath.AddBezier(PreviousPoint, _
                                      New Point(PreviousPoint.X + (thisPoint.X - PreviousPoint.X) * 0.25, PreviousPoint.Y + (thisPoint.Y - PreviousPoint.Y) * 0.25), _
                                      New Point(thisPoint.X + (PreviousPoint.X - thisPoint.X) * 0.25, thisPoint.Y + (PreviousPoint.Y - thisPoint.Y) * 0.25), _
                                      thisPoint)

                Dim gp As New GraphicsPath

                gp.AddPath(DrawingPath, True)

                gp.CloseAllFigures()

                If Adding Then
                    gp = gp.Union(OriginalPath)
                End If

                NewObject.SetPath(gp)

                PreviousPoint = thisPoint
            End If

            'Turn off the Animating flag:
            Designer.Animating = False

            'Make the surface refresh itself:
            e.Surface.Refresh()
        End If
    End Sub
#End Region

End Class

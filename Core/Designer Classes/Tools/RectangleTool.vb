Imports System.Drawing.Drawing2D

''' <summary>
''' The tool for creating rectangle-based vector objects
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class RectangleTool
    Inherits ToolBase

    Private Adding As Boolean = False ' Tracks whether we are creating a new VectorObject, or unioning this ellipse to a pre-existing one
    Private DragStart As Point ' The location of the new object
    Private Dragging As Boolean = False ' Whether or not we are currently drawing the rectangle
    Private NewObject As VectorObject ' The VectorObject we are creating (or adding to)
    Private OriginalPath As GraphicsPath ' The original GraphicsPath of the VectorObject we are adding to (if applicable)

#Region "Properties"
    Public Overrides ReadOnly Property Cursor As Cursor
        Get
            Return Cursors.Cross
        End Get
    End Property

    Public Overrides ReadOnly Property Description As String
        Get
            Return "Create a rectangle. Hold down Shift to create squares. Ctrl+Click to union to currently-selected shape."
        End Get
    End Property

    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Rectangle
        End Get
    End Property

    Public Overrides ReadOnly Property Index As Integer
        Get
            Return 2
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "Rectangle"
        End Get
    End Property
#End Region

#Region "OnMouseDown"
    Public Overrides Sub OnMouseDown(e As ToolMouseEvent)
        'Keep track of where we started dragging:
        DragStart = e.Location
        Dragging = True
        Designer.PointEditMode = False

        'Are we unioning to an existing object:
        If e.ControlKeyDown Then
            Adding = True
            OriginalPath = New GraphicsPath()
            OriginalPath.AddPath(Designer.SelectedObject.As(Of VectorObject).Path(), True)
            NewObject = Designer.SelectedObject
        Else
            Adding = False
            'Create a new VectorObject at this location:
            NewObject = CreateNewObject(e.Location)
        End If
    End Sub
#End Region

#Region "OnMouseMove"
    Public Overrides Sub OnMouseMove(e As ToolMouseEvent)
        If Dragging AndAlso DragStart.X <> e.Location.X AndAlso DragStart.Y <> e.Location.Y Then
            'Setting Animating = True prevents controls from refreshing with every change of an object's property:
            Designer.Animating = True

            Dim gp As New GraphicsPath
            Dim rw As Integer = Math.Abs(e.Location.X - DragStart.X)
            Dim rh As Integer = Math.Abs(e.Location.Y - DragStart.Y)

            'Are we drawing a square? If so then make the width & height equal to the maximum of either:
            If e.ShiftKeyDown Then
                rw = Math.Max(rw, rh)
                rh = Math.Max(rw, rh)
            End If

            'Create the bounds of the new object (centered on [0, 0]):
            Dim objectBounds As New Rectangle(-rw, -rh, rw * 2, rh * 2)

            'If we're adding (unioning) to an existing object, translate the x, y coordinates
            'relative to the coordiates of the existing object:
            If Adding Then
                objectBounds.X = DragStart.X - NewObject.X.Delta - rw
                objectBounds.Y = DragStart.Y - NewObject.Y.Delta - rh
            End If

            'Add the rectangle to our new path:
            gp.AddRectangle(objectBounds)

            gp.CloseAllFigures()

            'If we're adding then perform the union:
            If Adding Then
                gp = gp.Union(OriginalPath)
            End If

            'Set the new object's path to the resulting GraphicsPath:
            NewObject.SetPath(gp)

            'Animate the object at the current scene (so that the deltas are correct):
            NewObject.Animate(New AnimationContext(Designer.SelectedScene.Frame))

            'Turn off the Animating flag:
            Designer.Animating = False

            'Make the surface refresh itself:
            e.Surface.Refresh()
        End If
    End Sub
#End Region

#Region "OnMouseUp"
    Public Overrides Sub OnMouseUp(e As ToolMouseEvent)
        If Dragging Then
            Dragging = False

            'If we were unioning to an existing object, add the path change to the undo buffer:
            If Adding Then
                Designer.AddUndo(New VectorPathChangeUndo(NewObject, OriginalPath))
            End If

            Designer.SelectedObject = NewObject

            'Set the designer's tool to arrow:
            Designer.SelectedTool = New ArrowTool
        End If
    End Sub
#End Region

End Class

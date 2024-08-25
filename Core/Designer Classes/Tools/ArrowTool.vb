''' <summary>
''' The Arrow tool, which handles selecting and moving objects and points
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class ArrowTool
    Inherits ToolBase

    Private SelectionStart As Point ' The starting point for selection boxes
    Private FrameRect As Rectangle ' The bounds of the selection frame
    Private DraggingObjects As New List(Of ArtificeMoveable) ' The list of objects currently being moved
    Private DraggingOffsets As New List(Of Point) ' The list of offsets of the mouse coordinates from the actual locations of the objects being moved
    Private OriginalPoints As New List(Of PointF) ' The original locations of the objects being moved
    Private OriginalDeltas As New List(Of PointF) ' The original deltas of the locations of the objects being moved
    Private KeyDownCount As Integer = 0 ' Keeps track of how many times OnKeyDown has fired since the last OnKeyUp event

#Region "Properties"
    Public Overrides ReadOnly Property Description As String
        Get
            Return "Used to select and maniuplate objects. Double-click vector objects to toggle point-editing mode."
        End Get
    End Property

    Public Overrides ReadOnly Property Index As Integer
        Get
            Return 1
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "Select"
        End Get
    End Property
#End Region

#Region "DrawSelectionFrame"
    ''' <summary>
    ''' Draws a reversable frame over the given control
    ''' </summary>
    ''' <param name="c">The control to draw the frame on</param>
    ''' <remarks></remarks>
    Private Sub DrawSelectionFrame(c As Control)
        'We need to convert the local coordinates to screen coordinates for DrawReversibleFrame
        Dim newLoc As Point = c.PointToScreen(FrameRect.Location)
        Dim newRect As New Rectangle(newLoc, FrameRect.Size)
        ControlPaint.DrawReversibleFrame(newRect, Color.Black, FrameStyle.Dashed)
    End Sub
#End Region

#Region "OnKeyDown"
    Public Overrides Sub OnKeyDown(surface As SceneSurface, e As KeyEventArgs)

        'We only care about the arrow keys for moving the selected objects:
        If e.KeyCode = Keys.Up OrElse e.KeyCode = Keys.Down OrElse e.KeyCode = Keys.Left OrElse e.KeyCode = Keys.Right Then

            'OnKeyDown keeps firing so we need to keep track of whether it's fired or not:
            If KeyDownCount = 0 Then
                OriginalPoints.Clear()
                'Record the original locations of the selected objects:
                For Each vm As ArtificeMoveable In Designer.SelectedObjects
                    OriginalPoints.Add(New PointF(vm.X.Value, vm.Y.Value))
                Next
            End If

            'Add one to the number of times this event has fired:
            KeyDownCount += 1

            'Create a new animation context for the currently-viewed frame:
            Dim ac As New AnimationContext(Designer.SelectedScene.Frame)

            'Tell the designer we're animating to prevent every property update from causing control refreshes:
            Designer.Animating = True

            For Each vm As ArtificeMoveable In Designer.SelectedObjects
                'Adjust the location of the object based on which key is down:
                Select Case e.KeyCode
                    Case Keys.Up
                        vm.Y.Value -= 1.0

                    Case Keys.Down
                        vm.Y.Value += 1.0

                    Case Keys.Left
                        vm.X.Value -= 1.0

                    Case Keys.Right
                        vm.X.Value += 1.0
                End Select

                'Animate this object (this causes the Delta values to be properly adjusted to what they should be this frame):
                vm.Animate(ac)
            Next

            'Tell the designer we're done animating:
            Designer.Animating = False

            'Force a refresh of the surface:
            surface.Refresh()

            'Tell the SceneSurface control that we handled this key press and it shouldn't do anything else with it:
            e.Handled = True
        End If

    End Sub
#End Region

#Region "OnKeyUp"
    Public Overrides Sub OnKeyUp(surface As SceneSurface, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Up, Keys.Down, Keys.Left, Keys.Right
                KeyDownCount -= 1

                'Once we're done with keypresses we can record the final position in the undo buffer:
                If KeyDownCount = 0 Then
                    'Start a new undo batch:
                    Designer.StartUndoBatch()

                    Dim pointIndex As Integer = 0

                    For Each vm As ArtificeMoveable In Designer.SelectedObjects
                        'We only need to add an undo record if the new value is different from the original one:
                        If vm.X.Value <> OriginalPoints(pointIndex).X Then
                            Designer.AddUndo(New PropertyChangeUndo(vm.X, "Value", OriginalPoints(pointIndex).X, vm.X.Value))
                        End If

                        If vm.Y.Value <> OriginalPoints(pointIndex).Y Then
                            Designer.AddUndo(New PropertyChangeUndo(vm.Y, "Value", OriginalPoints(pointIndex).Y, vm.Y.Value))
                        End If

                        pointIndex += 1
                    Next

                    'Close our undo batch:
                    Designer.EndUndoBatch()

                    'Clear our list of original points:
                    OriginalPoints.Clear()
                End If
        End Select

    End Sub
#End Region

#Region "OnMouseDoubleClick"
    Public Overrides Sub OnMouseDoubleClick(e As ToolMouseEvent)

        If e.ActiveObject IsNot Nothing Then
            Designer.SelectedObject = e.ActiveObject

            'If we've double-clicked a VectorObject, toggle PointEditMode:
            If e.ActiveObject.Is(Of VectorObject)() Then
                Designer.PointEditMode = Not Designer.PointEditMode
                'If we've double-clicked a group, make it the SelectedGroup:
                'TODO: This is likely broken and there should probably be some sort of EditingGroupStack or EditingParentStack in the Designer class:
            ElseIf e.ActiveObject.Is(Of Group)() Then
                Designer.SelectedContainer = e.ActiveObject
                Designer.PointEditMode = False
                'If we've double-clicked on a subscene object, make that scene the currently-edited scene:
            ElseIf e.ActiveObject.Is(Of Subscene)() Then
                If e.ActiveObject.As(Of Subscene).Scene IsNot Nothing Then
                    Designer.SelectedScene = e.ActiveObject.As(Of Subscene).Scene
                Else
                    'TODO: popup scene selector here
                End If
            End If
        ElseIf Designer.SelectedContainer IsNot Nothing AndAlso Not Designer.SelectedContainer.Is(Of KeyFrame)() Then
            'If our container is not a keyframe (like a group), pop up one level
            Designer.SelectedContainer = Designer.SelectedContainer.Parent            
        End If

    End Sub
#End Region

#Region "OnMouseDown"
    Public Overrides Sub OnMouseDown(e As ToolMouseEvent)

        If e.ActiveObject IsNot Nothing AndAlso Not e.ShiftKeyDown Then
            'We handle VectorPoints differently (though we probably shouldn't have):
            If e.ActiveObject.Is(Of VectorPoint)() Then
                MainWindow.Designer.SelectedPoint = e.ActiveObject

                DraggingObjects.Add(e.ActiveObject)

                Dim g As Graphics = e.Surface.CreateGraphics()
                Dim rc As New RenderContext(Designer.SelectedScene.Frame, g)

                e.Surface.BeforeRender(rc)

                DraggingOffsets.Add(e.ObjectLocation)
                OriginalPoints.Add(New PointF(e.ActiveObject.X.Value, e.ActiveObject.Y.Value))
                OriginalDeltas.Add(New PointF(e.ActiveObject.X.Delta, e.ActiveObject.Y.Delta))

                g.Dispose()
            Else
                'If the active object isn't currently selected then make it the only selected object:
                If Not Designer.SelectedObjects.Contains(e.ActiveObject) Then MainWindow.Designer.SelectedObject = e.ActiveObject
            End If
        ElseIf e.ActiveObject IsNot Nothing AndAlso e.ShiftKeyDown Then
            'If shift key is down add the object to the current list of selected objects
            Designer.AddSelection(e.ActiveObject)
        ElseIf Not e.ShiftKeyDown Then
            'If there's no object under the mouse then deselect all:
            Designer.SelectedObject = Nothing
        End If

        'Are we clicking down on one of the already-selected objects?
        If Designer.SelectedObjects.Count > 0 AndAlso Designer.SelectedObjects.First.Is(Of ArtificeMoveable)() AndAlso e.ActiveObject IsNot Nothing AndAlso Not e.ActiveObject.Is(Of VectorPoint)() Then
            'Build the list of dragging objects, offets, and original location values/deltas:
            For Each vm As ArtificeMoveable In Designer.SelectedObjects
                DraggingObjects.Add(vm)
                DraggingOffsets.Add(New Point(e.Location.X - vm.X.Value, e.Location.Y - vm.Y.Value))
                OriginalPoints.Add(New PointF(vm.X.Value, vm.Y.Value))
                OriginalDeltas.Add(New PointF(vm.X.Delta, vm.Y.Delta))
            Next
        Else
            'We're drawing a selection box:
            SelectionStart = New Point(e.ControlLocation.X, e.ControlLocation.Y)
            FrameRect = New Rectangle(e.ControlLocation.X, e.ControlLocation.Y, 0, 0)
            DrawSelectionFrame(e.Surface)
        End If

    End Sub
#End Region

#Region "OnMouseMove"
    Public Overrides Sub OnMouseMove(e As ToolMouseEvent)
        'Are we currently moving any objects?
        If DraggingObjects.Any Then
            'Setting Animating = True prevents controls from refreshing with every change of an object's property:
            Designer.Animating = True

            Dim ac As New AnimationContext(Designer.SelectedScene.Frame)
            Dim g As Graphics = e.Surface.CreateGraphics()
            Dim rc As New RenderContext(Designer.SelectedScene.Frame, g)

            For i As Integer = 0 To DraggingObjects.Count - 1
                Dim vm As ArtificeMoveable = DraggingObjects(i)
                rc.PushGraphicsState()

                e.Surface.BeforeRender(rc)

                Dim parentLocation As Point = e.Location

                'If this is a child of a group (or a VectorPoint), we need to get the location
                'relative to the parent. We can't use e.Location because HitTest might not have returned
                'the same object if the mouse was moved off of what we're dragging:
                If vm.Parent.Is(Of ArtificeMoveable)() Then parentLocation = vm.Parent.Translate(e.ControlLocation, rc)

                vm.X.Value = parentLocation.X - DraggingOffsets(i).X
                vm.Y.Value = parentLocation.Y - DraggingOffsets(i).Y

                rc.PopGraphicsState()

                vm.Animate(ac)
            Next

            g.Dispose()

            'Turn off the Animating flag:
            Designer.Animating = False

            'Tell the surface to redraw itself:
            e.Surface.Refresh()
        ElseIf e.Mouse.Button = MouseButtons.Left Then
            'Determine the new bounds of our selection frame:
            Dim newFrameRect As New Rectangle(Math.Min(SelectionStart.X, e.ControlLocation.X), Math.Min(SelectionStart.Y, e.ControlLocation.Y), Math.Abs(e.ControlLocation.X - SelectionStart.X), Math.Abs(e.ControlLocation.Y - SelectionStart.Y))

            If Not FrameRect.Equals(newFrameRect) Then
                'Erase the previous selection frame:
                DrawSelectionFrame(e.Surface)

                'Set the selection frame bounds to the new bounds:
                FrameRect = newFrameRect

                If Designer.SelectedContainer IsNot Nothing Then
                    'Setting Animating = True prevents controls from refreshing with every change of an object's property:
                    Designer.Animating = True

                    'Go through everything in the currently-selected keyframe and see if their
                    'TransformedBounds intersects with our selection frame. If it does,
                    'add it to the selection collection, otherwise remove it.
                    For Each vo As ArtificeMoveable In Designer.SelectedContainer.Children
                        If vo.TransformedBounds.IntersectsWith(FrameRect) Then
                            If Not Designer.SelectedObjects.Contains(vo) Then
                                Designer.AddSelection(vo)
                            End If
                        Else
                            Designer.RemoveSelection(vo)
                        End If
                    Next

                    'Turn off the animation flag:
                    Designer.Animating = False

                    'Make the surface redraw itself:
                    e.Surface.Refresh()
                End If

                'Draw the new selection frame
                DrawSelectionFrame(e.Surface)
            End If
        End If
    End Sub
#End Region

#Region "OnMouseUp"
    Public Overrides Sub OnMouseUp(e As ToolMouseEvent)
        If DraggingObjects.Any() Then
            'Create an undo point for all objects:
            Designer.StartUndoBatch()

            For i As Integer = 0 To DraggingObjects.Count - 1
                Dim vm As ArtificeMoveable = DraggingObjects(i)

                'We only need to add an undo record if the new value is different from the original one:
                If OriginalPoints(i).X <> vm.X.Value Then Designer.AddUndo(New PropertyChangeUndo(vm.X, "Value", OriginalPoints(i).X, vm.X.Value))
                If OriginalPoints(i).Y <> vm.Y.Value Then Designer.AddUndo(New PropertyChangeUndo(vm.Y, "Value", OriginalPoints(i).Y, vm.Y.Value))

                If OriginalDeltas(i).X <> vm.X.Delta Then Designer.AddUndo(New PropertyChangeUndo(vm.X, "Delta", OriginalDeltas(i).X, vm.X.Delta))
                If OriginalDeltas(i).Y <> vm.Y.Delta Then Designer.AddUndo(New PropertyChangeUndo(vm.Y, "Delta", OriginalDeltas(i).Y, vm.Y.Delta))
            Next

            'Close our undo batch:
            Designer.EndUndoBatch()

            'Clear all our dragging variables
            DraggingObjects.Clear()
            DraggingOffsets.Clear()            
            OriginalDeltas.Clear()
            OriginalPoints.Clear()
        ElseIf e.Mouse.Button = MouseButtons.Left Then
            'If we've been dragging a selection frame, we need to draw it one last time to erase it:
            DrawSelectionFrame(e.Surface)
        End If
    End Sub
#End Region

End Class

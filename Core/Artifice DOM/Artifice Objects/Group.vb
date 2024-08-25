Imports System.Drawing.Drawing2D

''' <summary>
''' Represents a grouping of visual objects on a layer
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class Group
    Inherits ArtificeTransformable

#Region "Properties"
    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Group
        End Get
    End Property
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Creates a new instance of a Group. If this is at design time (vs. loading), the currently selected objects will be automatically added to this group.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        If Designer.SelectedObjects.Count > 1 Then
            'Get our list of new kids (exclude vector points because they break things:
            Dim newKids() As ArtificeObject = (From so In Designer.SelectedObjects Where Not so.Is(Of VectorPoint)() Order By so.ParentIndex).ToArray()
            Dim bounds As RectangleF = RectangleF.Empty

            Designer.Animating = True

            'We're going to be doing a lot here so let's prepare an undo batch:
            Designer.StartUndoBatch()

            'Add me to the children of my new kids' parent:
            newKids.First.Parent.AddChild(Me)

            'Record my adding to the undo batch:
            Designer.AddUndo(New ObjectAddedUndo(Me))

            Dim l, t, r, b As Single

            l = 0
            t = 0
            r = 0
            b = 0

            'Figure out my bounds in order to determine my actual coordinates:
            For Each kid As ArtificeMoveable In newKids
                Dim kidBounds As RectangleF = kid.GetBounds()

                If kid Is newKids.First Then
                    l = kid.X.Delta - kidBounds.Width() / 2
                    t = kid.Y.Delta - kidBounds.Height() / 2
                    r = kid.X.Delta + kidBounds.Width() / 2
                    b = kid.Y.Delta + kidBounds.Height() / 2
                Else
                    l = Math.Min(l, kid.X.Delta - kidBounds.Width() / 2)
                    t = Math.Min(t, kid.Y.Delta - kidBounds.Height() / 2)
                    r = Math.Max(r, kid.X.Delta + kidBounds.Width() / 2)
                    b = Math.Max(b, kid.Y.Delta + kidBounds.Height() / 2)
                End If
            Next

            bounds = RectangleF.FromLTRB(l, t, r, b)

            'Set my coordinates to the center of my bounds:
            X = New LerpableSingle(bounds.Left + bounds.Width() / 2)
            Y = New LerpableSingle(bounds.Top + bounds.Height() / 2)

            'Put each kid under me and change their coordinates to be relative to mine:
            For Each kid As ArtificeMoveable In newKids
                Designer.AddUndo(New ObjectRemovedUndo(kid))
                kid.Remove()

                Designer.AddUndo(New PropertyChangeUndo(kid.X, "Value", kid.X.Value, kid.X.Value - X.Value))
                Designer.AddUndo(New PropertyChangeUndo(kid.Y, "Value", kid.Y.Value, kid.Y.Value - Y.Value))

                kid.X = New LerpableSingle(kid.X.Value - X.Value)
                kid.Y = New LerpableSingle(kid.Y.Value - Y.Value)

                kid.X.Reset()
                kid.Y.Reset()

                AddChild(kid)

                Designer.AddUndo(New ObjectAddedUndo(kid))
            Next

            Designer.EndUndoBatch()

            Designer.Animating = False

            'Make me the currently-selected object:
            Designer.SetSelection(Me)
        End If
    End Sub
#End Region

#Region "OnGetBounds"
    Protected Overrides Function OnGetBounds() As RectangleF
        Dim bounds As RectangleF = RectangleF.Empty
        Dim x, y, r, b As Single

        x = 0
        y = 0
        r = 0
        b = 0

        'Find the outer maximum/minimum points from all my children:
        For Each kid As ArtificeMoveable In Children
            Dim kidBounds As RectangleF = kid.GetBounds()

            x = Math.Min(x, kid.X.Delta + kidBounds.Left)
            y = Math.Min(y, kid.Y.Delta + kidBounds.Top)
            r = Math.Max(r, kid.X.Delta + kidBounds.Right)
            b = Math.Max(b, kid.Y.Delta + kidBounds.Bottom)
        Next

        bounds = RectangleF.FromLTRB(x, y, r, b)

        Return bounds
    End Function
#End Region

#Region "OnHitTest"
    Protected Overrides Function OnHitTest(x As Single, y As Single, rc As RenderContext, ByRef transformedLocation As Point) As ArtificeMoveable        
        If Not Designer.SelectedContainer Is Me AndAlso Not IsAncestorOf(Designer.SelectedContainer) Then
            Dim lastPoint As New Point(transformedLocation)

            Dim points(1) As Point
            points(0) = New Point(x, y)

            rc.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, points)

            transformedLocation.X = points(0).X
            transformedLocation.Y = points(0).Y

            If GetBounds.Contains(points(0)) Then Return Me

            transformedLocation.X = lastPoint.X
            transformedLocation.Y = lastPoint.Y
        Else
            'Only allow my children to be selected when I'm the selected group:
            Return MyBase.OnHitTest(x, y, rc, transformedLocation)
        End If

        Return Nothing
    End Function
#End Region

#Region "OnRender"
    Protected Overrides Sub OnRender(rc As RenderContext)
        'If we're inside a group (Designer.SelectedGroup is not Nothing), 
        'then everything but my objects will be drawn at 50% opacity against a darkended background.
        'Here we clear the opacity stack and set it to just be 1.0 for my children, then reset it afterwards:
        If Designer.SelectedContainer Is Me Then
            Dim opacities(rc.OpacityStack.Count - 1) As Single

            rc.OpacityStack.CopyTo(opacities, 0)

            rc.OpacityStack.Clear()

            rc.OpacityStack.Push(1.0)

            MyBase.OnRender(rc)

            rc.OpacityStack.Clear()

            For Each op As Single In opacities
                rc.OpacityStack.Push(op)
            Next
        Else
            'Render as normal:
            MyBase.OnRender(rc)
        End If
    End Sub
#End Region

#Region "OnSaveAsSVG"
    Protected Overrides Function OnSaveAsSVG() As XElement()
        'Export as an SVG group:
        Dim g As XElement = <g id=<%= "g" & Id %>/>

        g.@transform = "translate(" & X.Delta.ToString & " " & Y.Delta.ToString & ") "

        If ScaleX.Delta <> 1.0 OrElse ScaleY.Delta <> 1.0 Then
            g.@transform &= "scale(" & ScaleX.Delta.ToString & " " & ScaleY.Delta.ToString() & ") "
        End If

        If Rotation.Delta <> 0.0 Then
            g.@transform &= "rotate(" & Rotation.Delta.ToString() & ")"
        End If

        g.Add(MyBase.OnSaveAsSVG())

        Return New XElement() {g}
    End Function
#End Region

#Region "UnGroup"
    ''' <summary>
    ''' Returns all the children of this group to this group's parent, and then removes the group
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UnGroup()
        Dim goodByeKids As New List(Of ArtificeObject)

        goodByeKids.AddRange(Children)

        Designer.Animating = True

        Designer.StartUndoBatch()

        For Each kid As ArtificeMoveable In goodByeKids
            Designer.AddUndo(New ObjectRemovedUndo(kid))
            kid.Remove()

            Designer.AddUndo(New PropertyChangeUndo(kid.X, "Value", kid.X.Value, kid.X.Value + X.Value))
            Designer.AddUndo(New PropertyChangeUndo(kid.Y, "Value", kid.Y.Value, kid.Y.Value + Y.Value))

            kid.X = New LerpableSingle(kid.X.Value + X.Value)
            kid.Y = New LerpableSingle(kid.Y.Value + Y.Value)

            kid.X.Reset()
            kid.Y.Reset()

            AddBefore(kid)
            Designer.AddUndo(New ObjectAddedUndo(kid))
        Next

        Remove()

        Designer.EndUndoBatch()

        Designer.Animating = False
    End Sub
#End Region

End Class

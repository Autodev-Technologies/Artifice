Imports System.Threading
Imports System.Drawing.Drawing2D

''' <summary>
''' The main form of the Artifice application
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class MainWindow

    Private playbackTimer As New Stopwatch()
    Private Running As Boolean = True

#Region "Properites"
    Public ReadOnly Property Designer As Designer
        Get
            Return Global.Artifice.Designer.Singleton
        End Get
    End Property
#End Region


#Region "AboutToolStripMenuItem_Click"
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim av As New AboutArtifice

        av.ShowDialog(Me)
    End Sub
#End Region

#Region "ActualSize100ToolStripMenuItem_Click"
    Private Sub ActualSize100ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualSize100ToolStripMenuItem.Click
        SceneSurface1.Zoom = 1.0
    End Sub
#End Region

#Region "AddKeyframeToolStripMenuItem_Click"
    Private Sub AddKeyframeToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If Designer.SelectedLayer Is Nothing OrElse Not Designer.SelectedLayer.Is(Of Layer)() Then Return

        If Designer.SelectedContainer IsNot Nothing AndAlso Not Designer.SelectedContainer.Is(Of KeyFrame)() Then
            If Designer.SelectedContainer.Is(Of KeyFrame)() AndAlso Designer.SelectedScene.Frame = Designer.SelectedContainer.As(Of KeyFrame)().Frame Then Return

            If Designer.SelectedContainer.GetAncestor(Of KeyFrame).Frame = Designer.SelectedScene.Frame Then Return
        End If

        Designer.SelectedContainer = Designer.SelectedLayer.As(Of Layer)().AddKeyFrame(Designer.SelectedScene.Frame)
    End Sub
#End Region

#Region "AlignBottomButton_Click"
    Private Sub AlignBottomButton_Click(sender As Object, e As EventArgs) Handles AlignBottomButton.Click
        Dim alignRect As RectangleF = Designer.GetSelectionBounds

        Designer.Animating = True

        Designer.StartUndoBatch()

        Dim topEdge As Single = Designer.SelectedObjects.First.As(Of ArtificeMoveable)().GetBounds.Bottom + Designer.SelectedObjects.First.As(Of ArtificeMoveable)().Y.Delta
        Dim ac As New AnimationContext(Designer.SelectedScene.Frame)

        For vmi As Integer = 1 To Designer.SelectedObjects.Count - 1
            Dim vm As ArtificeMoveable = Designer.SelectedObjects(vmi)

            Dim topDelta As Single = vm.GetBounds().Bottom + vm.Y.Delta - topEdge

            Designer.AddUndo(New PropertyChangeUndo(vm.Y, "Value", vm.Y.Value, vm.Y.Value - topDelta))

            vm.Y.Value -= topDelta


            vm.Animate(ac)
        Next

        Designer.EndUndoBatch()

        Designer.Animating = False
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "AlignHorizontalCenterButton_Click"
    Private Sub AlignHorizontalCenterButton_Click(sender As Object, e As EventArgs) Handles AlignHorizontalCenterButton.Click
        Dim alignRect As RectangleF = Designer.GetSelectionBounds

        Designer.Animating = True

        Designer.StartUndoBatch()

        Dim centerX As Single = Designer.SelectedObjects.First.As(Of ArtificeMoveable)().X.Delta + (Designer.SelectedObjects.First.As(Of ArtificeMoveable)().GetBounds.Left + Designer.SelectedObjects.First.As(Of ArtificeMoveable)().GetBounds.Width / 2)
        Dim ac As New AnimationContext(Designer.SelectedScene.Frame)

        For vmi As Integer = 1 To Designer.SelectedObjects.Count - 1
            Dim vm As ArtificeMoveable = Designer.SelectedObjects(vmi)

            Dim centerDelta As Single = vm.X.Delta + (vm.GetBounds().Left + vm.GetBounds().Width / 2) - centerX

            Designer.AddUndo(New PropertyChangeUndo(vm.X, "Value", vm.X.Value, vm.X.Value - centerDelta))

            vm.X.Value -= centerDelta


            vm.Animate(ac)
        Next

        Designer.EndUndoBatch()

        Designer.Animating = False
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "AlignLeftButton_Click"
    Private Sub AlignLeftButton_Click(sender As Object, e As EventArgs) Handles AlignLeftButton.Click
        Dim alignRect As RectangleF = Designer.GetSelectionBounds

        Designer.Animating = True

        Designer.StartUndoBatch()

        Dim leftEdge As Single = Designer.SelectedObjects.First.As(Of ArtificeMoveable)().GetBounds.Left + Designer.SelectedObjects.First.As(Of ArtificeMoveable)().X.Delta
        Dim ac As New AnimationContext(Designer.SelectedScene.Frame)

        For vmi As Integer = 1 To Designer.SelectedObjects.Count - 1
            Dim vm As ArtificeMoveable = Designer.SelectedObjects(vmi)

            Dim leftDelta As Single = vm.GetBounds().Left + vm.X.Delta - leftEdge

            Designer.AddUndo(New PropertyChangeUndo(vm.X, "Value", vm.X.Value, vm.X.Value - leftDelta))

            vm.X.Value -= leftDelta

            vm.Animate(ac)
        Next

        Designer.EndUndoBatch()

        Designer.Animating = False
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "AlignRightButton_Click"
    Private Sub AlignRightButton_Click(sender As Object, e As EventArgs) Handles AlignRightButton.Click
        Dim alignRect As RectangleF = Designer.GetSelectionBounds

        Designer.Animating = True

        Designer.StartUndoBatch()

        Dim rightEdge As Single = Designer.SelectedObjects.First.As(Of ArtificeMoveable)().GetBounds.Right + Designer.SelectedObjects.First.As(Of ArtificeMoveable)().X.Delta
        Dim ac As New AnimationContext(Designer.SelectedScene.Frame)

        For vmi As Integer = 1 To Designer.SelectedObjects.Count - 1
            Dim vm As ArtificeMoveable = Designer.SelectedObjects(vmi)

            Dim rightDelta As Single = vm.GetBounds().Right + vm.X.Delta - rightEdge

            Designer.AddUndo(New PropertyChangeUndo(vm.X, "Value", vm.X.Value, vm.X.Value - rightDelta))

            vm.X.Value -= rightDelta


            vm.Animate(ac)
        Next

        Designer.EndUndoBatch()

        Designer.Animating = False
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "AlignTopButton_Click"
    Private Sub AlignTopButton_Click(sender As Object, e As EventArgs) Handles AlignTopButton.Click
        Dim alignRect As RectangleF = Designer.GetSelectionBounds

        Designer.Animating = True

        Designer.StartUndoBatch()

        Dim topEdge As Single = Designer.SelectedObjects.First.As(Of ArtificeMoveable)().GetBounds.Top + Designer.SelectedObjects.First.As(Of ArtificeMoveable)().Y.Delta
        Dim ac As New AnimationContext(Designer.SelectedScene.Frame)

        For vmi As Integer = 1 To Designer.SelectedObjects.Count - 1
            Dim vm As ArtificeMoveable = Designer.SelectedObjects(vmi)

            Dim topDelta As Single = vm.GetBounds().Top + vm.Y.Delta - topEdge

            Designer.AddUndo(New PropertyChangeUndo(vm.Y, "Value", vm.Y.Value, vm.Y.Value - topDelta))

            vm.Y.Value -= topDelta


            vm.Animate(ac)
        Next

        Designer.EndUndoBatch()

        Designer.Animating = False
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "AlignVerticalCenterButton_Click"
    Private Sub AlignVerticalCenterButton_Click(sender As Object, e As EventArgs) Handles AlignVerticalCenterButton.Click
        Dim alignRect As RectangleF = Designer.GetSelectionBounds

        Designer.Animating = True

        Designer.StartUndoBatch()

        Dim centerY As Single = Designer.SelectedObjects.First.As(Of ArtificeMoveable)().Y.Delta + (Designer.SelectedObjects.First.As(Of ArtificeMoveable)().GetBounds().Top + Designer.SelectedObjects.First.As(Of ArtificeMoveable)().GetBounds().Height / 2)
        Dim ac As New AnimationContext(Designer.SelectedScene.Frame)

        For vmi As Integer = 1 To Designer.SelectedObjects.Count - 1
            Dim vm As ArtificeMoveable = Designer.SelectedObjects(vmi)

            Dim centerDelta As Single = vm.Y.Delta + (vm.GetBounds.Top() + vm.GetBounds().Height / 2) - centerY

            Designer.AddUndo(New PropertyChangeUndo(vm.Y, "Value", vm.Y.Value, vm.Y.Value - centerDelta))

            vm.Y.Value -= centerDelta


            vm.Animate(ac)
        Next

        Designer.EndUndoBatch()

        Designer.Animating = False
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "BringForwardToolStripMenuItem_Click"
    Private Sub BringForwardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BringForwardToolStripMenuItem.Click
        Designer.Animating = True
        Designer.StartUndoBatch()
        For Each vo As ArtificeObject In Designer.SelectedObjects
            Designer.AddUndo(New ObjectMoveUndo(vo, vo.ParentIndex + 1))
            vo.BringForward()
        Next
        Designer.EndUndoBatch()
        Designer.Animating = False
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "BringToFrontToolStripMenuItem_Click"
    Private Sub BringToFrontToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BringToFrontToolStripMenuItem.Click
        Designer.Animating = True
        Designer.StartUndoBatch()
        For Each vo As ArtificeObject In Designer.SelectedObjects
            Designer.AddUndo(New ObjectMoveUndo(vo, vo.Parent.Children.Count - 1))
            vo.BringToFront()
        Next
        Designer.EndUndoBatch()
        Designer.Animating = False
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "CenterStageHorizontalButton_Click"
    Private Sub CenterStageHorizontalButton_Click(sender As Object, e As EventArgs) Handles CenterStageHorizontalButton.Click
        Dim center As PointF = Designer.GetSelectionBounds.Center
        Dim ac As New AnimationContext(Designer.SelectedScene.Frame)

        Designer.Animating = True

        Designer.StartUndoBatch()

        For Each vm As ArtificeMoveable In Designer.SelectedObjects
            If vm.X.Value - center.X <> vm.X.Value Then
                Designer.AddUndo(New PropertyChangeUndo(vm.X, "Value", vm.X.Value, vm.X.Value - center.X))
                vm.X.Value -= center.X

                vm.Animate(ac)
            End If
        Next

        Designer.EndUndoBatch()

        Designer.Animating = False
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "CenterStageVerticalButton_Click"
    Private Sub CenterStageVerticalButton_Click(sender As Object, e As EventArgs) Handles CenterStageVerticalButton.Click
        Dim center As PointF = Designer.GetSelectionBounds.Center
        Dim ac As New AnimationContext(Designer.SelectedScene.Frame)

        Designer.Animating = True

        Designer.StartUndoBatch()

        For Each vm As ArtificeMoveable In Designer.SelectedObjects
            If vm.Y.Value - center.Y <> vm.X.Value Then
                Designer.AddUndo(New PropertyChangeUndo(vm.Y, "Value", vm.Y.Value, vm.Y.Value - center.Y))
                vm.Y.Value -= center.Y

                vm.Animate(ac)
            End If
        Next

        Designer.EndUndoBatch()

        Designer.Animating = False
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "ClearKeyframeToolStripMenuItem_Click"
    Private Sub ClearKeyframeToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If Designer.SelectedLayer Is Nothing OrElse Designer.SelectedContainer Is Nothing Then Return

        Dim kf As KeyFrame

        If Designer.SelectedContainer.Is(Of KeyFrame)() Then
            kf = Designer.SelectedContainer
        Else
            kf = Designer.SelectedContainer.GetAncestor(Of KeyFrame)()
        End If

        'Don't clear the keyframe unless we're explicitly at that frame:
        If Designer.SelectedScene.Frame <> kf.Frame Then Return

        kf.Remove()

        Designer.SelectedContainer = Designer.SelectedLayer.As(Of Layer)().GetKeyFrame(Designer.SelectedScene.Frame)
    End Sub
#End Region

#Region "CodeProjectArticleAboutArtificeToolStripMenuItem_Click"
    Private Sub CodeProjectArticleAboutArtificeToolStripMenuItem_Click(sender As Object, e As EventArgs)
    End Sub
#End Region

#Region "ConfigureUI"
    ''' <summary>
    ''' Configures all unbound toolbar buttons, menu items, etc. when anything changes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigureUI()
        'Don't bother reacting while we're playing; it just slows down animation:
        If Designer.Playing Then Return

        RevertToolStripMenuItem.Enabled = Designer.Project IsNot Nothing AndAlso Designer.Project.Dirty AndAlso Designer.Project.FileName <> ""

        SaveToolStripMenuItem.Enabled = Designer.Project IsNot Nothing
        SaveasToolStripMenuItem.Enabled = Designer.Project IsNot Nothing

        CutToolStripMenuItem.Enabled = Designer.SelectedObjects.Any
        CopyToolStripMenuItem.Enabled = Designer.SelectedObjects.Any
        DeleteLayerToolStripMenuItem.Enabled = Designer.SelectedObjects.Any

        Try
            PasteToolStripMenuItem.Enabled = False

            If Clipboard.ContainsText() Then
                If Clipboard.GetText().StartsWith("<ArtificeObjects>") Then
                    PasteToolStripMenuItem.Enabled = True
                End If
            End If
        Catch ex As Exception
            'Do nothing; invalid data
        End Try

        DuplicateToolStripMenuItem.Enabled = Designer.SelectedObjects.Any
        DeleteToolStripMenuItem.Enabled = Designer.SelectedObjects.Any
        SelectallToolStripMenuItem.Enabled = Designer.SelectedContainer IsNot Nothing AndAlso Designer.SelectedContainer.Children.Any

        NewLayer.Enabled = Designer.SelectedScene IsNot Nothing
        NewFolder.Enabled = Designer.SelectedScene IsNot Nothing
        RenameLayerToolStripMenuItem.Enabled = Designer.SelectedLayer IsNot Nothing
        DeleteLayer.Enabled = Designer.SelectedLayer IsNot Nothing
        DeleteSceneToolStripMenuItem.Enabled = Designer.SelectedScene IsNot Nothing

        PlayAudioToolStripMenuItem.Checked = Designer.MuteAudio
        HighQualityRenderToolStripMenuItem.Checked = Designer.HighQualityRender

        AddLayerToolStripMenuItem.Enabled = NewLayer.Enabled
        AddFolderToolStripMenuItem.Enabled = NewFolder.Enabled
        DeleteLayerToolStripMenuItem.Enabled = DeleteLayer.Enabled

        FlipHorizontalToolStripMenuItem.Enabled = Designer.SelectedObjects.Count = 1 AndAlso Designer.SelectedObjects.First.Is(Of ArtificeMoveable)()
        FlipVerticalToolStripMenuItem.Enabled = Designer.SelectedObjects.Count = 1 AndAlso Designer.SelectedObjects.First.Is(Of ArtificeMoveable)()

        NewSceneToolStripMenuItem.Enabled = Designer.Project IsNot Nothing

        CenterStageHorizontalButton.Enabled = Designer.SelectedObjects.Any AndAlso Designer.SelectedObjects.First.Is(Of ArtificeMoveable)()
        CenterStageVerticalButton.Enabled = Designer.SelectedObjects.Any AndAlso Designer.SelectedObjects.First.Is(Of ArtificeMoveable)()
        AlignLeftButton.Enabled = Designer.SelectedObjects.Count > 1
        AlignRightButton.Enabled = Designer.SelectedObjects.Count > 1
        AlignHorizontalCenterButton.Enabled = Designer.SelectedObjects.Count > 1

        AlignTopButton.Enabled = Designer.SelectedObjects.Count > 1
        AlignBottomButton.Enabled = Designer.SelectedObjects.Count > 1
        AlignVerticalCenterButton.Enabled = Designer.SelectedObjects.Count > 1

        ObjectToolStripMenuItem.Visible = Designer.SelectedObjects.Any AndAlso Designer.SelectedObjects.First.Is(Of ArtificeMoveable)()

        CreateGroupToolStripMenuItem.Enabled = Designer.SelectedObjects.Count > 1
        UngroupToolStripMenuItem.Enabled = Designer.SelectedObjects.Count = 1 AndAlso Designer.SelectedObject.Is(Of Group)()

        BringToFrontToolStripMenuItem.Enabled = Designer.SelectedObjects.Any
        BringForwardToolStripMenuItem.Enabled = Designer.SelectedObjects.Any
        SendBackwardToolStripMenuItem.Enabled = Designer.SelectedObjects.Any
        SendToBackToolStripMenuItem.Enabled = Designer.SelectedObjects.Any

        CopyToNextKeyframeToolStripMenuItem.Enabled = Designer.SelectedObjects.Any AndAlso Designer.SelectedContainer IsNot Nothing AndAlso Designer.SelectedContainer.GetClosest(Of KeyFrame)().Next IsNot Nothing
        CopyToPreviousKeyframeToolStripMenuItem.Enabled = Designer.SelectedObjects.Any AndAlso Designer.SelectedContainer IsNot Nothing AndAlso Designer.SelectedContainer.GetClosest(Of KeyFrame)().Previous IsNot Nothing

        SceneList.Enabled = Designer.Project IsNot Nothing
        NewSceneButton.Enabled = Designer.Project IsNot Nothing

        For Each item As ToolStripItem In DesignerTools.Items
            If item.Tag IsNot Nothing Then
                CType(item, ToolStripButton).Enabled = Designer.Project IsNot Nothing
            End If
        Next

        ConvertToVectorObjectToolStripMenuItem.Visible = Designer.SelectedObjects.Count = 1 AndAlso Designer.SelectedObject.Is(Of Text)()

        AddEffectButton.Enabled = Designer.SelectedObjects.Count = 1
        RemoveEffectButton.Enabled = EffectList.SelectedItems.Count > 0

        ExportToolStripMenuItem.Enabled = Designer.SelectedScene IsNot Nothing

        UndoToolStripMenuItem.Enabled = Designer.CanUndo
        RedoToolStripMenuItem.Enabled = Designer.CanRedo
    End Sub
#End Region

#Region "ConstructEffectsList"
    ''' <summary>
    ''' Builds the EffectsList listview based on the currently selected object
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConstructEffectsList()
        If Properties.SelectedObject IsNot Nothing AndAlso Properties.SelectedObject.GetType().IsSubclassOf(GetType(EffectBase)) Then Return

        EffectList.BeginUpdate()

        EffectList.Items.Clear()

        If Properties.SelectedObjects.Count = 1 Then
            'Add the effects (if any) to the effects list:
            For Each e As EffectBase In CType(Properties.SelectedObject, ArtificeObject).Effects
                Dim li As New ListViewItem(e.GetType().Name)

                li.Tag = e

                If Not ProjectTreeImages.Images.ContainsKey(e.GetType().ToString()) Then
                    ProjectTreeImages.Images.Add(e.GetType().ToString(), e.Icon)
                End If

                li.ImageKey = e.GetType().ToString()

                EffectList.Items.Add(li)
            Next
        End If

        EffectList.EndUpdate()
    End Sub
#End Region

#Region "ConstructTree"
    ''' <summary>
    ''' Builds the project path tree as well as any effects in the effects list
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConstructTree()
        ProjectTree.BeginUpdate()

        ProjectTree.Nodes.Clear()

        If Designer.SelectedObjects.Count = 1 Then
            Dim curr As ArtificeObject = Designer.SelectedObject
            Dim lastNode As TreeNode = Nothing
            Dim selected As TreeNode = Nothing

            While curr IsNot Nothing
                Dim n As New TreeNode(curr.ToString())

                n.Tag = curr

                'Image stuff:
                If Not ProjectTreeImages.Images.ContainsKey(curr.GetType().ToString()) Then
                    ProjectTreeImages.Images.Add(curr.GetType().ToString(), curr.Icon)
                End If

                n.ImageKey = curr.GetType().ToString()
                n.SelectedImageKey = curr.GetType().ToString()

                If lastNode IsNot Nothing Then
                    n.Nodes.Add(lastNode)
                Else
                    selected = n
                End If

                lastNode = n

                curr = curr.Parent
            End While

            ProjectTree.Nodes.Add(lastNode)
            ProjectTree.ExpandAll()
            ProjectTree.SelectedNode = selected
        End If

        ProjectTree.EndUpdate()
    End Sub
#End Region

#Region "ConvertToVectorObjectToolStripMenuItem_Click"
    Private Sub ConvertToVectorObjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConvertToVectorObjectToolStripMenuItem.Click
        If Designer.SelectedObject Is Nothing OrElse Not Designer.SelectedObject.Is(Of Text)() Then Return

        Designer.SelectedObject = Designer.SelectedObject.As(Of Text)().ConvertToVectorObject()
    End Sub
#End Region

#Region "CopyToNextKeyframeToolStripMenuItem_Click"
    Private Sub CopyToNextKeyframeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToNextKeyframeToolStripMenuItem.Click
        Dim kf As KeyFrame = Designer.SelectedContainer.GetClosest(Of KeyFrame)().Next

        If kf Is Nothing Then Return

        For Each vo As ArtificeObject In Designer.SelectedObjects

            Dim copy As ArtificeObject = vo.Clone()

            Dim kfCopy As ArtificeObject = kf.GetChildById(vo.Id)

            If kfCopy Is Nothing Then
                kf.AddChild(copy)
                Designer.AddUndo(New ObjectAddedUndo(copy))
            Else
                If MsgBox("This object exists in the next keyframe. If you continue any changes to this object in the next keyframe will be lost and replaced with this object. Are you sure you want to continue?", MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation, "Copy to Next Keyframe") = MsgBoxResult.No Then
                    Return
                End If

                Designer.StartUndoBatch()

                kfCopy.AddBefore(copy)

                Designer.AddUndo(New ObjectAddedUndo(copy))
                Designer.AddUndo(New ObjectRemovedUndo(kfCopy))

                kfCopy.Remove()

                Designer.EndUndoBatch()
            End If
        Next

        Designer.SelectedScene.Frame = kf.Frame
        Designer.SelectedContainer = kf
    End Sub
#End Region

#Region "CopyToPreviousKeyframeToolStripMenuItem_Click"
    Private Sub CopyToPreviousKeyframeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToPreviousKeyframeToolStripMenuItem.Click
        Dim kf As KeyFrame = Designer.SelectedContainer.GetClosest(Of KeyFrame)().Previous

        If kf Is Nothing Then Return

        For Each vo As ArtificeObject In Designer.SelectedObjects

            Dim copy As ArtificeObject = vo.Clone()

            Dim kfCopy As ArtificeObject = kf.GetChildById(vo.Id)

            If kfCopy Is Nothing Then
                kf.AddChild(copy)
                Designer.AddUndo(New ObjectAddedUndo(copy))
            Else
                If MsgBox("This object exists in the previous keyframe. If you continue any changes to this object in the previous keyframe will be lost and replaced with this object. Are you sure you want to continue?", MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation, "Copy to Next Keyframe") = MsgBoxResult.No Then
                    Return
                End If

                Designer.StartUndoBatch()

                kfCopy.AddBefore(copy)

                Designer.AddUndo(New ObjectAddedUndo(copy))
                Designer.AddUndo(New ObjectRemovedUndo(kfCopy))

                kfCopy.Remove()

                Designer.EndUndoBatch()
            End If
        Next

        Designer.SelectedScene.Frame = kf.Frame
        Designer.SelectedContainer = kf
    End Sub
#End Region

#Region "CopySelection"
    ''' <summary>
    ''' Copies the currently-selected objects to the clipboard
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CopySelection()
        If Designer.SelectedObjects Is Nothing Then Return

        Dim selection As XElement = <ArtificeObjects/>

        For Each vo As ArtificeObject In Designer.SelectedObjects
            selection.Add(vo.ToXElement())
        Next

        Clipboard.SetText(selection.ToString())
    End Sub
#End Region

#Region "CopyToolStripMenuItem_Click"
    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        CopySelection()
    End Sub
#End Region

#Region "CreateGroupToolStripMenuItem_Click"
    Private Sub CreateGroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateGroupToolStripMenuItem.Click
        'We turn point edit mode off so no selected points get included in the group:
        Designer.PointEditMode = False

        'The Group constructor has all the logic in it:
        Dim g As New Group()
    End Sub
#End Region

#Region "CutToolStripMenuItem_Click"
    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        CopySelection()
        DeleteSelection()
    End Sub
#End Region

#Region "DeleteLayer_Click"
    Private Sub DeleteLayer_Click(sender As Object, e As EventArgs) Handles DeleteLayer.Click
        If Designer.SelectedLayer Is Nothing Then Return

        If MsgBox("Are you sure you want to delete this layer?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return

        Designer.AddUndo(New ObjectRemovedUndo(Designer.SelectedLayer))

        Designer.SelectedLayer.Remove()
        Designer.SelectedLayer = Nothing
        Designer.SelectedObject = Nothing
    End Sub
#End Region

#Region "DeleteObjectToolStripMenuItem_Click"
    Private Sub DeleteObjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        DeleteSelection()
    End Sub
#End Region

#Region "DeleteSceneToolStripMenuItem_Click"
    Private Sub DeleteSceneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteSceneToolStripMenuItem.Click
        If Designer.SelectedScene Is Nothing Then Return

        If Designer.Project.Children.Count = 1 Then
            MsgBox("You cannot remove the only scene in this project.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Exclamation, "Remove Scene")
            Return
        End If

        If MsgBox("Are you sure you want to delete scene " & Designer.SelectedScene.Name & "?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Remove Scene") = MsgBoxResult.No Then Return

        Designer.AddUndo(New ObjectRemovedUndo(Designer.SelectedScene))
        Designer.SelectedScene.Remove()

        Designer.SelectedScene = Designer.Project.Children.First
    End Sub
#End Region

#Region "DeleteSelection"
    Private Sub DeleteSelection()
        Designer.Animating = True

        If Designer.PointEditMode AndAlso Designer.SelectedPoint IsNot Nothing Then
            If Designer.SelectedObject.Is(Of RasterImage)() Then
                MsgBox("You cannot remove the anchor points for a Raster Image", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Artifice")
                Return
            End If

            If Designer.SelectedPoint.ControlPoint Then
                MsgBox("You cannot delete a control point of a curve", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Artifice")
                Return
            End If

            If (From c As VectorPoint In Designer.SelectedPoint.Parent.Children Where Not c.ControlPoint).Count <= 2 Then
                MsgBox("You cannot delete a point if only two remain", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Artifice")
                Return
            End If

            'If this is a bezier curve point then we have to remove the surrounding control points or the graphics path won't be valid:
            If Designer.SelectedPoint.PointType = 0 AndAlso Designer.SelectedPoint.Parent.Children(Designer.SelectedPoint.ParentIndex + 1).As(Of VectorPoint).PointType = 3 Then
                Designer.StartUndoBatch()

                Dim newStart As VectorPoint = Designer.SelectedPoint.Parent.Children(3)

                Designer.AddUndo(New ObjectRemovedUndo(Designer.SelectedPoint.Parent.Children(2)))
                Designer.SelectedPoint.Parent.Children(2).Remove()

                Designer.AddUndo(New ObjectRemovedUndo(Designer.SelectedPoint.Parent.Children(1)))
                Designer.SelectedPoint.Parent.Children(1).Remove()

                Designer.AddUndo(New ObjectRemovedUndo(Designer.SelectedPoint.Parent.Children(0)))
                Designer.SelectedPoint.Parent.Children(0).Remove()

                Designer.EndUndoBatch()
            ElseIf (Designer.SelectedPoint.PointType And 3) = 3 Then
                Dim index As Integer = Designer.SelectedPoint.ParentIndex

                Designer.StartUndoBatch()

                Dim removeTwoBack As Boolean = False

                If index + 1 < Designer.SelectedPoint.Parent.Children.Count Then
                    Designer.AddUndo(New ObjectRemovedUndo(Designer.SelectedPoint.Parent.Children(index + 1)))
                    Designer.SelectedPoint.Parent.Children(index + 1).Remove()
                Else
                    removeTwoBack = True
                End If

                Designer.AddUndo(New ObjectRemovedUndo(Designer.SelectedPoint.Parent.Children(index - 1)))
                Designer.SelectedPoint.Parent.Children(index - 1).Remove()

                If removeTwoBack Then
                    Designer.AddUndo(New ObjectRemovedUndo(Designer.SelectedPoint.Parent.Children(index - 2)))
                    Designer.SelectedPoint.Parent.Children(index - 2).Remove()
                End If

                Designer.AddUndo(New ObjectRemovedUndo(Designer.SelectedPoint))

                Designer.SelectedPoint.Remove()

                Designer.EndUndoBatch()
            Else
                Designer.AddUndo(New ObjectRemovedUndo(Designer.SelectedPoint))

                Designer.SelectedPoint.Remove()
            End If

            Designer.SelectedPoint = Nothing
        Else
            Designer.StartUndoBatch()

            For Each vo As ArtificeObject In Designer.SelectedObjects
                Designer.AddUndo(New ObjectRemovedUndo(vo))
                vo.Remove()
            Next

            Designer.EndUndoBatch()

            Designer.SelectedObject = Nothing
        End If

        Designer.Animating = False

        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "DesignerPropertyChanged"
    ''' <summary>
    ''' The handler of all change events that occur in the Designer and the Artifice DOM
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DesignerPropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs)

        'PropertyGrid class doesn't automatically detect when an object's properties are changed programmatically,
        'so we'll force a refresh if the object with the changed property is currently selected in the property grid:
        If Properties.SelectedObjects.Contains(sender) Then Properties.Refresh()

        Select Case e.PropertyName
            Case "Project"
                With SceneList.ComboBox
                    .DataBindings.Clear()
                    .ValueMember = "Id"
                    .DisplayMember = "Name"
                    .DataSource = Designer.Project.Children
                    .DataBindings.Add("SelectedItem", Designer, "SelectedScene")
                End With

            Case "Children"
                If sender.GetType() Is GetType(Project) Then
                    With SceneList.ComboBox
                        .DataSource = Designer.Project.Children
                    End With
                End If
            Case "Playing"
                ConfigureUI()
            Case "SelectedObjects"
                If Designer.SelectedObjects.Count = 1 Then
                    Properties.SelectedObject = Designer.SelectedObjects.First
                ElseIf Designer.SelectedObjects.Count = 0 Then
                    Properties.SelectedObject = Nothing
                Else
                    Properties.SelectedObjects = Designer.SelectedObjects
                End If

                ConfigureUI()
                ConstructTree()
            Case "SelectedScene"
                Designer.SelectedObject = Designer.SelectedScene.Children.FirstOrDefault

                ConfigureUI()
                ConstructTree()

                SceneSurface1.Refresh()
            Case "SelectedLayer", "Undo"
                ConfigureUI()
            Case "SelectedTool"
                For Each item As ToolStripItem In DesignerTools.Items
                    If item.Tag IsNot Nothing Then
                        CType(item, ToolStripButton).Checked = item.Tag Is Designer.SelectedTool.GetType()
                    End If
                Next
            Case "Frame"
                ConfigureUI()

                If Designer.SelectedObjects.Count = 1 Then
                    'Did we change keyframes? if so, select the object that matches this id in the new keyframe:
                    Dim kf As KeyFrame = Designer.SelectedObject.GetAncestor(Of KeyFrame)()
                    '(otherwise unselect)            
                Else
                    Designer.SelectedObject = Nothing
                End If
        End Select
    End Sub
#End Region

#Region "DuplicateToolStripMenuItem_Click"
    Private Sub DuplicateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DuplicateToolStripMenuItem.Click
        Dim selection As XElement = <ArtificeObjects/>

        For Each vo As ArtificeObject In Designer.SelectedObjects
            selection.Add(vo.ToXElement())
        Next

        Paste(selection)
    End Sub
#End Region

#Region "EffectList_SelectedIndexChanged"
    Private Sub EffectList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles EffectList.SelectedIndexChanged
        If EffectList.SelectedItems.Count = 0 Then Return

        Properties.SelectedObject = EffectList.SelectedItems(0).Tag

        ConfigureUI()
    End Sub
#End Region

#Region "ExitToolStripMenuItem_Click"
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub
#End Region

#Region "ExportPNGMenuItem_Click"
    Private Sub ExportPNGMenuItem_Click(sender As Object, e As EventArgs) Handles ExportPNGMenuItem.Click
        'Prompt for filename:
        If ImageExportFilename.ShowDialog(Me) <> Windows.Forms.DialogResult.OK Then Return

        Designer.Exporting = True

        Dim b As New Bitmap(Designer.Project.StageWidth, Designer.Project.StageHeight)
        Dim g As Graphics = Graphics.FromImage(b)

        Try
            'Clear the image to transparent:
            g.Clear(Color.Transparent)

            'Set our origin to the center of the image:
            g.TranslateTransform(b.Width / 2, b.Height / 2)

            'We're rendering so let's produce the highest-quality image we can:
            g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit

            Dim rc As New RenderContext(Designer.SelectedScene.Frame, g)

            Designer.SelectedScene.Render(rc)

            'PNG's have to be written to a MemoryStream first, then you can dump the stream to a file:
            Dim ms As New IO.MemoryStream
            b.Save(ms, Imaging.ImageFormat.Png)

            Dim bytes() As Byte = ms.ToArray()

            Dim fs As New IO.FileStream(ImageExportFilename.FileName, IO.FileMode.OpenOrCreate)

            fs.Write(bytes, 0, bytes.Length)

            fs.Close()

            MsgBox("Export complete.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Artifice Image Export")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Artifice")
        Finally
            'Dispose our graphics object so that there's no memory leaks
            g.Dispose()
        End Try

        Designer.Exporting = False
    End Sub
#End Region

#Region "ExportVideoMenuItem_Click"
    Private Sub ExportVideoMenuItem_Click(sender As Object, e As EventArgs)
    End Sub
#End Region

#Region "FillColorButton_Click"
    Private Sub FillColorButton_Click(sender As Object, e As EventArgs) Handles FillColorButton.Click
        Dim ColorPicker As New AdvancedColorPicker

        ColorPicker.Color = New ArtificeColor(Designer.FillColor)

        If ColorPicker.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Designer.FillColor = ColorPicker.Color.Color

            Designer.Animating = True
            Designer.StartUndoBatch()

            For Each so In Designer.SelectedObjects
                If so.Is(Of VectorObject)() Then
                    If Not so.As(Of VectorObject)().FillColor.Value.Equals(ColorPicker.Color) Then
                        Designer.AddUndo(New PropertyChangeUndo(so.As(Of VectorObject)().FillColor, "Value", so.As(Of VectorObject)().FillColor.Value, ColorPicker.Color))
                        so.As(Of VectorObject)().FillColor.Value = ColorPicker.Color
                    End If
                End If
            Next

            Designer.EndUndoBatch()
            Designer.Animating = False

            SceneSurface1.Refresh()
        End If
    End Sub
#End Region

#Region "FindFocussedControl"
    ''' <summary>
    ''' Finds the control that actually has the focus, searching recursively through the control stack
    ''' </summary>
    ''' <param name="ctr"></param>
    ''' <returns></returns>
    ''' <remarks>Taken from http://stackoverflow.com/questions/660173/how-do-i-find-out-which-control-has-focus-in-net-windows-forms </remarks>
    Function FindFocussedControl(ByVal ctr As Control) As Control
        Dim container As ContainerControl = TryCast(ctr, ContainerControl)
        Do While (container IsNot Nothing)
            ctr = container.ActiveControl
            container = TryCast(ctr, ContainerControl)
        Loop
        Return ctr
    End Function
#End Region

#Region "FitZoomMenuItem_Click"
    Private Sub FitZoomMenuItem_Click(sender As Object, e As EventArgs) Handles FitZoomMenuItem.Click
        SceneSurface1.AutoFit = True
    End Sub
#End Region

#Region "HighQualityRenderToolStripMenuItem_Click"
    Private Sub HighQualityRenderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HighQualityRenderToolStripMenuItem.Click
        Designer.HighQualityRender = Not Designer.HighQualityRender
        ConfigureUI()
    End Sub
#End Region

#Region "FlipHorizontalToolStripMenuItem_Click"
    Private Sub FlipHorizontalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FlipHorizontalToolStripMenuItem.Click
        Designer.StartUndoBatch()
        Designer.Animating = True
        Designer.SelectedObjects.First.Flip(True, False)
        Designer.SelectedObjects.First.Animate(New AnimationContext(Designer.SelectedScene.Frame))
        Designer.Animating = False
        Designer.EndUndoBatch()
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "FlipVerticalToolStripMenuItem_Click"
    Private Sub FlipVerticalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FlipVerticalToolStripMenuItem.Click
        Designer.StartUndoBatch()
        Designer.Animating = True
        Designer.SelectedObjects.First.Flip(False, True)
        Designer.SelectedObjects.First.Animate(New AnimationContext(Designer.SelectedScene.Frame))
        Designer.Animating = False
        Designer.EndUndoBatch()
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "GetFileName"
    ''' <summary>
    ''' Gets a filename to save the current project to
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFileName() As Boolean
        If Designer.Project.FileName <> "" Then
            Saver.FileName = Designer.Project.FileName
        End If

        Return Saver.ShowDialog(Me) = DialogResult.OK
    End Function
#End Region

#Region "InternalSave"
    ''' <summary>
    ''' Prompts the user for a filename and saves the Artifice project if they clicked "OK"
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InternalSave() As Boolean
        If GetFileName() Then
            Designer.Project.SaveAs(Saver.FileName)

            Return True
        End If

        Return False
    End Function
#End Region

#Region "LineColorButton_Click"
    Private Sub LineColorButton_Click(sender As Object, e As EventArgs) Handles LineColorButton.Click
        Dim ColorPicker As New AdvancedColorPicker

        ColorPicker.Color = New ArtificeColor(Designer.LineColor)

        If ColorPicker.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Designer.LineColor = ColorPicker.Color.Color

            Designer.Animating = True
            Designer.StartUndoBatch()

            For Each so In Designer.SelectedObjects
                If so.Is(Of VectorObject)() Then
                    If Not so.As(Of VectorObject)().LineColor.Value.Equals(ColorPicker.Color) Then
                        Designer.AddUndo(New PropertyChangeUndo(so.As(Of VectorObject)().LineColor, "Value", so.As(Of VectorObject)().LineColor.Value, New ArtificeColor(ColorPicker.Color.Color)))
                        so.As(Of VectorObject)().LineColor.Value = ColorPicker.Color
                    End If
                End If
            Next

            Designer.EndUndoBatch()
            Designer.Animating = False
        End If
    End Sub
#End Region

#Region "LoopButton_Click"
    Private Sub LoopButton_Click(sender As Object, e As EventArgs)
        Designer.LoopPlayback = Not Designer.LoopPlayback
    End Sub
#End Region

#Region "MainWindow_FormClosing"
    Private Sub MainWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Designer.Project IsNot Nothing AndAlso Designer.Project.Dirty Then
            If PromptForSave("You have unsaved changes. Would you like to save before closing?") Then
                e.Cancel = True
                Return
            End If
        End If

        Designer.Playing = False
        Running = False

        Thread.Sleep(300)
    End Sub
#End Region

#Region "NewFolder_Click"
    Private Sub NewFolder_Click(sender As Object, e As EventArgs) Handles NewFolder.Click, AddFolderToolStripMenuItem.Click
        If Designer.SelectedScene Is Nothing Then Return

        Dim l As New LayerGroup()

        l.Name = "New Folder"

        If Designer.SelectedLayer Is Nothing Then
            Designer.SelectedScene.AddChild(l)
        Else
            'Insert above selected layer
            Designer.SelectedLayer.AddAfter(l)
        End If
    End Sub
#End Region

#Region "NewLayer_Click"
    Private Sub NewLayer_Click(sender As Object, e As EventArgs) Handles NewLayer.Click, AddLayerToolStripMenuItem.Click
        If Designer.SelectedScene Is Nothing Then Return

        Dim l As New Layer()

        l.Name = "New Layer"

        If Designer.SelectedLayer Is Nothing Then
            Designer.SelectedScene.AddChild(l)
        Else
            'Insert above selected layer
            Designer.SelectedLayer.AddAfter(l)
        End If

        Designer.SelectedLayer = l
    End Sub
#End Region

#Region "NewSceneToolStripMenuItem_Click"
    Private Sub NewSceneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewSceneToolStripMenuItem.Click, NewSceneButton.Click
        Dim s As New Scene()
        Dim counter As Integer = 0
        Dim found As Boolean = True

        While found
            found = False

            counter += 1

            s.Name = "Artwork Page " & counter

            For Each child As Scene In Designer.Project.Children
                If child.Name = s.Name Then found = True
            Next
        End While

        Dim l As New Layer()

        l.Name = "Layer 1"

        s.AddChild(l)

        Designer.Project.AddChild(s)

        Designer.SelectedScene = s
        Designer.SelectedLayer = l
        Designer.SelectedObject = l
    End Sub
#End Region

#Region "NewToolStripMenuItem_Click"
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Dim np As New NewProject()

        If Designer.Project IsNot Nothing AndAlso Designer.Project.Dirty Then
            If PromptForSave("You have unsaved changes. Would you like to save before creating a new project?") Then
                Return
            End If
        End If

        If np.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Dim p As New Project()

            p.Name = np.ProjectName.Text.Trim()
            p.StageHeight = np.StageHeight.Value
            p.StageWidth = np.StageWidth.Value

            Dim sc As New Scene()

            sc.Name = "Main Artwork"

            p.AddChild(sc)

            Dim l As New Layer()

            l.Name = "Layer 1"

            sc.AddChild(l)

            Designer.Project = p
            Designer.SelectedScene = sc
            Designer.SelectedLayer = l
            Designer.SelectedObject = l
        End If
    End Sub
#End Region

#Region "OnLoad"
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        'Set up databinding for some controls:
        PointEditModeButton.DataBindings.Add(New Binding("Checked", Designer, "PointEditMode"))

        FillColorButton.DataBindings.Add(New Binding("Color", Designer, "FillColor"))
        LineColorButton.DataBindings.Add(New Binding("Color", Designer, "LineColor"))

        AddHandler Designer.PropertyChanged, AddressOf DesignerPropertyChanged

        'Get the list of tools:  
        Dim toolsToAdd As New List(Of ToolBase)

        For Each t As Type In Me.GetType().Assembly.GetTypes()
            If t.IsSubclassOf(GetType(ToolBase)) Then
                Dim tool As ToolBase = Activator.CreateInstance(t)

                toolsToAdd.Add(tool)
            End If
        Next

        'Sort our list by index:
        toolsToAdd.Sort(Function(x, y) CType(x, ToolBase).Index.CompareTo(CType(y, ToolBase).Index))

        'Add our tools (in sorted order) to our toolstrip:
        Dim toolbarPosition As Integer = 0
        For Each tool As ToolBase In toolsToAdd
            Dim tsb As New ToolStripButton()

            tsb.Image = tool.Icon
            tsb.ToolTipText = tool.Description & " (Hotkey " & (toolbarPosition + 1) & ")"
            tsb.Text = tool.Name
            tsb.Tag = tool.GetType()
            tsb.DisplayStyle = ToolStripItemDisplayStyle.Image

            'Use a lambda funciton (yay!) to assign the global designer object's
            '"SelectedTool" property to the tool associated with this button:
            AddHandler tsb.Click, Sub(sender As Object, e2 As EventArgs)
                                      Designer.SelectedTool = Activator.CreateInstance(tsb.Tag)
                                      StatusInformation.Text = Designer.SelectedTool.Description
                                  End Sub

            'Add it to the list:
            DesignerTools.Items.Insert(toolbarPosition, tsb)

            'Make sure we start with a tool selected:
            If Designer.SelectedTool Is Nothing Then Designer.SelectedTool = tool

            toolbarPosition += 1
        Next

        'Get our list of effects to add to the dropdown:
        For Each t As Type In Me.GetType().Assembly.GetTypes()
            If t.IsSubclassOf(GetType(EffectBase)) Then
                Dim effect As EffectBase = Activator.CreateInstance(t)

                AddEffectButton.DropDownItems.Add(t.Name, effect.Icon, Sub(sender As Object, evt As EventArgs)
                                                                           'Add the effect to this object
                                                                           Dim eff As EffectBase = Activator.CreateInstance(t)
                                                                           CType(Properties.SelectedObject, ArtificeObject).AddEffect(eff)

                                                                           Designer.AddUndo(New ObjectAddedUndo(eff))

                                                                           Dim li As New ListViewItem(t.Name)

                                                                           If Not ProjectTreeImages.Images.ContainsKey(t.ToString()) Then
                                                                               ProjectTreeImages.Images.Add(t.ToString(), effect.Icon)
                                                                           End If

                                                                           li.Tag = eff

                                                                           li.ImageKey = t.ToString()

                                                                           EffectList.Items.Add(li)

                                                                           li.Selected = True

                                                                           Properties.SelectedObject = eff
                                                                       End Sub)
            End If
        Next

        ConfigureUI()

        Dim pt As New Thread(AddressOf PlayingThread)

        pt.Start()
    End Sub
#End Region

#Region "OpenToolStripMenuItem_Click"
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        If Designer.Project IsNot Nothing AndAlso Designer.Project.Dirty Then
            If PromptForSave("You have unsaved changes. Would you like to save before opening another file?") Then
                Return
            End If
        End If

        If Opener.ShowDialog(Me) = DialogResult.OK Then
            Designer.Project = Project.FromFile(Opener.FileName)
        End If
    End Sub
#End Region

#Region "Paste"
    ''' <summary>
    ''' Pastses the content of the clipboard into the current layer
    ''' </summary>
    ''' <param name="selectedObjects"></param>
    ''' <remarks></remarks>
    Private Sub Paste(selectedObjects As XElement)
        Dim newObjects As New List(Of ArtificeObject)

        Designer.StartUndoBatch()

        If Designer.SelectedContainer Is Nothing Then
            Dim kf As KeyFrame = Designer.SelectedLayer.As(Of Layer)().AddKeyFrame(Designer.SelectedScene.Frame)

            Designer.AddUndo(New ObjectAddedUndo(kf))

            Designer.SelectedContainer = kf
        End If

        For Each child As XElement In selectedObjects.Elements
            Dim vo As ArtificeObject = ArtificeObject.FromXElement(child, True)
            Designer.SelectedContainer.AddChild(vo)

            newObjects.Add(vo)

            Designer.AddUndo(New ObjectAddedUndo(vo))
        Next

        Designer.EndUndoBatch()

        Designer.SelectedObject = Nothing
        Designer.AddSelection(newObjects.ToArray())
    End Sub
#End Region

#Region "PasteToolStripMenuItem_Click"
    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        Try
            Dim selectedObjects As XElement = XElement.Parse(Clipboard.GetText())

            Paste(selectedObjects)

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "PlayAudioToolStripMenuItem_Click"
    Private Sub PlayAudioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlayAudioToolStripMenuItem.Click
        Designer.MuteAudio = Not Designer.MuteAudio
        ConfigureUI()
    End Sub
#End Region

#Region "PlayButton_Click"
    Private Sub PlayButton_Click(sender As Object, e As EventArgs)
        Designer.Playing = Not Designer.Playing
    End Sub
#End Region

#Region "PlayButton_CheckedChanged"
    Private Sub PlayButton_CheckedChanged(sender As Object, e As EventArgs)
    End Sub
#End Region

#Region "PlayingThread"
    ''' <summary>
    ''' The background thread which handles the playing of the selected scene with the appropriate timing
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PlayingThread()
        Dim frameLengthInMilliseconds As Long
        Dim startFrameInMilliseconds As Long

        While Running
            'Are we in playing mode (but the playing timer hasn't been started yet)? Time to initialize everything!
            If Designer.Playing AndAlso Not playbackTimer.IsRunning Then
                frameLengthInMilliseconds = 1000 / Designer.Project.FramesPerSecond ' Compute the number of milliseconds each frame will take
                startFrameInMilliseconds = Designer.SelectedScene.Frame * frameLengthInMilliseconds ' Based on the frame we're on right now, how many milliseconds into the scene are we?
                playbackTimer.Start() ' Start the time that tracks exactly how many milliseconds have elapsed. We use this instead of DateTime.Subtract() because it is nowhere near as precise.
            ElseIf Designer.Playing Then 'If we're playing AND our timer is running
                Dim nextFrameInMilliseconds = (startFrameInMilliseconds + playbackTimer.ElapsedMilliseconds) 'This is where we're at now in milliseconds

                'Our actual frame number is the new total divided by the number of frames in milliseconds.
                'Since we're dealing in Longs and not floating-point numbers, we will lose a little precision. For 24fps, it's about 4ms per second.
                ThreadsafeFrameSet(nextFrameInMilliseconds \ frameLengthInMilliseconds)
            Else
                'If we're not playing, then stop and reset our timer and let this thread sleep in the background for awhile:
                playbackTimer.Stop()
                playbackTimer.Reset()
                Thread.Sleep(100)
            End If
        End While

    End Sub
#End Region

#Region "PointEditModeButton_Click"
    Private Sub PointEditModeButton_Click(sender As Object, e As EventArgs) Handles PointEditModeButton.Click
        Designer.PointEditMode = Not Designer.PointEditMode
    End Sub
#End Region

#Region "ProcessCmdKey"
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        'We want to trap space and enter, but not if you're in an actual control:
        If FindFocussedControl(Me) IsNot Nothing AndAlso (FindFocussedControl(Me).GetType().ToString() = "System.Windows.Forms.PropertyGridInternal.PropertyGridView+GridViewEdit" OrElse FindFocussedControl(Me).GetType() Is GetType(TextBox)) Then
            Return False
        End If

        If keyData >= Keys.D1 AndAlso keyData <= Keys.D9 Then
            Dim toolIndex As Integer = keyData - Keys.D1

            Dim toolType As Type = CType(DesignerTools.Items(toolIndex), ToolStripButton).Tag

            If toolType IsNot Nothing Then Designer.SelectedTool = Activator.CreateInstance(toolType)
        End If

        Select Case keyData
            Case Keys.Space
                Designer.Playing = Not Designer.Playing

                Return True
            Case Keys.Enter
                Designer.SelectedScene.Frame = 1

                Return True
            Case Else
                Return MyBase.ProcessCmdKey(msg, keyData)
        End Select
    End Function
#End Region

#Region "ProjectTree_AfterSelect"
    Private Sub ProjectTree_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles ProjectTree.AfterSelect
        If ProjectTree.SelectedNode Is Nothing OrElse ProjectTree.SelectedNode.Tag Is Nothing Then Return

        Properties.SelectedObject = ProjectTree.SelectedNode.Tag

        If CType(ProjectTree.SelectedNode.Tag, ArtificeObject).Is(Of ArtificeMoveable)() Then
            SceneSurface1.Refresh()
        End If
    End Sub
#End Region

#Region "PromptForSave"
    ''' <summary>
    ''' Prompts the user to save the current Project
    ''' </summary>
    ''' <param name="promptText">The text to prompt with</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PromptForSave(promptText As String) As Boolean
        Select Case MsgBox(promptText, MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNoCancel)
            Case MsgBoxResult.Yes
                If Designer.Project.FileName = "" Then
                    'If they cancelled out of saving, don't continue, because they'll lose their work!
                    If Not InternalSave() Then
                        Return True
                    End If
                Else
                    Designer.Project.Save()
                End If
            Case MsgBoxResult.No
                'Do nothing 
            Case MsgBoxResult.Cancel
                'Return a cancel status
                Return True
        End Select

        Return False
    End Function
#End Region

#Region "Properties_PropertyValueChanged"
    Private Sub Properties_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) Handles Properties.PropertyValueChanged
        If Properties.SelectedObjects.Count = 1 Then
            Designer.AddUndo(New PropertyChangeUndo(Properties.SelectedObject, e.ChangedItem.PropertyDescriptor.Name, e.OldValue, e.ChangedItem.Value))
        Else
            Designer.StartUndoBatch()
            'TODO: this will break because of a flaw in PropertyGrid. Not sure what the workaround is.
            For Each vo As ArtificeObject In Properties.SelectedObjects
                Designer.AddUndo(New PropertyChangeUndo(vo, e.ChangedItem.PropertyDescriptor.Name, e.OldValue, e.ChangedItem.Value))
            Next

            Designer.EndUndoBatch()
        End If
    End Sub
#End Region

#Region "Properties_SelectedObjectsChanged"
    Private Sub Properties_SelectedObjectsChanged(sender As Object, e As EventArgs) Handles Properties.SelectedObjectsChanged
        ConstructEffectsList()
    End Sub
#End Region

#Region "RedoToolStripMenuItem_Click"
    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click
        Designer.Redo()
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "RemoveEffectButton_Click"
    ''' <summary>
    ''' The handler for the Remove Effect button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RemoveEffectButton_Click(sender As Object, e As EventArgs) Handles RemoveEffectButton.Click
        If EffectList.SelectedItems.Count = 0 Then Return

        Dim effect As EffectBase = EffectList.SelectedItems(0).Tag

        Designer.AddUndo(New ObjectRemovedUndo(effect))

        effect.Remove()

        EffectList.SelectedItems(0).Remove()

        ConfigureUI()
    End Sub
#End Region

#Region "RenameLayerToolStripMenuItem_Click"
    Private Sub RenameLayerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenameLayerToolStripMenuItem.Click
        DesignerTimeline.StartLayerRename()
    End Sub
#End Region

#Region "RevertToolStripMenuItem_Click"
    Private Sub RevertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RevertToolStripMenuItem.Click
        If MsgBox("Are you sure you want revert to the last saved version of this project? You will lose any unsaved changes.", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Return

        Designer.Project = Project.FromFile(Designer.Project.FileName)

    End Sub
#End Region

#Region "RewindButton_Click"
    Private Sub RewindButton_Click(sender As Object, e As EventArgs)
        If Designer.SelectedScene Is Nothing Then Return
        Designer.SelectedScene.Frame = 1
    End Sub
#End Region

#Region "SaveasToolStripMenuItem_Click"
    Private Sub SaveasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveasToolStripMenuItem.Click
        InternalSave()
    End Sub
#End Region

#Region "SaveToolStripMenuItem_Click"
    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If Designer.Project.FileName = "" Then
            'Prompt for filename:
            InternalSave()
        Else
            'We already have a filename; just save:
            Designer.Project.Save()
        End If
    End Sub
#End Region

#Region "SceneList_SelectedIndexChanged"
    Private Sub SceneList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SceneList.SelectedIndexChanged
        Designer.SelectedScene = SceneList.ComboBox.SelectedItem
    End Sub
#End Region

#Region "SceneSurface1_AutoFitChanged"
    Private Sub SceneSurface1_AutoFitChanged(sender As Object, e As EventArgs) Handles SceneSurface1.AutoFitChanged, SceneSurface1.ZoomChanged
        ZoomSelectorButton.Text = IIf(SceneSurface1.AutoFit, "Fit", SceneSurface1.Zoom.ToString("0%"))
        ZoomSelectorButton.ToolTipText = SceneSurface1.Zoom.ToString("0%")
    End Sub
#End Region

#Region "SelectallToolStripMenuItem_Click"
    Private Sub SelectallToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectallToolStripMenuItem.Click
        If Designer.SelectedContainer Is Nothing Then Return

        Designer.SelectedObject = Nothing

        Designer.AddSelection(Designer.SelectedContainer.Children)

        Designer.SelectedTool = New ArrowTool
    End Sub
#End Region

#Region "SendBackwardToolStripMenuItem_Click"
    Private Sub SendBackwardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendBackwardToolStripMenuItem.Click
        Designer.Animating = True
        Designer.StartUndoBatch()
        For Each vo As ArtificeObject In Designer.SelectedObjects
            Designer.AddUndo(New ObjectMoveUndo(vo, vo.ParentIndex - 1))
            vo.SendBackward()
        Next
        Designer.EndUndoBatch()
        Designer.Animating = False
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "SendToBackToolStripMenuItem_Click"
    Private Sub SendToBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendToBackToolStripMenuItem.Click
        Designer.Animating = True
        Designer.StartUndoBatch()
        For Each vo As ArtificeObject In Designer.SelectedObjects
            Designer.AddUndo(New ObjectMoveUndo(vo, 0))
            vo.SendToBack()
        Next
        Designer.EndUndoBatch()
        Designer.Animating = False
        SceneSurface1.Refresh()
    End Sub
#End Region

#Region "SingleFrameAsSVGToolStripMenuItem_Click"
    Private Sub SingleFrameAsSVGToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SingleFrameAsSVGToolStripMenuItem.Click
        If SVGFileName.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            Designer.ExportToSVG(SVGFileName.FileName)
        End If
    End Sub
#End Region

#Region "ThreadsafeFrameChange"
    Private Delegate Sub ThreadsafeFrameChangeDelegate(newFrame As Integer)
    ''' <summary>
    ''' Changes the currently-selected Scene's frame by the given amount in a threadsafe manner
    ''' </summary>
    ''' <param name="newFrame">The new frame to change to</param>
    ''' <remarks></remarks>
    Private Sub ThreadsafeFrameSet(newFrame As Integer)
        If Me.InvokeRequired Then
            Dim del As New ThreadsafeFrameChangeDelegate(AddressOf ThreadsafeFrameSet)

            Me.Invoke(del, New Object() {newFrame})
        Else
            Designer.SelectedScene.Frame = newFrame

            Dim moreFrames As Boolean = False

            For Each l As ArtificeLayerBase In Designer.SelectedScene.Children
                If l.HasKeyFramesAfter(Designer.SelectedScene.Frame) Then
                    moreFrames = True
                    Exit For
                End If
            Next

            If Not moreFrames Then
                If Designer.LoopPlayback Then
                    Designer.SelectedScene.Frame = 1
                    playbackTimer.Stop()
                    playbackTimer.Reset()
                Else
                    Designer.Playing = False
                End If
            End If
        End If
    End Sub
#End Region

#Region "ToolStripButton_MouseEnter"
    Private Sub ToolStripButton_MouseEnter(sender As Object, e As EventArgs) Handles CenterStageVerticalButton.MouseEnter, CenterStageHorizontalButton.MouseEnter
        StatusInformation.Text = CType(sender, ToolStripButton).ToolTipText
    End Sub
#End Region

#Region "ToolStripButton_MouseEnter"
    Private Sub ToolStripButton_MouseLeave(sender As Object, e As EventArgs) Handles CenterStageVerticalButton.MouseLeave, CenterStageHorizontalButton.MouseLeave
        StatusInformation.Text = Designer.SelectedTool.Description
    End Sub
#End Region

#Region "UngroupToolStripMenuItem_Click"
    Private Sub UngroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UngroupToolStripMenuItem.Click
        If Designer.SelectedObject Is Nothing OrElse Not Designer.SelectedObject.Is(Of Group)() Then
            Return
        End If

        Designer.SelectedObject.As(Of Group)().UnGroup()

        'For Each child As ArtificeObject In Designer.SelectedObject.Children
        '    Designer.SelectedObject.Parent.AddChild(child)
        'Next
        Designer.SelectedObject = Nothing
    End Sub
#End Region

#Region "UndoToolStripMenuItem_Click"
    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        Designer.Undo()
    End Sub
#End Region

#Region "ZoomInToolStripMenuItem_Click"
    Private Sub ZoomInToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZoomInToolStripMenuItem.Click
        SceneSurface1.Zoom += 0.1
    End Sub
#End Region

#Region "ZoomOutToolStripMenuItem_Click"
    Private Sub ZoomOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZoomOutToolStripMenuItem.Click
        SceneSurface1.Zoom -= 0.1
    End Sub
#End Region

#Region "FormClosed"
    Private Sub MainWindow_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub
#End Region

#Region "ToolList"
    Dim EffectPanel As Boolean
    Dim PropPanelPop As Boolean
    Dim HierPanelPop As Boolean

    Private Sub ToolStripBindableButton1_Click(sender As Object, e As EventArgs) Handles ToolStripBindableButton1.Click
        If EffectPanel = False Then
            EffectsPanel.Visible = True
            PropPanel.Visible = False
            HierPanel.Visible = False
        ElseIf EffectPanel = True Then
            EffectsPanel.Visible = False
            PropPanel.Visible = False
            HierPanel.Visible = False
        End If
    End Sub
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If PropPanelPop = False Then
            PropPanel.Visible = True
            EffectsPanel.Visible = False
            HierPanel.Visible = False
        ElseIf PropPanelPop = True Then
            PropPanel.Visible = False
            EffectsPanel.Visible = False
            HierPanel.Visible = False
        End If
    End Sub
    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        If HierPanelPop = False Then
            HierPanel.Visible = True
            EffectsPanel.Visible = False
            PropPanel.Visible = False
        ElseIf HierPanelPop = True Then
            HierPanel.Visible = False
            EffectsPanel.Visible = False
            PropPanel.Visible = False
        End If
    End Sub
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles Hide.Click
        HierPanel.Visible = False
        EffectsPanel.Visible = False
        PropPanel.Visible = False
    End Sub

    Private Sub ToolStripButton1_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripButton1_Click_2(sender As Object, e As EventArgs) Handles HideDesignTimeline.Click
        If SurfaceTimelineSplitter.Panel2Collapsed = False Then
            SurfaceTimelineSplitter.Panel2Collapsed = True
            HideDesignTimeline.Text = "Hide"
        ElseIf SurfaceTimelineSplitter.Panel2Collapsed = True Then
            SurfaceTimelineSplitter.Panel2Collapsed = False
            HideDesignTimeline.Text = "Show"
        End If
    End Sub
#End Region
End Class

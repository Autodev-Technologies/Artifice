''' <summary>
''' Handles the undoing/redoing when an object is removed from the Artifice DOM
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class ObjectRemovedUndo
    Inherits UndoBase

    Private MyObjectParent As ArtificeObject
    Private MyObject As ArtificeObject
    Private MyParentIndex As Integer

#Region "Constructor"
    Public Sub New(o As ArtificeObject)
        MyObject = o
        MyObjectParent = o.Parent
        MyParentIndex = o.ParentIndex
    End Sub
#End Region

    Protected Overrides Sub OnRedo()
        MyObject.Remove()
    End Sub

    Protected Overrides Sub OnUndo()
        If MyParentIndex >= MyObjectParent.Children.Count Then
            MyObjectParent.AddChild(MyObject)
        Else
            MyObjectParent.Children(MyParentIndex).AddBefore(MyObject)
        End If
    End Sub
End Class

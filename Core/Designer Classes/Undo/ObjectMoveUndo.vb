
''' <summary>
''' Handles undoing/redoing objects that have been moved within their parent's list of children. It should be created before the move is actually performed
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class ObjectMoveUndo
    Inherits UndoBase

    Private MyObject As ArtificeObject ' The object that has been moved
    Private MyNewIndex As Integer ' The new index
    Private MyOldIndex As Integer ' The original index

#Region "Constructor"
    ''' <summary>
    ''' Constructs a new instance of ObjectMoveUndo for the given object and its new index. This should be created prior to the move actually happening.
    ''' </summary>
    ''' <param name="vo">The object being moved</param>
    ''' <param name="newIndex">The new index of this object</param>
    ''' <remarks></remarks>
    Public Sub New(vo As ArtificeObject, newIndex As Integer)
        MyObject = vo
        MyOldIndex = vo.ParentIndex
        MyNewIndex = newIndex
    End Sub
#End Region

    Protected Overrides Sub OnRedo()
        MyObject.MoveInParent(MyNewIndex)
    End Sub

    Protected Overrides Sub OnUndo()
        MyObject.MoveInParent(MyOldIndex)
    End Sub
End Class

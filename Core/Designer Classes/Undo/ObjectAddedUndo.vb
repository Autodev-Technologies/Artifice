''' <summary>
''' Handles the undoing/redoing when an object is added to the Artifice DOM
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class ObjectAddedUndo
    Inherits UndoBase

    Private MyObjectParent As ArtificeObject ' The original parent of the object
    Private MyObject As ArtificeObject ' The object being added

#Region "Constructor"
    ''' <summary>
    ''' Creates a new instance of ObjectAddedUndo for the given object
    ''' </summary>
    ''' <param name="vo">The newly-added ArtificeObject</param>
    ''' <remarks></remarks>
    Public Sub New(vo As ArtificeObject)
        MyObject = vo
        MyObjectParent = vo.Parent
    End Sub
#End Region

#Region "Redo"
    Protected Overrides Sub OnRedo()
        'Re-add this object to its parent
        MyObjectParent.AddChild(MyObject)
    End Sub
#End Region

#Region "OnUndo"
    Protected Overrides Sub OnUndo()
        'Remove it from its parent
        MyObject.Remove()
    End Sub
#End Region

End Class

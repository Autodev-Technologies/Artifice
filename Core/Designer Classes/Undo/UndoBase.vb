''' <summary>
''' The abstract base class for all undo/redo operations in Artifice
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public MustInherit Class UndoBase

    ''' <summary>
    ''' Called when a derived class is to handle the Redo command
    ''' </summary>
    ''' <remarks></remarks>
    Protected MustOverride Sub OnRedo()

    ''' <summary>
    ''' Called when a derived class is to handle the Undo command
    ''' </summary>
    ''' <remarks></remarks>
    Protected MustOverride Sub OnUndo()

    ''' <summary>
    ''' Redoes this action
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Redo()
        OnRedo()
    End Sub

    ''' <summary>
    ''' Undoes this action
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Undo()
        OnUndo()
    End Sub

End Class

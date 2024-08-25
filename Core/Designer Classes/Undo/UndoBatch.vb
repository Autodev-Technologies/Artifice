''' <summary>
''' Combines a group of UndoBase objects at once
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class UndoBatch
    Inherits UndoBase

    Private MyUndoList As New List(Of UndoBase) ' The internal list of undo objects

    Public ReadOnly Property IsEmpty As Boolean
        Get
            Return Not MyUndoList.Any
        End Get
    End Property

#Region "AddUndo"
    ''' <summary>
    ''' Adds a new undo object to this batch list
    ''' </summary>
    ''' <param name="u">The undo object to add</param>
    ''' <remarks></remarks>
    Public Sub AddUndo(u As UndoBase)
        MyUndoList.Add(u)
    End Sub
#End Region

#Region "onRedo"
    Protected Overrides Sub OnRedo()
        Designer.Singleton.Animating = True

        For Each u In MyUndoList
            u.Redo()
        Next

        Designer.Singleton.Animating = False
    End Sub
#End Region

#Region "OnUndo"
    Protected Overrides Sub OnUndo()
        Designer.Singleton.Animating = True

        'Go through our list backwards to undo it in the same order it was done:
        For uIndex As Integer = MyUndoList.Count - 1 To 0 Step -1
            MyUndoList(uIndex).Undo()
        Next

        Designer.Singleton.Animating = False
    End Sub
#End Region

End Class

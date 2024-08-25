Imports System.Drawing.Drawing2D

''' <summary>
''' Handles undoing of path changes of vector objects
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class VectorPathChangeUndo
    Inherits UndoBase

    Private MyVectorObject As VectorObject 'The VectorObject whose path was changed
    Private MyNewPath, MyOriginalPath As GraphicsPath 'The original and new GraphicsPath objects that comprised the VectorObject

#Region "Constructor"
    Public Sub New(vo As VectorObject, originalPath As GraphicsPath)
        MyVectorObject = vo
        MyOriginalPath = New GraphicsPath
        MyNewPath = New GraphicsPath

        'Copy the paths (so we're not dealing with object references):
        MyNewPath.AddPath(vo.Path, False)
        MyOriginalPath.AddPath(originalPath, False)
    End Sub
#End Region

#Region "OnRedo"
    Protected Overrides Sub OnRedo()
        MyVectorObject.SetPath(MyNewPath)
    End Sub
#End Region

#Region "OnUndo"
    Protected Overrides Sub OnUndo()
        MyVectorObject.SetPath(MyOriginalPath)
    End Sub
#End Region

End Class

Imports System.Reflection

''' <summary>
''' Handles the undoing/redoing when an object's property is changed
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class PropertyChangeUndo
    Inherits UndoBase

    Private MyObject As Object ' The object that was changed
    Private MyOldValue, MyNewValue As Object 'The old and new values of the property
    Private MyPropertyName As String 'The name of the property

#Region "Constructor"
    ''' <summary>
    ''' Creates a new instance of PropertyChangeUndo, recording the old value and new value of the given object's property
    ''' </summary>
    ''' <param name="o">The object that was changed</param>
    ''' <param name="propertyName">The name of the property that was changed</param>
    ''' <param name="oldValue">The old value of the property</param>
    ''' <param name="newValue">The new value of the property</param>
    ''' <remarks></remarks>
    Public Sub New(o As Object, propertyName As String, oldValue As Object, newValue As Object)
        MyObject = o
        MyPropertyName = propertyName
        MyOldValue = oldValue
        MyNewValue = newValue
    End Sub
#End Region

#Region "OnRedo"
    Protected Overrides Sub OnRedo()
        'Use reflection to set the property to the new value:
        Dim pi As PropertyInfo = MyObject.GetType().GetProperty(MyPropertyName)

        pi.SetValue(MyObject, MyNewValue)
    End Sub
#End Region

#Region "OnUndo"
    Protected Overrides Sub OnUndo()
        'Use reflection to set the property to the old value:
        Dim pi As PropertyInfo = MyObject.GetType().GetProperty(MyPropertyName)

        pi.SetValue(MyObject, MyOldValue)
    End Sub
#End Region

End Class

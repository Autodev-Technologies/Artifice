''' <summary>
''' A class that extends ToolStripButton to allow binding to its properties
''' </summary>
''' <remarks>Created by Autodev. Adapted from http://blog.decayingcode.com/post/easily-enable-databindings-on</remarks>
Public Class ToolStripBindableButton
    Inherits ToolStripButton
    Implements IBindableComponent

    Private MyDataBindings As ControlBindingsCollection = Nothing
    Private MyBindingContext As BindingContext = Nothing

    Public Property BindingContext As BindingContext Implements IBindableComponent.BindingContext
        Get
            If MyBindingContext Is Nothing Then MyBindingContext = New BindingContext()

            Return MyBindingContext
        End Get
        Set(value As BindingContext)
            MyBindingContext = value
        End Set
    End Property

    Public ReadOnly Property DataBindings As ControlBindingsCollection Implements IBindableComponent.DataBindings
        Get
            If MyDataBindings Is Nothing Then MyDataBindings = New ControlBindingsCollection(Me)

            Return MyDataBindings
        End Get
    End Property
End Class

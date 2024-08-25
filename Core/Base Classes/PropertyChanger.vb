Imports System.ComponentModel

''' <summary>
''' Provides the framework for firing the PropertyChangedEventHandler for any properties that wish to do so
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public MustInherit Class PropertyChanger
    Implements INotifyPropertyChanged

    ' Declare the event
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    ' Create the OnPropertyChanged method to raise the event
    Protected Friend Sub OnPropertyChanged(ByVal name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub

    'Handles bubbling up from child classes
    Protected Friend Sub OnPropertyChanged(sender As PropertyChanger, e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(sender, e)
    End Sub
End Class

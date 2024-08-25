Imports System.ComponentModel

''' <summary>
''' A special type of layer that can contain only other layers
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class LayerGroup
    Inherits ArtificeLayerBase

#Region "Properties"

    Private MyExpanded As Boolean = True
    ''' <summary>
    ''' Determines whether or not the child layers are visible (expanded) in the Timeline control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)> _
    Public Property Expanded As Boolean
        Get
            Return MyExpanded
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = MyExpanded <> value

            MyExpanded = value

            If changed Then OnPropertyChanged("Expanded")
        End Set
    End Property

    <Browsable(False)> _
    Public Overrides ReadOnly Property Icon As Image
        Get
            'The icon we return is based on our expanded state:
            Return IIf(Expanded, My.Resources.FolderOpen, My.Resources.FolderClosed)
        End Get
    End Property

#End Region

#Region "HasKeyframesAfter"
    Public Overrides Function HasKeyFramesAfter(frame As Integer) As Boolean
        For Each lb As ArtificeLayerBase In Children
            If lb.HasKeyFramesAfter(frame) Then Return True
        Next

        Return False
    End Function
#End Region

#Region "OnLoad"
    Protected Overrides Sub OnLoad(el As XElement)
        MyBase.OnLoad(el)

        Expanded = CBool(el.@Expanded)
    End Sub
#End Region

#Region "OnSave"
    Protected Overrides Sub OnSave(el As XElement)
        MyBase.OnSave(el)

        el.@Expanded = Expanded.ToString()
    End Sub
#End Region

End Class

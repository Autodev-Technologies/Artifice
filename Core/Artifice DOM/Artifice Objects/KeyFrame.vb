Imports System.ComponentModel

''' <summary>
''' Represents a single keyframe on a layer
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class KeyFrame
    Inherits ArtificeObject

#Region "Properties"

    Private MyFrame As Integer
    ''' <summary>
    ''' The frame that this keyframe is on
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)> _
    Public Property Frame As Integer
        Get
            Return MyFrame
        End Get
        Set(value As Integer)
            Dim changed As Boolean = value <> MyFrame

            'Don't allow two frames in the same layer to share the same frame value (prevent the change):
            If Parent IsNot Nothing AndAlso (From l As KeyFrame In Parent.Children Where l.Frame = value).Any() Then Return

            MyFrame = value

            If changed Then
                OnPropertyChanged("Frame")
            End If
        End Set
    End Property

    <Browsable(False)> _
    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Keyframe
        End Get
    End Property

#End Region

#Region "OnAnimate"
    Protected Overrides Sub OnAnimate(ac As AnimationContext)
        'Push me onto the keyframe stack:
        ac.KeyFrameStack.Push(Me)
    End Sub
#End Region

#Region "OnLoad"
    Protected Overrides Sub OnLoad(el As XElement)
        MyBase.OnLoad(el)

        MyFrame = Integer.Parse(el.@Frame)
    End Sub
#End Region

#Region "OnSave"
    Protected Overrides Sub OnSave(el As XElement)
        MyBase.OnSave(el)

        el.@Frame = MyFrame.ToString()
    End Sub
#End Region

#Region "OnSaveAsSVG"
    Protected Overrides Function OnSaveAsSVG() As XElement()
        'Only export if I'm the current Frame:
        If Parent.As(Of Layer).GetKeyFrame(Designer.SelectedScene.Frame) Is Me Then
            Return MyBase.OnSaveAsSVG()
        End If

        Return Nothing
    End Function
#End Region

End Class

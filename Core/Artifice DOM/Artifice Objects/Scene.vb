Imports System.ComponentModel

''' <summary>
''' This class represents a single animation in the Project
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class Scene
    Inherits ArtificeObject

#Region "Properties"

    Private MyFrame As Integer = 1
    ''' <summary>
    ''' The current frame this scene is displaying
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

            MyFrame = value

            If changed Then
                'If the frame has changed, let's animate:
                Dim ac As New AnimationContext(value)

                Designer.Animating = True

                Animate(ac)

                Designer.Animating = False

                OnPropertyChanged("Frame")
            End If
        End Set
    End Property

    <Browsable(False)> _
    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Scene
        End Get
    End Property

    Private MyLoopPlayback As Boolean = False
    ''' <summary>
    ''' True if playback for this scene is looped at design time
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)> _
    Public Property LoopPlayback As Boolean
        Get
            Return MyLoopPlayback
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = value <> MyLoopPlayback

            MyLoopPlayback = value

            If changed Then OnPropertyChanged("LoopPlayback")
        End Set
    End Property

    Private MyStageColor As New ArtificeColor(Color.White)
    ''' <summary>
    ''' The background color of the stage behind this scene
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Appearance"), DisplayName("Stage Color"), Description("The background color of the stage behind this scene")> _
    Public Property StageColor As ArtificeColor
        Get
            Return MyStageColor
        End Get
        Set(value As ArtificeColor)
            Dim changed As Boolean = Not MyStageColor.Equals(value)

            MyStageColor = value

            If changed Then
                OnPropertyChanged("StageColor")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Returns the maximum frame number in this scene
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)> _
    Public ReadOnly Property TotalFrames As Integer
        Get
            Dim lastFrame As Integer = 1

            For Each l As ArtificeLayerBase In Children
                lastFrame = Math.Max(lastFrame, l.TotalFrames)
            Next

            Return lastFrame
        End Get
    End Property

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

End Class

Imports NAudio
Imports NAudio.Wave
Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports NAudio.Wave.SampleProviders

''' <summary>
''' Represents a single sound that can be played
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class Sound
    Inherits ArtificeMoveable

    Private Shared AudioReaders As New Dictionary(Of Integer, AudioFileReader)
    Private Shared WaveOutBuffers As New Dictionary(Of Integer, WaveOut)
    
#Region "Properties"

    Private MyAction As SoundAction = SoundAction.Play
    ''' <summary>
    ''' The action to perform on the sound
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Playback"), Description("The action to perform on the sound")> _
    Public Property Action As SoundAction
        Get
            Return MyAction
        End Get
        Set(value As SoundAction)
            Dim changed As Boolean = value <> MyAction

            MyAction = value

            If changed Then OnPropertyChanged("Action")
        End Set
    End Property

    Private MyAudioFile As String = ""
    ''' <summary>
    ''' The sound file to play
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Editor(GetType(System.Windows.Forms.Design.FileNameEditor), GetType(System.Drawing.Design.UITypeEditor))> _
    <Category("Playback"), DisplayName("Audio File"), Description("The sound file to play")> _
    Public Property AudioFile As String
        Get
            Return MyAudioFile
        End Get
        Set(value As String)
            Dim changed As Boolean = value <> MyAudioFile

            MyAudioFile = value

            If changed Then
                WaveOut.Stop()

                If AudioReader IsNot Nothing Then
                    AudioReader.Dispose()
                End If

                AudioReaders.Remove(Me.Id)

                OnPropertyChanged("AudioFile")
            End If
        End Set
    End Property

    ''' <summary>
    ''' The internal instance of the AudioReader class for loading the 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)> _
    Protected ReadOnly Property AudioReader As AudioFileReader
        Get
            If Not AudioReaders.ContainsKey(Me.Id) Then
                If AudioFile <> "" AndAlso IO.File.Exists(AudioFile) Then
                    Dim afr As New AudioFileReader(AudioFile)
                    AudioReaders.Add(Me.Id, afr)
                    WaveOut.Init(afr)
                Else
                    Return Nothing
                End If
            End If

            Return AudioReaders(Me.Id)
        End Get
    End Property

    <Browsable(False)> _
    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Audio
        End Get
    End Property

    Private MyVolume As New LerpableSingle(1.0F)
    ''' <summary>
    ''' The volume of the audio playback
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Playback"), Description("The volume of the audio playback")> _
    Public Property Volume As LerpableSingle
        Get
            Return MyVolume
        End Get
        Set(value As LerpableSingle)
            Dim changed As Boolean = Not value.Equals(MyVolume)

            MyVolume = value

            If changed Then
                OnPropertyChanged("Volume")
            End If
        End Set
    End Property

    ''' <summary>
    ''' The buffered WaveOut instance for this audio file
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected ReadOnly Property WaveOut As WaveOut
        Get
            If Not WaveOutBuffers.ContainsKey(Me.Id) Then
                WaveOutBuffers.Add(Me.Id, New WaveOut())
            End If

            Return WaveOutBuffers(Me.Id)
        End Get
    End Property

#End Region

#Region "OnExportAudio"
    Protected Overrides Sub OnExportAudio(context As ExportAudioContext)
        Dim ac As New AnimationContext(context.Frame)
        Dim playUntilFrame As Integer = Designer.SelectedScene.TotalFrames

        Animate(ac)

        If Action = SoundAction.Play AndAlso Me.GetAncestor(Of KeyFrame).Frame = context.Frame Then

            Dim reader As New MediaFoundationReader(AudioFile)

            Dim audioStream As WaveStream = WaveFormatConversionStream.CreatePcmStream(reader)

            If Parent.Next IsNot Nothing Then
                playUntilFrame = Parent.Next.As(Of KeyFrame)().Frame
            End If

            Dim offset As New WaveOffsetStream(audioStream, _
                                               TimeSpan.FromSeconds(context.Frame / Designer.Project.FramesPerSecond + context.Timeshift), _
                                               TimeSpan.Zero,
                                               TimeSpan.FromSeconds((playUntilFrame - context.Frame) / Designer.Project.FramesPerSecond))

            Dim stream32 As New WaveChannel32(offset)

            stream32.PadWithZeroes = True
            stream32.Volume = Volume.Delta

            context.Mixer.AddInputStream(stream32)
        End If
    End Sub
#End Region

#Region "OnClone"
    Protected Overrides Sub OnClone(clone As ArtificeObject)
        clone.As(Of Sound).AudioFile = AudioFile
    End Sub
#End Region

#Region "OnGetBounds"
    Protected Overrides Function OnGetBounds() As RectangleF
        Return New RectangleF(New Point(-My.Resources.Audio_Large.Width() / 2, -My.Resources.Audio_Large.Height() / 2), My.Resources.Audio_Large.Size)
    End Function
#End Region

#Region "OnHitTest"
    Protected Overrides Function OnHitTest(x As Single, y As Single, rc As RenderContext, ByRef transformedLocation As Point) As ArtificeMoveable
        Dim lastPoint As New Point(transformedLocation)

        Dim points(1) As Point
        points(0) = New Point(x, y)

        rc.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, points)

        transformedLocation.X = points(0).X
        transformedLocation.Y = points(0).Y

        If GetBounds().Contains(points(0)) Then Return Me

        transformedLocation.X = lastPoint.X
        transformedLocation.Y = lastPoint.Y

        Return MyBase.OnHitTest(x, y, rc, transformedLocation)
    End Function
#End Region

#Region "OnLoad"
    Protected Overrides Sub OnLoad(el As XElement)
        AudioFile = el.@AudioFile
        If el.@Action <> "" Then Action = [Enum].Parse(GetType(SoundAction), el.@Action)
    End Sub
#End Region

#Region "OnRender"
    Protected Overrides Sub OnRender(rc As RenderContext)
        If Designer.Exporting Then Return

        AudioReader.Volume = Volume.Delta        

        'We only draw this when we're not playing:
        If Not Designer.Playing Then
            Static lastFrame As Integer = -1

            rc.Graphics.DrawImage(My.Resources.Audio_Large, GetBounds())

            If Designer.SelectedObjects.Contains(Me) Then
                Dim b As RectangleF = GetBounds()
                rc.Graphics.DrawRectangle(Pens.Red, b.X, b.Y, b.Width, b.Height)
            End If

            'If we have an audio file set: 
            If AudioReader IsNot Nothing AndAlso lastFrame < 0 OrElse lastFrame <> rc.Frame AndAlso Action = SoundAction.Play AndAlso Not Designer.MuteAudio Then
                lastFrame = rc.Frame

                'Advance to the current index of the frame:
                AudioReader.CurrentTime = TimeSpan.FromSeconds((rc.Frame - Me.GetAncestor(Of Layer)().GetKeyFrame(rc.Frame).Frame) / Designer.Project.FramesPerSecond)

                Try
                    WaveOut.Play()

                    'We're in design mode so just play one frame's worth of sound:
                    Threading.Thread.Sleep(1000 / Designer.Project.FramesPerSecond)

                    WaveOut.Stop()
                Catch ex As Exception
                    WaveOutBuffers.Remove(Me.Id)
                End Try
            End If

        ElseIf AudioReader IsNot Nothing AndAlso Not Designer.MuteAudio Then
            If WaveOut.PlaybackState <> PlaybackState.Playing AndAlso Action = SoundAction.Play Then
                'Advance to the current time index of this frame:
                AudioReader.CurrentTime = TimeSpan.FromSeconds((rc.Frame - Me.GetAncestor(Of Layer)().GetKeyFrame(rc.Frame).Frame) / Designer.Project.FramesPerSecond)
                WaveOut.Play()
            ElseIf WaveOut.PlaybackState = PlaybackState.Playing AndAlso Action = SoundAction.Stop Then
                WaveOut.Stop()
            End If
        End If
    End Sub
#End Region

#Region "OnSave"
    Protected Overrides Sub OnSave(el As XElement)
        el.@AudioFile = AudioFile
        el.@Action = Action.ToString()
    End Sub
#End Region

End Class


Public Enum SoundAction
    Play
    [Stop]
End Enum
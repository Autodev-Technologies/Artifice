''' <summary>
''' A class to assist in exporting audio content to video
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class ExportAudioContext

#Region "Properties"
    Private MyFrame As Integer
    ''' <summary>
    ''' The frame that is currently being exported
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Frame As Integer
        Get
            Return MyFrame
        End Get
    End Property

    Private MyMixer As NAudio.Wave.WaveMixerStream32
    ''' <summary>
    ''' The NAudio.Wave.WaveMixerStream32 used to mix all the audio components of this Scene
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Mixer As NAudio.Wave.WaveMixerStream32
        Get
            Return MyMixer
        End Get
    End Property

    Private MyTimeshift As Single = 0.0
    ''' <summary>
    ''' The timeshift to apply to exported audio (used to adjust audio being played in subscenes)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Timeshift As Single
        Get
            Return MyTimeshift
        End Get
    End Property
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Creates a new instance of ExportAudioContext with the given mixer for the given frame
    ''' </summary>
    ''' <param name="mixer">The WaveMixerStream32 class used to mix all the audio</param>
    ''' <param name="frame">The frame currently being exported</param>
    ''' <remarks></remarks>
    Public Sub New(mixer As NAudio.Wave.WaveMixerStream32, frame As Integer)
        Me.New(mixer, frame, 0.0)
    End Sub

    ''' <summary>
    ''' Creates a new instance of ExportAudioContext with the given mixer for the given frame and timeshift
    ''' </summary>
    ''' <param name="mixer">The WaveMixerStream32 class used to mix all the audio</param>
    ''' <param name="frame">The frame currently being exported</param>
    ''' <param name="timeshift">The time (in seconds) to shift the audio by</param>
    ''' <remarks></remarks>
    Public Sub New(mixer As NAudio.Wave.WaveMixerStream32, frame As Integer, timeshift As Single)
        MyMixer = mixer
        MyFrame = frame
        MyTimeshift = timeshift
    End Sub
#End Region

End Class

''' <summary>
''' Contains contextual information for the animation process in the Artifice DOM
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class AnimationContext

#Region "Properties"
    Private MyEffects As New List(Of EffectBase)
    ''' <summary>
    ''' The list of Effects that are cumulatively applied while animating
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Effects As List(Of EffectBase)
        Get
            Return MyEffects
        End Get
    End Property

    Private MyFrame As Integer
    ''' <summary>
    ''' The frame to animate to
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Frame As Integer
        Get
            Return MyFrame
        End Get
    End Property

    ''' <summary>
    ''' The current KeyFrame we are animating in (if any)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property KeyFrame As KeyFrame
        Get
            If MyKeyFrameStack.Any Then Return MyKeyFrameStack.Peek()

            Return Nothing
        End Get
    End Property

    Private MyKeyFrameStack As New Stack(Of KeyFrame)
    ''' <summary>
    ''' The stack of KeyFrame objects we are animating in
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property KeyFrameStack As Stack(Of KeyFrame)
        Get
            Return MyKeyFrameStack
        End Get
    End Property

    ''' <summary>
    ''' The amount being lerped based on the current frame and current KeyFrame
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LerpAmount As Single
        Get
            If KeyFrame Is Nothing OrElse KeyFrame.Next Is Nothing Then Return 0.0

            Return (Frame - KeyFrame.Frame) / (KeyFrame.Next.As(Of KeyFrame).Frame - KeyFrame.Frame)
        End Get
    End Property
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Creates a new instance of AnimationContext for the given frame
    ''' </summary>
    ''' <param name="frame">The frame to animate at</param>
    ''' <remarks></remarks>
    Sub New(frame As Integer)
        MyFrame = frame
    End Sub
#End Region

End Class

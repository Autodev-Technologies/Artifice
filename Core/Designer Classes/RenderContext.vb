''' <summary>
''' Provides contextual information to assis with rendering throughout the Artifice DOM
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class RenderContext

    Private TransformStack As New Stack(Of System.Drawing.Drawing2D.GraphicsState)

#Region "Properties"
    Private MyEffects As New List(Of EffectBase)
    ''' <summary>
    ''' The list of effects being cumulatively applied to all ArtificeObjects
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
    ''' The frame currently being rendered
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Frame As Integer
        Get
            Return MyFrame
        End Get
    End Property

    Private MyGraphics As Graphics
    ''' <summary>
    ''' The graphics object to render to
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Graphics As Graphics
        Get
            Return MyGraphics
        End Get
    End Property

    Private MyOpacityStack As New Stack(Of Single)
    ''' <summary>
    ''' The stack of opacity transformations in this render context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property OpacityStack As Stack(Of Single)
        Get
            Return MyOpacityStack
        End Get
    End Property
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructs a new instance of RenderContext for the given frame and Graphics instance
    ''' </summary>
    ''' <param name="frame">The frame currently being rendered</param>
    ''' <param name="g">The graphics object to render to</param>
    ''' <remarks></remarks>
    Public Sub New(frame As Integer, g As Graphics)
        MyFrame = frame
        MyGraphics = g
    End Sub
#End Region

#Region "PopGraphicsState"
    ''' <summary>
    ''' Pops and restores the Graphics transform state from the transform stack
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PopGraphicsState()
        Graphics.Restore(TransformStack.Pop())
    End Sub
#End Region

#Region "PushGraphicsState"
    ''' <summary>
    ''' Pushes the current Graphics transform state to the transform stack
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PushGraphicsState()
        TransformStack.Push(Graphics.Save())
    End Sub
#End Region

End Class

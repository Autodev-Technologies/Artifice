Imports System.ComponentModel

''' <summary>
''' A special effect that creates a dropshadow around all applicable ArtificeObjects
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class DropShadow
    Inherits EffectBase

#Region "Properties"
    <Browsable(False)> _
    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.DropShadow
        End Get
    End Property

    Private WithEvents MyShadowColor As New LerpableColor(Color.FromArgb(99, 0, 0, 0))
    <Category("Effect"), DisplayName("Shadow Color"), Description("The color of the shadow being projected")> _
    Public ReadOnly Property ShadowColor As LerpableColor
        Get
            Return MyShadowColor
        End Get      
    End Property

    Private MyXOffset As New LerpableSingle(15)
    <Category("Effect"), DisplayName("X Offset"), Description("The horizontal offset of the shadow")> _
    Public Property XOffset As LerpableSingle
        Get
            Return MyXOffset
        End Get
        Set(value As LerpableSingle)
            Dim changed As Boolean = Not value.Equals(MyXOffset)

            MyXOffset = value

            If changed Then
                OnPropertyChanged("XOffset")
            End If
        End Set
    End Property

    Private MyYOffset As New LerpableSingle(15)
    <Category("Effect"), DisplayName("Y Offset"), Description("The vertical offset of the shadow")> _
    Public Property YOffset As LerpableSingle
        Get
            Return MyYOffset
        End Get
        Set(value As LerpableSingle)
            Dim changed As Boolean = Not value.Equals(MyYOffset)

            MyYOffset = value

            If changed Then
                OnPropertyChanged("YOffset")
            End If
        End Set
    End Property
#End Region

#Region "OnBeforeRender"
    Public Overrides Sub OnBeforeRender(rc As RenderContext, obj As ArtificeObject)
        'Drop shadows are drawn BEFORE the actual object is drawn :)

        'We only apply drop shadow to VectorObjects:
        If obj.Is(Of VectorObject)() Then

            If obj.As(Of VectorObject)().Path IsNot Nothing Then
                rc.PushGraphicsState()

                rc.Graphics.TranslateTransform(XOffset.Delta, YOffset.Delta)

                'Dim br As New SolidBrush(Color.FromArgb(ShadowColor.Alpha * rc.OpacityStack.Peek, ShadowColor.Red, ShadowColor.Green, ShadowColor.Blue))
                Dim br As Brush = ShadowColor.Delta.GetBrush(rc.OpacityStack.Peek)
                rc.Graphics.FillPath(br, obj.As(Of VectorObject)().Path)
                br.Dispose()

                rc.PopGraphicsState()
            End If

        End If

    End Sub
#End Region

    Private Sub MyShadowColor_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles MyShadowColor.PropertyChanged
        If e.PropertyName = "Value" Then OnPropertyChanged("ShadowColor")
    End Sub
End Class

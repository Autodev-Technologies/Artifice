Imports System.ComponentModel

''' <summary>
''' The abstract base class for all visual objects that can be scaled and rotated
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public MustInherit Class ArtificeTransformable
    Inherits ArtificeMoveable

#Region "Properties"

    Private WithEvents MyRotation As New LerpableSingle(0.0)
    ''' <summary>
    ''' The rotation (in degrees) of this object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Transfom"), Description("The rotation (in degrees) of this object"), DefaultValue(0.0)> _
    Public Property Rotation As LerpableSingle
        Get
            Return MyRotation
        End Get
        Set(value As LerpableSingle)
            Dim changed As Boolean = Not MyRotation.Equals(value)

            MyRotation = value

            If changed Then
                OnPropertyChanged("Rotation")
            End If
        End Set
    End Property

    Private WithEvents MyScaleX As New LerpableSingle(1.0)
    ''' <summary>
    ''' The scale of the x-coordinates
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Transfom"), DisplayName("X Scale"), Description("The scale of the x-coordinate"), DefaultValue(1.0)> _
    Public Property ScaleX As LerpableSingle
        Get
            Return MyScaleX
        End Get
        Set(value As LerpableSingle)
            Dim changed As Boolean = Not MyScaleX.Equals(value)

            MyScaleX = value

            If changed Then
                OnPropertyChanged("ScaleX")
            End If
        End Set
    End Property

    Private WithEvents MyScaleY As New LerpableSingle(1.0)
    ''' <summary>
    ''' The scale of the y-coordinates
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Transfom"), DisplayName("Y Scale"), Description("The scale of the y-coordinate"), DefaultValue(1.0)> _
    Public Property ScaleY As LerpableSingle
        Get
            Return MyScaleY
        End Get
        Set(value As LerpableSingle)
            Dim changed As Boolean = Not MyScaleY.Equals(value)

            MyScaleY = value

            If changed Then
                OnPropertyChanged("ScaleY")
            End If
        End Set
    End Property

#End Region

#Region "MyRotation_PropertyChanged"
    Private Sub MyRotation_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles MyRotation.PropertyChanged
        If e.PropertyName = "Value" Then OnPropertyChanged("Rotation")
    End Sub
#End Region

#Region "MyScaleX_PropertyChanged"
    Private Sub MyScaleX_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles MyScaleX.PropertyChanged
        If e.PropertyName = "Value" Then OnPropertyChanged("ScaleX")
    End Sub
#End Region

#Region "MyScaleY_PropertyChanged"
    Private Sub MyScaleY_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles MyScaleY.PropertyChanged
        If e.PropertyName = "Value" Then OnPropertyChanged("ScaleY")
    End Sub
#End Region

#Region "OnBeforeRender"
    Protected Overrides Sub OnBeforeRender(rc As RenderContext)
        MyBase.OnBeforeRender(rc)

        'Translate the graphics object by our scale and rotation values:
        rc.Graphics.ScaleTransform(IIf(ScaleX.Delta = 0, 0.000001, ScaleX.Delta), IIf(ScaleY.Delta = 0, 0.000001, ScaleY.Delta))
        rc.Graphics.RotateTransform(Rotation.Delta)
    End Sub
#End Region

End Class

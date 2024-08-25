''' <summary>
''' The abstract base class for all effects
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public MustInherit Class EffectBase
    Inherits ArtificeObject

#Region "Properties"
    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Effect
        End Get
    End Property
#End Region

    ''' <summary>
    ''' Called after animation of the current ArtificeObject has occurred
    ''' </summary>
    ''' <param name="ac"></param>
    ''' <param name="obj"></param>
    ''' <remarks></remarks>
    Public Overridable Shadows Sub OnAfterAnimate(ac As AnimationContext, obj As ArtificeObject)

    End Sub

    ''' <summary>
    ''' Called after rendering of the current ArtificeObject has occurred
    ''' </summary>
    ''' <param name="rc"></param>
    ''' <param name="obj"></param>
    ''' <remarks></remarks>
    Public Overridable Overloads Sub OnAfterRender(rc As RenderContext, obj As ArtificeObject)

    End Sub

    ''' <summary>
    ''' Called before animation of the current ArtificeObject has occurred
    ''' </summary>
    ''' <param name="ac"></param>
    ''' <param name="obj"></param>
    ''' <remarks></remarks>
    Public Overridable Sub OnBeforeAnimate(ac As AnimationContext, obj As ArtificeObject)

    End Sub

    ''' <summary>
    ''' Called before rendering of the current ArtificeObject has occurred
    ''' </summary>
    ''' <param name="rc"></param>
    ''' <param name="obj"></param>
    ''' <remarks></remarks>
    Public Overridable Overloads Sub OnBeforeRender(rc As RenderContext, obj As ArtificeObject)

    End Sub

    Public Overrides Sub Remove()
        If Parent Is Nothing Then Return

        'Effects are stored in the "Effects" collection, not the "Children" collection
        Parent.Effects.Remove(Me)
        Parent.OnPropertyChanged("Effects")
    End Sub

End Class

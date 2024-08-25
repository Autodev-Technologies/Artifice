Imports System.ComponentModel

''' <summary>
''' A special effect that occillates VectorPoints with each frame
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class PointSquiggle
    Inherits EffectBase

#Region "Properties"
    Private MySquiggleAmount As New LerpableSingle(3.0)
    <Category("Effect"), DisplayName("Squiggle Amount"), Description("The maximum distance from the original value the point will move to")> _
    Public Property SquiggleAmount As LerpableSingle
        Get
            Return MySquiggleAmount
        End Get
        Set(value As LerpableSingle)
            Dim changed As Boolean = Not value.Equals(MySquiggleAmount)

            MySquiggleAmount = value

            If changed Then
                OnPropertyChanged("SquiggleAmount")
            End If
        End Set
    End Property
#End Region

#Region "OnAfterAnimate"
    Public Overrides Sub OnAfterAnimate(ac As AnimationContext, obj As ArtificeObject)

        'After animation we squiggle the delta values

        'We only do the squiggling when we're playing or exporting, and we only squiggle VectorPoints:
        If (Designer.Playing OrElse Designer.Exporting) AndAlso obj.Is(Of VectorPoint)() Then
            'In the case of ellipses, we need to make the last point match the first point or else there will be a single jagged edge in our shape
            If obj.ParentIndex = obj.Parent.Children.Count - 1 AndAlso (obj.As(Of VectorPoint)().PointType And 3) = 3 Then
                Dim firstPoint As VectorPoint = obj.Parent.Children.First

                obj.As(Of VectorPoint)().X.Delta = firstPoint.X.Delta
                obj.As(Of VectorPoint)().Y.Delta = firstPoint.Y.Delta                
            Else
                'There is no rhyme or reason for why I chose Sin and Cos where I did; I just played around until
                'I liked the effect it caused. I'm sure some math genius out there can explain why.
                obj.As(Of VectorPoint)().X.Delta += SquiggleAmount.Delta * Math.Sin(obj.ParentIndex + ac.Frame)
                obj.As(Of VectorPoint)().Y.Delta += SquiggleAmount.Delta * Math.Cos(obj.ParentIndex + ac.Frame)
            End If
        End If

    End Sub
#End Region

End Class

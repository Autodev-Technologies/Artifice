''' <summary>
''' Represents an individual lerpable Integer value
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class LerpableInteger
    Inherits Lerpable(Of Integer)

#Region "Lerp"
    Public Overloads Overrides Sub Lerp(destination As Integer, lerpAmount As Single)
        Delta = Value + ((destination - Value) * lerpAmount)
    End Sub
#End Region

End Class

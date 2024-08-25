Imports System.ComponentModel
Imports System.Drawing.Design

''' <summary>
''' Represents an individual lerpable Single value
''' </summary>
''' <remarks>Created by Autodev</remarks>
<TypeConverter(GetType(LerpableSingleConverter))> _
Public Class LerpableSingle
    Inherits Lerpable(Of Single)

#Region "Constructor"
    Public Sub New()

    End Sub

    Public Sub New(initialValue As Single)
        MyBase.New(initialValue)
    End Sub
#End Region

#Region "Lerp"
    Public Overrides Sub Lerp(destination As Single, lerpAmount As Single)
        Delta = Value + ((destination - Value) * lerpAmount)
    End Sub
#End Region

#Region "ToString"
    Public Overrides Function ToString() As String
        Return Value.ToString()
    End Function
#End Region

End Class

''' <summary>
''' Handles conversion of LerpableSingles, used by the PropertyGrid control
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class LerpableSingleConverter
    Inherits ExpandableObjectConverter

    Public Overrides Function CanConvertFrom(context As ITypeDescriptorContext, sourceType As Type) As Boolean
        If sourceType Is GetType(String) OrElse sourceType Is GetType(Single) OrElse sourceType Is GetType(Double) OrElse sourceType Is GetType(Integer) Then
            Return True
        End If

        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overrides Function ConvertFrom(context As ITypeDescriptorContext, culture As Globalization.CultureInfo, value As Object) As Object
        If value.GetType Is GetType(String) Then
            Return New LerpableSingle(Single.Parse(value))
        ElseIf value.GetType Is GetType(Single) OrElse value.GetType Is GetType(Double) OrElse value.GetType Is GetType(Integer) Then
            Return New LerpableSingle(value)
        End If

        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overrides Function ConvertTo(context As ITypeDescriptorContext, culture As Globalization.CultureInfo, value As Object, destinationType As Type) As Object
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function
End Class

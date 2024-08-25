Imports System.ComponentModel

''' <summary>
''' The abstract generic class for all Lerpable properties
''' </summary>
''' <typeparam name="T"></typeparam>
''' <remarks>Created by Autodev</remarks>
<TypeConverter(GetType(ExpandableObjectConverter))> _
Public MustInherit Class Lerpable(Of T)
    Inherits PropertyChanger

#Region "Properties"
    Private MyDelta As T
    ''' <summary>
    ''' The delta value of this amount at any given frame of animation
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)> _
    Public Property Delta As T
        Get
            Return MyDelta
        End Get
        Set(value As T)
            If Not Lerpable Then value = Me.Value

            Dim changed As Boolean = Not value.Equals(MyDelta)

            MyDelta = value

            If changed Then
                OnPropertyChanged("Delta")
            End If
        End Set
    End Property

    Private MyLerpable As Boolean = True
    ''' <summary>
    ''' When True, this amount is linerally interpolated during animation
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Lerpable As Boolean
        Get
            Return MyLerpable
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = value <> MyLerpable

            MyLerpable = value

            If changed Then OnPropertyChanged("Lerpable")
        End Set
    End Property

    Private MyValue As T
    ''' <summary>
    ''' The keyframe value of this lerpable object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value As T
        Get
            Return MyValue
        End Get
        Set(value As T)
            Dim changed As Boolean = Not value.Equals(MyValue)

            MyValue = value
            Reset()

            If changed Then
                OnPropertyChanged("Value")
            End If
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Creates a new instance of this lerpable, setting it's value to initialValue
    ''' </summary>
    ''' <param name="initialValue"></param>
    ''' <remarks></remarks>
    Public Sub New(initialValue As T)
        Value = initialValue
    End Sub
#End Region

#Region "Copy"
    ''' <summary>
    ''' Copies the values from newValue into this Lerpable
    ''' </summary>
    ''' <param name="newValue">The value to copy</param>
    ''' <remarks></remarks>
    Public Sub Copy(newValue As Lerpable(Of T))
        Value = newValue.Delta
        Lerpable = newValue.Lerpable
    End Sub
#End Region

#Region "Equals"
    ''' <summary>
    ''' Determines whether this Lerpable is equal to the object being passed in
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(obj As Object) As Boolean
        If obj.GetType() IsNot Me.GetType() Then Return False

        Return Me.Value.Equals(obj.Value)
    End Function
#End Region

#Region "Lerp"
    ''' <summary>
    ''' Linearly interpolates this value against the destination value, storing it in Delta
    ''' </summary>
    ''' <param name="destination"></param>
    ''' <param name="lerpAmount"></param>
    ''' <remarks></remarks>
    Public Sub Lerp(destination As Lerpable(Of T), lerpAmount As Single)
        Lerp(destination.Value, lerpAmount)
    End Sub
#End Region

#Region "Lerp"
    ''' <summary>
    ''' Linearaly interpolates this value against the destination value, storing it in Delta
    ''' </summary>
    ''' <param name="destination"></param>
    ''' <param name="lerpAmount"></param>
    ''' <remarks></remarks>
    Public MustOverride Sub Lerp(destination As T, lerpAmount As Single)
#End Region

#Region "Reset"
    ''' <summary>
    ''' Resets Delta to Value
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub Reset()
        Delta = Value
    End Sub
#End Region

End Class

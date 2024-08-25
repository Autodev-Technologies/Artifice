Imports System.ComponentModel
Imports System.Drawing.Design

''' <summary>
''' Represents an individual lerpable Color value
''' </summary>
''' <remarks>Created by Autodev</remarks>
<Editor(GetType(LerpableColorEditor), GetType(UITypeEditor))> _
Public Class LerpableColor
    Inherits Lerpable(Of ArtificeColor)

#Region "Constructor"
    ''' <summary>
    ''' Constructs a new instance of ArtificeColor, defaulted to Black
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Value = New ArtificeColor(Color.Black)
    End Sub

    ''' <summary>
    ''' Contstructs as new instance of ArtificeColor using the provided System.Drawing.Color
    ''' </summary>
    ''' <param name="c">The color to initialize this ArtificeColor with</param>
    ''' <remarks></remarks>
    Public Sub New(c As Color)
        Value = New ArtificeColor(c)
    End Sub
#End Region

#Region "Lerp"
    Public Overloads Overrides Sub Lerp(destination As ArtificeColor, lerpAmount As Single)
        Delta = New ArtificeColor(Value.Alpha + ((destination.Alpha - Value.Alpha) * lerpAmount), _
        Value.Red + ((destination.Red - Value.Red) * lerpAmount), _
        Value.Green + ((destination.Green - Value.Green) * lerpAmount), _
        Value.Blue + ((destination.Blue - Value.Blue) * lerpAmount))
    End Sub
#End Region

#Region "Reset"
    Public Overrides Sub Reset()
        If Delta Is Nothing Then Delta = New ArtificeColor
        Delta.Color = Value.Color
    End Sub
#End Region

#Region "ToString"
    Public Overrides Function ToString() As String
        Return Value.Red & ", " & Value.Green & ", " & Value.Blue
    End Function
#End Region

End Class

''' <summary>
''' Defines the editing behaviours of ArtificeColor objects in PropertyGrid controls
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class LerpableColorEditor
    Inherits UITypeEditor

    Public Overrides Function EditValue(context As ITypeDescriptorContext, provider As IServiceProvider, value As Object) As Object
        Dim cp As New AdvancedColorPicker
        Dim lerp As LerpableColor = value

        cp.Color = lerp.Value

        If cp.ShowDialog(My.Application.OpenForms(0)) = DialogResult.OK Then
            lerp.Value = cp.Color
        End If

        Return lerp
    End Function

    Public Overrides Function GetEditStyle(context As ITypeDescriptorContext) As UITypeEditorEditStyle
        Return UITypeEditorEditStyle.Modal
    End Function

    Public Overrides Function GetPaintValueSupported(context As ITypeDescriptorContext) As Boolean
        Return True        
    End Function

    Public Overrides Sub PaintValue(e As PaintValueEventArgs)
        If e.Value Is Nothing Then Return

        Using br As New SolidBrush(CType(e.Value, LerpableColor).Value.Color)
            e.Graphics.FillRectangle(br, e.Bounds)
        End Using

        e.Graphics.DrawRectangle(Pens.Black, e.Bounds)
    End Sub

End Class
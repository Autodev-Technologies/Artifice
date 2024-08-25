Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.Windows.Forms.Design
Imports System.Drawing.Text

''' <summary>
''' A UITypeEditor that provides a dropdown of Font Families in the PropertyGrid control
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class FontFamilyEditor
    Inherits UITypeEditor

    Public Overrides Function GetEditStyle(context As System.ComponentModel.ITypeDescriptorContext) As UITypeEditorEditStyle
        Return UITypeEditorEditStyle.DropDown
    End Function

    Public Overrides Function EditValue(context As ITypeDescriptorContext, provider As IServiceProvider, value As Object) As Object
        Dim _editorService As IWindowsFormsEditorService = provider.GetService(GetType(IWindowsFormsEditorService))

        Dim lb As New ListBox()

        lb.SelectionMode = SelectionMode.One

        AddHandler lb.SelectedValueChanged, Sub(sender As Object, e As EventArgs)
                                                _editorService.CloseDropDown()
                                            End Sub

        Dim installedFontCollection As New InstalledFontCollection()

        lb.DisplayMember = "Name"

        For Each ff As FontFamily In installedFontCollection.Families
            lb.Items.Add(ff)
        Next

        _editorService.DropDownControl(lb)

        If lb.SelectedItem Is Nothing Then Return value

        Return lb.SelectedItem
    End Function

End Class

''' <summary>
''' Handles conversion between String and FontFamily
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class FontFamilyTypeConverter
    Inherits TypeConverter

    Public Overrides Function CanConvertFrom(context As ITypeDescriptorContext, sourceType As Type) As Boolean
        Return sourceType Is GetType(String)
    End Function

    Public Overrides Function CanConvertTo(context As ITypeDescriptorContext, destinationType As Type) As Boolean
        Return destinationType Is GetType(FontFamily)
    End Function

    Public Overrides Function ConvertTo(context As ITypeDescriptorContext, culture As Globalization.CultureInfo, value As Object, destinationType As Type) As Object
        If destinationType Is GetType(String) Then
            Return CType(value, FontFamily).Name
        End If

        Return Nothing
    End Function

    Public Overrides Function ConvertFrom(context As ITypeDescriptorContext, culture As Globalization.CultureInfo, value As Object) As Object
        If value.GetType() Is GetType(String) Then
            Return New FontFamily(CStr(value))
        End If

        Return Nothing
    End Function

End Class

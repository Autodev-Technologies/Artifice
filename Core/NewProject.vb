''' <summary>
''' The dialog box for creating new Artifice projects
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class NewProject

    Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
        If ProjectName.Text.Trim() = "" Then
            MsgBox("You must provide a project name")
            ProjectName.Focus()
            Return
        End If

        DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If FormatSize.Text = "A4" Then
            StageWidth.Value = 793
            StageHeight.Value = 1122
        ElseIf FormatSize.Text = "Letter" Then
            StageWidth.Value = 816
            StageHeight.Value = 1056
        ElseIf FormatSize.Text = "Postcard" Then
            StageWidth.Value = 384
            StageHeight.Value = 746
        ElseIf FormatSize.Text = "Poster" Then
            StageWidth.Value = 1728
            StageHeight.Value = 2304
        ElseIf FormatSize.Text = "Legal" Then
            StageWidth.Value = 1344
            StageHeight.Value = 816
        ElseIf FormatSize.Text = "Tabloid" Then
            StageWidth.Value = 1632
            StageHeight.Value = 1056
        ElseIf FormatSize.Text = "A3" Then
            StageWidth.Value = 853
            StageHeight.Value = 640
        ElseIf FormatSize.Text = "B5" Then
            StageWidth.Value = 944
            StageHeight.Value = 665
        ElseIf FormatSize.Text = "B4" Then
            StageWidth.Value = 1375
            StageHeight.Value = 971
        ElseIf FormatSize.Text = "A5" Then
            StageWidth.Value = 559
            StageHeight.Value = 793
        ElseIf FormatSize.Text = "A3" Then
            StageWidth.Value = 559
            StageHeight.Value = 793
        End If
    End Sub
End Class
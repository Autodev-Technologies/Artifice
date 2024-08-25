''' <summary>
''' The simple About Artifice dialog box
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class AboutArtifice

    Private Sub AboutArtifice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
    End Sub

    Private Sub button2_Click(sender As Object, e As EventArgs) Handles button2.Click
        panel1.Visible = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        panel1.Visible = False
    End Sub
End Class
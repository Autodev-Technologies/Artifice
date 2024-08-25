Public Class Splash
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(2)
        If ProgressBar1.Value >= 100 Then
            Timer1.Enabled = False
            Dim FrmMain As New MainWindow
            FrmMain.Show()
            Me.Hide()
        End If
    End Sub
End Class
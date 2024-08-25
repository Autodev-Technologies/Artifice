''' <summary>
''' A button for displaying the default colors for fill and line in a ToolstripContainer
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class ToolStripColorButton
    Inherits ToolStripBindableButton

    Public Enum DisplayMode
        Fill 'A large rectangle
        Line 'A thin rectangle (representing LineColor)
    End Enum

    Public Event ColorChanged As EventHandler
    Public Event DisplayChanged As EventHandler

#Region "Properties"
    Private MyColor As Color = Drawing.Color.Blue
    Public Property Color As Color
        Get
            Return MyColor
        End Get
        Set(value As Color)
            Dim changed As Boolean = Not value.Equals(MyColor)

            MyColor = value

            If changed Then
                RaiseEvent ColorChanged(Me, EventArgs.Empty)
                If Owner IsNot Nothing Then Me.Owner.Refresh()
            End If
        End Set
    End Property

    Private MyDisplayMode As DisplayMode = DisplayMode.Fill
    Public Property Display As DisplayMode
        Get
            Return MyDisplayMode
        End Get
        Set(value As DisplayMode)
            Dim changed As Boolean = value <> MyDisplayMode

            MyDisplayMode = value

            If changed Then
                RaiseEvent DisplayChanged(Me, EventArgs.Empty)
                If Owner IsNot Nothing Then Me.Owner.Refresh()
            End If
        End Set
    End Property
#End Region

#Region "OnPaint"
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim b As New Rectangle(4, 4, Bounds.Width - 8, Bounds.Height - 8)

        If Display = DisplayMode.Line Then
            b = New Rectangle(4, Bounds.Height \ 2 - 2, Bounds.Width - 8, 4)
        End If

        Dim br As New SolidBrush(Color)

        e.Graphics.FillRectangle(br, b)

        br.Dispose()

        If Display = DisplayMode.Fill Then
            e.Graphics.DrawRectangle(Pens.Black, b)
        End If
    End Sub
#End Region

End Class

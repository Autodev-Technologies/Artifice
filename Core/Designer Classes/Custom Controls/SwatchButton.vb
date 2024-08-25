''' <summary>
''' A single swatch color in the AdvancedColorPicker dialog
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class SwatchButton

    Public Event ArtificeColorChanged As EventHandler

#Region "Properties"
    Private MyArtificeColor As ArtificeColor
    ''' <summary>
    ''' The ArtificeColor this swatch records
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ArtificeColor As ArtificeColor
        Get
            Return MyArtificeColor
        End Get
        Set(value As ArtificeColor)
            Dim changed As Boolean = Not value.Equals(MyArtificeColor)

            MyArtificeColor = value

            If changed Then
                RaiseEvent ArtificeColorChanged(Me, EventArgs.Empty)
                Refresh()
            End If
        End Set
    End Property
#End Region

#Region "OnPaint"
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        If ArtificeColor Is Nothing Then

            'If there's no ArtificeColor, then this is the "Add" swatch. Draw the green "plus" sign:
            e.Graphics.FillRectangle(Brushes.LightGreen, New Rectangle(Width / 2 - 2, 4, 4, ClientRectangle.Height - 8))
            e.Graphics.FillRectangle(Brushes.LightGreen, New Rectangle(4, Height / 2 - 2, ClientRectangle.Width - 8, 4))

            ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Flat)
        Else
            Dim br As Brush = ArtificeColor.GetBrush

            e.Graphics.FillRectangle(br, New Rectangle(0, 0, Width, Height))

            br.Dispose()

            ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Adjust)
        End If
    End Sub
#End Region

End Class

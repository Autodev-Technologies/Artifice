''' <summary>
''' A generic dialog box for selecting child nodes of a ArtificeObject
''' </summary>
''' <remarks></remarks>
Public Class ChildSelector

#Region "Properties"
    Private MyParentObject As ArtificeObject = Nothing
    ''' <summary>
    ''' The ArtificeObject whose children we are going to choose from
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ParentObject As ArtificeObject
        Get
            Return MyParentObject
        End Get
        Set(value As ArtificeObject)
            Dim changed As Boolean = value IsNot MyParentObject

            MyParentObject = value

            If changed Then
                If MyParentObject Is Nothing Then
                    ChildList.Items.Clear()
                Else
                    ChildList.Items.AddRange(MyParentObject.Children)
                End If
            End If
        End Set
    End Property
#End Region

#Region "ChildList_DrawItem"
    Private Sub ChildList_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ChildList.DrawItem
        'Draw the default background for a list item:
        e.DrawBackground()

        Dim vo As ArtificeObject = ChildList.Items(e.Index)
        Dim sf As New StringFormat

        sf.LineAlignment = StringAlignment.Center

        'Draw the ArtificeObject's icon:
        e.Graphics.DrawImage(vo.Icon, New PointF(e.Bounds.Left + 3, e.Bounds.Top + (e.Bounds.Height - vo.Icon.Height) / 2))

        'Draw it's name:
        Dim br As New SolidBrush(e.ForeColor)
        e.Graphics.DrawString(vo.Name, e.Font, br, New RectangleF(e.Bounds.Left + 3 + vo.Icon.Width + 3, e.Bounds.Top, e.Bounds.Width - (e.Bounds.Left + 3 + vo.Icon.Width + 3), e.Bounds.Height), sf)
        br.Dispose()

        e.DrawFocusRectangle()
    End Sub
#End Region

#Region "ChildList_SelectedIndexChanged"
    Private Sub ChildList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChildList.SelectedIndexChanged
        OkButton.Enabled = ChildList.SelectedIndex >= 0
    End Sub
#End Region

End Class
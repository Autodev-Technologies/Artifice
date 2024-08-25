''' <summary>
''' The tool for adding RasterImage Artifice objects to the scene
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class ImageTool
    Inherits ToolBase

#Region "Properties"
    Public Overrides ReadOnly Property Description As String
        Get
            Return "Adds a raster image to the keyframe."
        End Get
    End Property

    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Image
        End Get
    End Property

    Public Overrides ReadOnly Property Index As Integer
        Get
            Return 9
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "Raster Image"
        End Get
    End Property
#End Region

#Region "OnMouseClick"
    Public Overrides Sub OnMouseClick(e As ToolMouseEvent)
        Dim ofd As New OpenFileDialog()

        ofd.Filter = "All Images|*.bmp;*.jpg;*.gif;*.png;*.jpeg|All Files|*.*"

        If ofd.ShowDialog(My.Application.OpenForms(0)) <> DialogResult.OK Then
            'If they cancel out of the dialog box, exit:
            Return
        End If

        Try
            'Since we might be adding a keyframe, start an undo batch:
            Designer.StartUndoBatch()

            If Designer.SelectedContainer Is Nothing Then
                Designer.SelectedContainer = Designer.SelectedLayer.As(Of Layer)().AddKeyFrame(Designer.SelectedScene.Frame)
                Designer.AddUndo(New ObjectAddedUndo(Designer.SelectedContainer))
            End If

            'Verify that it's an actual image (an exception will be thrown if it isn't):
            Dim testImage As Image = Image.FromFile(ofd.FileName)

            testImage.Dispose()

            'Create our new RasterImage object:
            Dim ri As New RasterImage

            ri.ImageFile = ofd.FileName

            ri.X.Value = e.Location.X
            ri.Y.Value = e.Location.Y

            'Add our image to the selected keyframe:
            Designer.SelectedContainer.AddChild(ri)

            'Make our new object the selected object:
            Designer.SelectedObject = ri

            'Add our new object to the undo batch:
            Designer.AddUndo(New ObjectAddedUndo(ri))

            'Reset the selected tool to the Arrow tool:
            Designer.SelectedTool = New ArrowTool

            'Close our undo batch:
            Designer.EndUndoBatch()
        Catch ex As Exception
            MsgBox("Selected file is not a valid image." & vbNewLine & "Error: " & ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkCancel, "Artifice")
        End Try
    End Sub
#End Region

End Class

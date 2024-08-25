''' <summary>
''' The tool for adding text to the scene
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class TextTool
    Inherits ToolBase

#Region "Properties"
    Public Overrides ReadOnly Property Description As String
        Get
            Return "Adds text to the current keyframe."
        End Get
    End Property

    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Text
        End Get
    End Property

    Public Overrides ReadOnly Property Index As Integer
        Get
            Return 7
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "Text"
        End Get
    End Property
#End Region

#Region "OnMouseClick"
    Public Overrides Sub OnMouseClick(e As ToolMouseEvent)
        Dim NewText = New Text()

        'Create a new keyframe if there isn't one for this layer:
        If Designer.SelectedContainer Is Nothing Then
            Designer.SelectedContainer = Designer.SelectedLayer.As(Of Layer)().AddKeyFrame(Designer.SelectedScene.Frame)
        End If

        'Set line & fill colours based on the current designer globals:
        NewText.LineColor.Value = New ArtificeColor(Designer.LineColor)
        NewText.FillColor.Value = New ArtificeColor(Designer.FillColor)
        NewText.LineWidth.Value = Designer.LineWidth

        'Center the text on the location of the click:
        NewText.X.Value = e.Location.X
        NewText.Y.Value = e.Location.Y

        Designer.SelectedContainer.AddChild(NewText)

        'Make this object the selected object:
        Designer.SelectedObject = NewText

        'Record this object added in the undo buffer:
        Designer.AddUndo(New ObjectAddedUndo(NewText))

        'Reset the tool to Arrow:
        Designer.SelectedTool = New ArrowTool
    End Sub
#End Region

End Class

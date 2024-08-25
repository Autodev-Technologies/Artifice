''' <summary>
''' The tool for adding subscenes
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class SubSceneTool
    Inherits ToolBase

#Region "Properties"
    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Subscene
        End Get
    End Property

    Public Overrides ReadOnly Property Index As Integer
        Get
            Return 8
        End Get
    End Property

    Public Overrides ReadOnly Property Name As String
        Get
            Return "Subscene"
        End Get
    End Property

    Public Overrides ReadOnly Property Description As String
        Get
            Return "Allows you to play a scene within a scene."
        End Get
    End Property
#End Region

#Region "OnMouseClick"
    Public Overrides Sub OnMouseClick(e As ToolMouseEvent)
        Dim ss As New Subscene()

        'TODO: prompt for subscene selection?

        'Create a keyframe if none exist in this layer:
        If Designer.SelectedContainer Is Nothing Then
            Designer.SelectedContainer = Designer.SelectedLayer.As(Of Layer)().AddKeyFrame(Designer.SelectedScene.Frame)
        End If

        ss.X.Value = e.Location.X
        ss.Y.Value = e.Location.Y

        'I set the scale of the new scene to 25% so as not to make it fill the whole stage immediately:
        ss.ScaleX.Value = 0.25
        ss.ScaleY.Value = 0.25

        Designer.SelectedContainer.AddChild(ss)

        Designer.SelectedObject = ss

        'Record the new object addition:
        Designer.AddUndo(New ObjectAddedUndo(ss))

        'Reset the designer to the arrow tool:
        Designer.SelectedTool = New ArrowTool()
    End Sub
#End Region

End Class

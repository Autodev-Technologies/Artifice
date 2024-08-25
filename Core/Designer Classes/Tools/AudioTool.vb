'''' <summary>
'''' The tool for placing Audio Artifice objects into a scene
'''' </summary>
'''' <remarks>Created by Autodev</remarks>
'Public Class AudioTool
'    Inherits ToolBase

'#Region "Properties"
'    Public Overrides ReadOnly Property Description As String
'        Get
'            Return "Adds a sound object to the current keyframe."
'        End Get
'    End Property

'    Public Overrides ReadOnly Property Icon As Image
'        Get
'            Return My.Resources.Audio
'        End Get
'    End Property

'    Public Overrides ReadOnly Property Index As Integer
'        Get
'            Return 4
'        End Get
'    End Property

'    Public Overrides ReadOnly Property Name As String
'        Get
'            Return "Sound"
'        End Get
'    End Property
'#End Region

'#Region "OnMouseClick"
'    Public Overrides Sub OnMouseClick(e As ToolMouseEvent)
'        'Prompt for the file to load:
'        Dim ofd As New OpenFileDialog()

'        ofd.Filter = "Audio Files|*.wav;*.mp3|WAV Files|*.wav|MP3 Files|*.mp3|All Files|*.*"
'        ofd.Title = "Select an audio file to add..."

'        If ofd.ShowDialog(My.Application.OpenForms(0)) <> DialogResult.OK Then
'            'If they cancelled out of ofd, just leave the function without doing anything else
'            Return
'        End If

'        'Because we might be adding a keyframe, we'll open an undo batch:
'        Designer.StartUndoBatch()

'        'If there's no keyframe for this layer at the current frame, add one:
'        If Designer.SelectedContainer Is Nothing Then
'            Dim kf As KeyFrame = Designer.SelectedLayer.As(Of Layer)().AddKeyFrame(Designer.SelectedScene.Frame)
'            'Make the new keyframe the currently-selected one:
'            Designer.SelectedContainer = kf
'            'Record the adding of this keyframe to the undo buffer:
'            Designer.AddUndo(New ObjectAddedUndo(kf))
'        End If

'        'Create our new Sound object, and place it at the location the mouse was clicked:
'        Dim newObject = New Sound

'        newObject.X.Value = e.Location.X
'        newObject.Y.Value = e.Location.Y
'        newObject.AudioFile = ofd.FileName

'        'Add it to the current keyframe:
'        Designer.SelectedContainer.AddChild(newObject)

'        'Make it the selected object:
'        Designer.SelectedObject = newObject

'        'Change the designer's selected tool to the ArrowTool:
'        Designer.SelectedTool = New ArrowTool

'        'Record the addition of this object to the undo buffer:
'        Designer.AddUndo(New ObjectAddedUndo(newObject))

'        'Close the undo batch:
'        Designer.EndUndoBatch()
'    End Sub
'#End Region

'End Class

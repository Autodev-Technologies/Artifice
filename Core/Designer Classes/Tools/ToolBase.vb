''' <summary>
''' The base class for all tool objects
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public MustInherit Class ToolBase

#Region "Properties"
    ''' <summary>
    ''' The cursor to display when this control is selected
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property Cursor As Cursor
        Get
            Return Cursors.Arrow
        End Get
    End Property

    ''' <summary>
    ''' The help text that appears as the tooltip text for the control and the help text in the status bar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property Description As String
        Get
            Return "Description goes here"
        End Get
    End Property

    ''' <summary>
    ''' The singleton instance of the Designer class
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected ReadOnly Property Designer As Designer
        Get
            Return Global.Artifice.Designer.Singleton
        End Get
    End Property

    ''' <summary>
    ''' The image to display for this tool in the toolbar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property Icon As Image
        Get
            Return My.Resources.Arrow
        End Get
    End Property

    ''' <summary>
    ''' The display order of this tool in the toolbar. Also doubles as the hotkey to select the tool
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public MustOverride ReadOnly Property Index As Integer

    Public Overridable ReadOnly Property ShortcutKey As Keys
        Get
            Return Keys.None
        End Get
    End Property

    ''' <summary>
    ''' The display name of this tool
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public MustOverride ReadOnly Property Name As String
#End Region

#Region "CreateNewObject"
    ''' <summary>
    ''' Creates a new instance of VectorObject at the given location
    ''' </summary>
    ''' <param name="location">The world-space location to create the object at</param>
    ''' <returns>The new VectorObject</returns>
    ''' <remarks></remarks>
    Protected Function CreateNewObject(location As Point) As VectorObject
        Dim NewObject = New VectorObject

        'Because we might be adding a keyframe (in addition to a new object), let's start an undo batch:
        Designer.StartUndoBatch()

        'If there's no keyframe for the currently selected frame, let's create one:
        If Designer.SelectedContainer Is Nothing Then
            Dim kf As KeyFrame = Designer.SelectedLayer.As(Of Layer)().AddKeyFrame(Designer.SelectedScene.Frame)
            Designer.SelectedContainer = kf
            Designer.AddUndo(New ObjectAddedUndo(kf))
        End If

        'Prime the new object with the currently-selected colours in the designer:
        NewObject.LineColor.Value = New ArtificeColor(Designer.LineColor)
        NewObject.FillColor.Value = New ArtificeColor(Designer.FillColor)
        NewObject.LineWidth.Value = Designer.LineWidth
        NewObject.X.Value = location.X
        NewObject.Y.Value = location.Y

        'Add the new object to the selected keyframe:
        Designer.SelectedContainer.AddChild(NewObject)

        'Select this object:
        Designer.SelectedObject = NewObject

        'Record the object's addition in the undo buffer:
        Designer.AddUndo(New ObjectAddedUndo(NewObject))

        'Close our undo batch:
        Designer.EndUndoBatch()

        'Return our object
        Return NewObject
    End Function
#End Region

#Region "OnKeyDown"
    ''' <summary>
    ''' Receives KeyDown events from the SceneSurface control
    ''' </summary>
    ''' <param name="surface">The surface receiving the original events</param>
    ''' <param name="e">The KeyEventArgs from the original event</param>
    ''' <remarks></remarks>
    Public Overridable Sub OnKeyDown(surface As SceneSurface, e As KeyEventArgs)

    End Sub
#End Region

#Region "OnKeyUp"
    ''' <summary>
    ''' Receives KeyUp events from the SceneSurface control
    ''' </summary>
    ''' <param name="surface">The surface receiving the original events</param>
    ''' <param name="e">The KeyEventArgs from the original event</param>
    ''' <remarks></remarks>
    Public Overridable Sub OnKeyUp(surface As SceneSurface, e As KeyEventArgs)

    End Sub
#End Region

#Region "OnMouseClick"
    ''' <summary>
    ''' Receives MouseClick events from the SceneSurface control
    ''' </summary>
    ''' <param name="e">The ToolMouseEvent instance containing information about the event</param>
    ''' <remarks></remarks>
    Public Overridable Sub OnMouseClick(e As ToolMouseEvent)

    End Sub
#End Region

#Region "OnMouseDoubleClick"
    ''' <summary>
    ''' Receives MouseDoubleClick events from the SceneSurface control
    ''' </summary>
    ''' <param name="e">The ToolMouseEvent instance containing information about the event</param>
    ''' <remarks></remarks>
    Public Overridable Sub OnMouseDoubleClick(e As ToolMouseEvent)

    End Sub
#End Region

#Region "OnMouseDown"
    ''' <summary>
    ''' Receives MouseDown events from the SceneSurface control
    ''' </summary>
    ''' <param name="e">The ToolMouseEvent instance containing information about the event</param>
    ''' <remarks></remarks>
    Public Overridable Sub OnMouseDown(e As ToolMouseEvent)

    End Sub
#End Region

#Region "OnMouseMove"
    ''' <summary>
    ''' Receives MouseMove events from the SceneSurface control
    ''' </summary>
    ''' <param name="e">The ToolMouseEvent instance containing information about the event</param>
    ''' <remarks></remarks>
    Public Overridable Sub OnMouseMove(e As ToolMouseEvent)

    End Sub
#End Region

#Region "MouseUp"
    ''' <summary>
    ''' Receives MouseUp events from the SceneSurface control
    ''' </summary>
    ''' <param name="e">The ToolMouseEvent instance containing information about the event</param>
    ''' <remarks></remarks>
    Public Overridable Sub OnMouseUp(e As ToolMouseEvent)

    End Sub
#End Region

End Class


''' <summary>
''' Represents an object containing mouse event information from a SceneSurface object passed to an object derived from ToolBase
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class ToolMouseEvent
    Inherits EventArgs

#Region "Properties"

    Private MyActiveObject As ArtificeMoveable
    ''' <summary>
    ''' Returns the ArtificeObject at the provided device coordinates; Nothing otherwise
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ActiveObject As ArtificeMoveable
        Get
            Return MyActiveObject
        End Get
    End Property

    Public ReadOnly Property AltKeyDown As Boolean
        Get
            Return (Control.ModifierKeys And Keys.Alt) = Keys.Alt
        End Get
    End Property

    Private MyControlLocation As Point
    ''' <summary>
    ''' The mouse location, in device (control) units
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ControlLocation As Point
        Get
            Return MyControlLocation
        End Get
    End Property

    ''' <summary>
    ''' Returns True if a CONTROL key is down, False otherwise
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ControlKeyDown As Boolean
        Get
            Return (Control.ModifierKeys And Keys.Control) = Keys.Control
        End Get
    End Property

    ''' <summary>
    ''' Returns the singleton instance of the Designer class
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected ReadOnly Property Designer As Designer
        Get
            Return Global.Artifice.Designer.Singleton
        End Get
    End Property

    Private MyLocation As Point
    ''' <summary>
    ''' The mouse location in world units, relative to the currently selected container's origin
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Location As Point
        Get
            Return MyLocation
        End Get
    End Property

    Private MyMouse As MouseEventArgs
    ''' <summary>
    ''' The instance of MouseEventArgs that was passed into the original SceneSurface mouse event handlers
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Mouse As MouseEventArgs
        Get
            Return MyMouse
        End Get
    End Property

    Private MyObjectLocation As Point
    ''' <summary>
    ''' The mouse coordinates in world units, relative to ActiveObject
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ObjectLocation As Point
        Get
            Return MyObjectLocation
        End Get
    End Property

    ''' <summary>
    ''' Returns True if a SHIFT key is down; false otherwise
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ShiftKeyDown As Boolean
        Get
            Return (Control.ModifierKeys And Keys.Shift) = Keys.Shift
        End Get
    End Property

    Private MySurface As SceneSurface
    ''' <summary>
    ''' The instance of SceneSurface where the mouse events originated from
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Surface As SceneSurface
        Get
            Return MySurface
        End Get
    End Property
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Creates a new instance of ToolMouseEvents and initializes all the members based on the passed-in SceneSurface and control coordinates (in device units)
    ''' </summary>
    ''' <param name="surface">The SceneSurface control where the original mouse event originated from</param>
    ''' <param name="e">The MouseEventArgs from the original mouse events on the SceneSurface</param>
    ''' <remarks></remarks>
    Public Sub New(surface As SceneSurface, e As MouseEventArgs)
        'Create a new device graphices instace for this surface;
        'we'll use this to calculate our locations and perform hit tests 
        'on the Artifice DOM:
        Dim g As Graphics = surface.CreateGraphics

        'These are our easy properties to set because they're just references to the ones passed in:
        MySurface = surface
        MyControlLocation = e.Location
        MyMouse = e

        'Create a new render context for the current frame and our graphics object:
        Dim rc As New RenderContext(Designer.SelectedScene.Frame, g)

        'Let the surface provide it's transformations (zooming and panning):
        surface.BeforeRender(rc)

        'If we have a selected container, do the coordinate translation relative to it:
        If Designer.SelectedContainer IsNot Nothing Then
            MyLocation = Designer.SelectedContainer.Translate(ControlLocation, rc)
        Else
            'Translate our coorindates relative to the entire scene:
            MyLocation = Designer.SelectedScene.Translate(ControlLocation, rc)
        End If

        'Default object location to the control's location (I can't remember why I do this)
        MyObjectLocation = ControlLocation
        
        If Designer.SelectedContainer IsNot Nothing Then
            'If we have a selected container, perform a hit test relative to the closest KeyFrame to it:
            MyActiveObject = Designer.SelectedContainer.GetClosest(Of KeyFrame)().HitTest(ControlLocation.X, ControlLocation.Y, rc, MyObjectLocation)

            ' This made the status text equal to whatever object I hit tested positive on (mostly for debugging purposes):
            'If MyActiveObject IsNot Nothing Then
            '    CType(surface.FindForm, MainWindow).StatusInformation.Text = MyActiveObject.ToString()
            'End If
        End If

        'If we have an actual object:
        If MyActiveObject IsNot Nothing Then
            'Make ObjectLocation equal to the x, y coordinates of the object in world units:
            MyObjectLocation = ActiveObject.Translate(ControlLocation, rc)

            'If the active object is a child of a ArtificeMovable object, change Location to be relative to the parent's tranformation:
            If MyActiveObject.Parent IsNot Nothing AndAlso MyActiveObject.Parent.Is(Of ArtificeMoveable)() Then
                MyLocation = MyActiveObject.Parent.Translate(ControlLocation, rc)
            End If
        End If

        'Let the SceneSurface perform any cleanup:
        surface.AfterRender(rc)

        ' ALWAYS dispose of graphics objects created with Control.CreateGraphics:
        g.Dispose()
    End Sub
#End Region

End Class

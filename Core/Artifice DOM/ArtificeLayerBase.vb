Imports System.ComponentModel

''' <summary>
''' The abstract base class for Layers and LayerGroups (folders)
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public MustInherit Class ArtificeLayerBase
    Inherits ArtificeObject

#Region "Properties"
    Private MyLocked As Boolean = False
    ''' <summary>
    ''' Determines if this layer (or its child layers) are to be locked from editing in the designer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Design"), Description("Determines if objects on this layer are locked from editing")> _
    Public Property Locked As Boolean
        Get
            Return MyLocked
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = MyLocked <> value

            MyLocked = value

            If changed Then OnPropertyChanged("Locked")
        End Set
    End Property

    ''' <summary>
    ''' Returns the maximum frame number in this layer
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)> _
    Public Overridable ReadOnly Property TotalFrames As Integer
        Get
            Dim lastFrame As Integer = 1

            For Each l As ArtificeLayerBase In Children
                lastFrame = Math.Max(lastFrame, l.TotalFrames)
            Next

            Return lastFrame
        End Get
    End Property

    Private MyVisible As Boolean = True
    ''' <summary>
    ''' Determines if this layer (or its child layers) are to be rendered
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Design"), Description("Determines if this layer (or its children) are rendered")> _
    Public Property Visible As Boolean
        Get
            'If Parent.GetType().IsSubclassOf(GetType(ArtificeLayerBase)) Then
            '    Return CType(Parent, ArtificeLayerBase).Visible AndAlso MyVisible
            'End If

            Return MyVisible
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = MyVisible <> value

            MyVisible = value

            If changed Then OnPropertyChanged("Visible")
        End Set
    End Property
#End Region

#Region "HasKeyFramesAfter"
    ''' <summary>
    ''' Returns True if this layer has KeyFrames after this given frame, False otherwise
    ''' </summary>
    ''' <param name="frame">The frame to check against</param>
    ''' <returns></returns>
    ''' <remarks>Handled by dervied classes</remarks>
    Public Overridable Function HasKeyFramesAfter(frame As Integer) As Boolean
        Return False
    End Function
#End Region

#Region "OnExportAudio"
    Protected Overrides Sub OnExportAudio(context As ExportAudioContext)
        'Only export if visible:
        If Not Visible Then Return

        MyBase.OnExportAudio(context)
    End Sub
#End Region

#Region "OnHitTest"
    Protected Overrides Function OnHitTest(x As Single, y As Single, rc As RenderContext, ByRef transformedLocation As Point) As ArtificeMoveable
        'Only perform the hit test if we're visible and not locked:
        If Visible AndAlso Not Locked Then Return MyBase.OnHitTest(x, y, rc, transformedLocation)

        Return Nothing
    End Function
#End Region

#Region "OnLoad"
    Protected Overrides Sub OnLoad(el As XElement)
        MyBase.OnLoad(el)

        Locked = Boolean.Parse(el.@Locked)
        Visible = Boolean.Parse(el.@Visible)
    End Sub
#End Region

#Region "OnRender"
    Protected Overrides Sub OnRender(rc As RenderContext)
        'Only render if we're visible:
        If Visible Then MyBase.OnRender(rc)
    End Sub
#End Region

#Region "OnSave"
    Protected Overrides Sub OnSave(el As XElement)
        MyBase.OnSave(el)

        el.@Locked = Locked.ToString()
        el.@Visible = Visible.ToString()
    End Sub
#End Region

#Region "ONSaveAsSVG"
    Protected Overrides Function OnSaveAsSVG() As XElement()
        'Only export our contents to SVG if we're visible:
        If Visible Then Return MyBase.OnSaveAsSVG()

        Return New XElement() {}
    End Function
#End Region

End Class

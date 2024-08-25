Imports System.ComponentModel

''' <summary>
''' Represents a layer (which contains keyframes)
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class Layer
    Inherits ArtificeLayerBase

#Region "Properties"

    <Browsable(False)> _
    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Layer
        End Get
    End Property

    Public Overrides ReadOnly Property TotalFrames As Integer
        Get
            If Not Children.Any Then Return 0

            Return Children.Last.As(Of KeyFrame).Frame
        End Get
    End Property

#End Region

#Region "AddKeyFrame"
    ''' <summary>
    ''' Add's a new KeyFrame for the given frame
    ''' </summary>
    ''' <param name="frame">The frame to add the keyframe to</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddKeyFrame(frame As Integer) As KeyFrame
        Dim newKf As New KeyFrame()
        Dim pkf As KeyFrame = GetKeyFrame(frame)

        newKf.Frame = frame

        If pkf IsNot Nothing Then
            For Each child As ArtificeObject In pkf.Children
                newKf.AddChild(child.Clone())
            Next
        End If

        AddChild(newKf)

        Return newKf
    End Function
#End Region

#Region "GetKeyFrame"
    ''' <summary>
    ''' Returns the applicable KeyFrame for the given frame
    ''' </summary>
    ''' <param name="frame">The frame to retrieve the KeyFrame for</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKeyFrame(frame As Integer) As KeyFrame
        Dim found As KeyFrame = Nothing

        'Find the KeyFrame that best matches this frame (the KeyFrame on or before this frame):
        For Each kf As KeyFrame In Children
            If kf.Frame <= frame AndAlso (kf.Next() Is Nothing OrElse CType(kf.Next(), KeyFrame).Frame > frame) Then
                found = kf
                Exit For
            End If
        Next

        Return found
    End Function
#End Region

#Region "HasKeyFramesAfter"
    ''' <summary>
    ''' Returns True if there are KeyFrames after the given frame, False otherwise.
    ''' </summary>
    ''' <param name="frame">The frame to test against</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function HasKeyFramesAfter(frame As Integer) As Boolean
        If ChildrenList.Count = 0 Then Return False

        Return ChildrenList.Last.As(Of KeyFrame)().Frame > frame
    End Function
#End Region

#Region "OnHitTest"
    Protected Overrides Function OnHitTest(x As Single, y As Single, rc As RenderContext, ByRef transformedLocation As Point) As ArtificeMoveable
        'Only do this if we're visible and not locked:
        If Not Visible OrElse Locked Then Return Nothing

        Dim kf As KeyFrame = GetKeyFrame(rc.Frame)

        'If there's no valid keyframe for this frame, there's no point in running the test:
        If kf IsNot Nothing Then Return kf.HitTest(x, y, rc, transformedLocation)

        Return Nothing
    End Function
#End Region

#Region "OnChildAdded"
    Protected Overrides Sub OnChildAdded()
        MyBase.OnChildAdded()

        'Reorder the children by frame number:
        ChildrenList.Sort(Function(x, y) CType(x, KeyFrame).Frame.CompareTo(CType(y, KeyFrame).Frame))
    End Sub
#End Region

#Region "OnExportAudio"
    Protected Overrides Sub OnExportAudio(context As ExportAudioContext)
        Dim kf As KeyFrame = GetKeyFrame(context.Frame)

        'Only do this export if we have a keyframe matching the given frame:
        If kf IsNot Nothing Then
            kf.ExportAudio(context)
        End If
    End Sub
#End Region

#Region "OnRender"
    Protected Overrides Sub OnRender(rc As RenderContext)
        'Only render if we're visible:
        If Visible Then
            Dim kf As KeyFrame = GetKeyFrame(rc.Frame)

            If kf IsNot Nothing Then
                kf.Render(rc)
            End If
        End If
    End Sub
#End Region

#Region "OnSaveAsSVG"
    Protected Overrides Function OnSaveAsSVG() As XElement()
        'Export as an SVG Group:
        Dim g As XElement = <g id=<%= "l" & Id %>/>

        g.Add(MyBase.OnSaveAsSVG())

        Return New XElement() {g}
    End Function
#End Region

End Class

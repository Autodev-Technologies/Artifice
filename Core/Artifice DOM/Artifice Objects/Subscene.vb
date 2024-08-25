Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing.Design

''' <summary>
''' A visual object that contains a Scene object and can play a Scene within a Scene
''' </summary>
''' <remarks></remarks>
Public Class Subscene
    Inherits ArtificeTransformable

    'We're going to store the SceneId of the scene we display, because that's what will be written to the file.
    Private MySceneId As Integer = -1

#Region "Property"

    <Browsable(False)> _
    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Subscene
        End Get
    End Property

    Private MyLoop As Integer = -1
    <Category("Playback"), Description("The number of times to loop this scene. -1 to repeat indefinitely")> _
    Public Property [Loop] As Integer
        Get
            Return MyLoop
        End Get
        Set(value As Integer)
            Dim changed As Boolean = value <> MyLoop

            MyLoop = value

            If changed Then
                OnPropertyChanged("Loop")
            End If
        End Set
    End Property

    Private MyScene As Scene = Nothing
    <Category("Playback"), Editor(GetType(SceneSelectionEditor), GetType(UITypeEditor))> _
    Public Property Scene As Scene
        Get
            Return MyScene
        End Get
        Set(value As Scene)
            Dim changed As Boolean = value IsNot MyScene

            If changed AndAlso MyScene IsNot Nothing Then
                'RemoveHandler MyScene.Parent.PropertyChanged, AddressOf ProjectPropertyChanged                
            End If

            MyScene = value
            If changed Then
                If MyScene IsNot Nothing Then
                    AddHandler MyScene.Parent.PropertyChanged, AddressOf ProjectPropertyChanged
                End If

                OnPropertyChanged("Scene")
            End If
        End Set
    End Property

#End Region

#Region "OnClone"
    Protected Overrides Sub OnClone(clone As ArtificeObject)
        clone.As(Of Subscene).Scene = Scene
        clone.As(Of Subscene).Loop = [Loop]
    End Sub
#End Region

#Region "OnExportAudio"
    Protected Overrides Sub OnExportAudio(context As ExportAudioContext)
        'Here we have to ripple the ExportAudio command down into the actual Scene object:
        If Scene IsNot Nothing Then
            Dim totalFrames As Integer = Scene.TotalFrames()
            Dim sceneFrame As Integer = ((context.Frame - Parent.As(Of KeyFrame).Frame) Mod totalFrames) + 1
            Dim newContext As New ExportAudioContext(context.Mixer, sceneFrame, context.Frame / Designer.Project.FramesPerSecond)

            Scene.ExportAudio(newContext)
        End If

    End Sub
#End Region

#Region "OnLoad"
    Protected Overrides Sub OnLoad(el As XElement)
        MyBase.OnLoad(el)

        If el.@SceneId <> "" Then
            'We cache the scene id for now, because at this point the scene in question might not have been loaded yet.
            MySceneId = CInt(el.@SceneId)
        End If
    End Sub
#End Region

#Region "OnSave"
    Protected Overrides Sub OnSave(el As XElement)
        MyBase.OnSave(el)

        If Scene IsNot Nothing Then
            el.@SceneId = Scene.Id
        Else
            el.@SceneId = ""
        End If
    End Sub
#End Region

#Region "OnGetBounds"
    Protected Overrides Function OnGetBounds() As RectangleF
        Return New RectangleF(-Designer.Project.StageWidth \ 2, -Designer.Project.StageHeight \ 2, Designer.Project.StageWidth, Designer.Project.StageHeight)
    End Function
#End Region

#Region "OnHitTest"
    Protected Overrides Function OnHitTest(x As Single, y As Single, rc As RenderContext, ByRef transformedLocation As Point) As ArtificeMoveable
        Dim lastPoint As New Point(transformedLocation)

        Dim points(1) As Point
        points(0) = New Point(x, y)

        rc.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, points)

        transformedLocation.X = points(0).X
        transformedLocation.Y = points(0).Y

        If GetBounds().Contains(points(0)) Then Return Me

        transformedLocation.X = lastPoint.X
        transformedLocation.Y = lastPoint.Y

        Return MyBase.OnHitTest(x, y, rc, transformedLocation)
    End Function
#End Region

#Region "OnRender"
    Protected Overrides Sub OnRender(rc As RenderContext)
        Dim b As RectangleF = GetBounds()

        'If this is the first time we're being rendered, and we loaded from a file (so we have a cached
        'SceneId), get the scene from the project:
        If MyScene Is Nothing AndAlso MySceneId >= 0 Then
            MyScene = Designer.Project.GetChildById(MySceneId)

            If MyScene IsNot Nothing Then
                AddHandler MyScene.Parent.PropertyChanged, AddressOf ProjectPropertyChanged
            End If
        End If

        If Scene Is Nothing Then
            'Only draw the "broken scene" graphic if we're not playing or exporting
            If Not Designer.Playing AndAlso Not Designer.Exporting Then
                rc.Graphics.FillRectangle(Brushes.White, -b.Width \ 2, -b.Height \ 2, b.Width, b.Height)

                rc.Graphics.DrawRectangle(Pens.Red, -b.Width \ 2, -b.Height \ 2, b.Width, b.Height)

                rc.Graphics.DrawLine(Pens.Red, -b.Width \ 2, -b.Height \ 2, b.Width \ 2, b.Height \ 2)
                rc.Graphics.DrawLine(Pens.Red, b.Width \ 2, -b.Height \ 2, -b.Width \ 2, b.Height \ 2)
            End If
        Else
            'We do our scene animation here, because if there are several instances of the same scene in this
            'render loop we want each one to be animated with respect to where it started:
            Dim totalFrames As Integer = Scene.TotalFrames()
            Dim sceneFrame As Integer = ((rc.Frame - Parent.As(Of KeyFrame).Frame) Mod totalFrames) + 1
            Dim ac As New AnimationContext(sceneFrame)

            'We don't want the designer to fire off redraw events while animate, as it will cause an infinite redraw loop:
            Designer.Animating = True

            Scene.Animate(ac)

            Designer.Animating = False

            Dim subRC As New RenderContext(sceneFrame, rc.Graphics)

            subRC.OpacityStack.Push(rc.OpacityStack.Peek)

            Scene.Render(subRC)
        End If
    End Sub
#End Region

#Region "OnSaveAsSVG"
    Protected Overrides Function OnSaveAsSVG() As XElement()

        Dim g As XElement = <g id=<%= "ss" & Id %>/>

        g.@transform = "translate(" & X.Delta.ToString & " " & Y.Delta.ToString & ") "

        If ScaleX.Delta <> 1.0 OrElse ScaleY.Delta <> 1.0 Then
            g.@transform &= "scale(" & ScaleX.Delta.ToString & " " & ScaleY.Delta.ToString() & ") "
        End If

        If Rotation.Delta <> 0.0 Then
            g.@transform &= "rotate(" & Rotation.Delta.ToString() & ")"
        End If

        g.Add(MyScene.ToSVG)

        Return New XElement() {g}
    End Function
#End Region

#Region "ProjectPropertyChanged"
    Private Sub ProjectPropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        If sender Is Designer.Project AndAlso e.PropertyName = "Children" Then
            If Not Designer.Project.Children.Contains(Me.Scene) Then
                Scene = Nothing
            End If
        End If
    End Sub
#End Region

End Class

''' <summary>
''' The PropertyGrid popup editor for the scene selector
''' </summary>
''' <remarks></remarks>
Public Class SceneSelectionEditor
    Inherits UITypeEditor

    Public Overrides Function EditValue(context As ITypeDescriptorContext, provider As IServiceProvider, value As Object) As Object
        Dim cs As New ChildSelector
        Dim selected As Scene = value

        cs.ParentObject = Designer.Singleton.Project

        cs.ChildList.SelectedItem = selected

        If cs.ShowDialog(My.Application.OpenForms(0)) = DialogResult.OK Then
            selected = cs.ChildList.SelectedItem
        End If

        Return selected
    End Function

    Public Overrides Function GetEditStyle(context As ITypeDescriptorContext) As UITypeEditorEditStyle
        Return UITypeEditorEditStyle.Modal
    End Function

    Public Overrides Function GetPaintValueSupported(context As ITypeDescriptorContext) As Boolean
        Return True
    End Function

    Public Overrides Sub PaintValue(e As PaintValueEventArgs)
        If e.Value IsNot Nothing Then
            e.Graphics.DrawImage(My.Resources.Scene, e.Bounds)
        End If
    End Sub

End Class
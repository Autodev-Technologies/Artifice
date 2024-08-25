Imports System.Threading
Imports System.Drawing.Drawing2D

''' <summary>
''' A singleton class that acts as the switchboard for all events and major design-time properties throughout Artifice
''' </summary>
''' <remarks></remarks>
Public Class Designer
    Inherits PropertyChanger

    Private UndoIndex As Integer = 0
    Private UndoList As New List(Of UndoBase)
    Private InUndoBatch As Boolean = False

#Region "Properties"
    ''' <summary>
    ''' When True, prevents controls from refreshing when Artifice DOM object properties change
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Animating As Boolean = False

    Public ReadOnly Property CanRedo As Boolean
        Get
            Return UndoIndex < UndoList.Count - 1
        End Get
    End Property

    Public ReadOnly Property CanUndo As Boolean
        Get
            Return UndoList.Count > 0 AndAlso UndoIndex >= 0
        End Get
    End Property

    Private MyExporting As Boolean = False
    Public Property Exporting As Boolean
        Get
            Return MyExporting
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = value <> MyExporting

            MyExporting = value

            If changed Then OnPropertyChanged("Exporting")
        End Set
    End Property

    Private MyFillColor As Color = Color.White
    Public Property FillColor As Color
        Get
            Return MyFillColor
        End Get
        Set(value As Color)
            Dim changed As Boolean = value <> MyFillColor

            MyFillColor = value

            If changed Then OnPropertyChanged("FillColor")
        End Set
    End Property

    Private MyHighQualityRender As Boolean = True
    Public Property HighQualityRender As Boolean
        Get
            Return MyHighQualityRender
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = MyHighQualityRender <> value

            MyHighQualityRender = value

            If changed Then OnPropertyChanged("HighQualityRender")
        End Set
    End Property

    Private MyImages As New Dictionary(Of String, ImageCacheItem)
    Public ReadOnly Property Images(fileName As String) As System.Drawing.Image
        Get
            If Not MyImages.ContainsKey(fileName) Then
                MyImages.Add(fileName, New ImageCacheItem(fileName))
            End If

            Return MyImages(fileName).Image
        End Get
    End Property

    Private MyLineColor As Color = Color.Black
    Public Property LineColor As Color
        Get
            Return MyLineColor
        End Get
        Set(value As Color)
            Dim changed As Boolean = value <> MyLineColor

            MyLineColor = value

            If changed Then OnPropertyChanged("LineColor")
        End Set
    End Property

    Private MyLineWidth As Single = 3.0
    Public Property LineWidth As Single
        Get
            Return MyLineWidth
        End Get
        Set(value As Single)
            Dim changed As Boolean = value <> MyLineWidth

            MyLineWidth = value

            If changed Then OnPropertyChanged("LineWidth")
        End Set
    End Property

    Public Property LoopPlayback As Boolean
        Get
            If SelectedScene Is Nothing Then Return False

            Return SelectedScene.LoopPlayback
        End Get
        Set(value As Boolean)
            If SelectedScene Is Nothing Then Return

            Dim changed As Boolean = value <> SelectedScene.LoopPlayback

            SelectedScene.LoopPlayback = value

            If changed Then OnPropertyChanged("LoopPlayback")
        End Set
    End Property

    Private MyMuteAudio As Boolean = False
    Public Property MuteAudio As Boolean
        Get
            Return MyMuteAudio
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = value <> MyMuteAudio
            MyMuteAudio = value
            If changed Then OnPropertyChanged("MuteAudio")
        End Set
    End Property

    Private MyPlaying As Boolean = False
    Public Property Playing As Boolean
        Get
            Return MyPlaying
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = value <> MyPlaying

            MyPlaying = value

            If changed Then OnPropertyChanged("Playing")
        End Set
    End Property

    Private MyPointEditMode As Boolean = False
    Public Property PointEditMode As Boolean
        Get
            Return MyPointEditMode
        End Get
        Set(value As Boolean)
            Dim changed As Boolean = value <> MyPointEditMode

            MyPointEditMode = value

            If changed Then
                If Not value Then SelectedPoint = Nothing

                OnPropertyChanged("PointEditMode")
            End If
        End Set
    End Property

    Private WithEvents MyProject As Project = Nothing
    Public Property Project As Project
        Get
            Return MyProject
        End Get
        Set(value As Project)
            Dim changed As Boolean = value IsNot MyProject

            MyProject = value

            If changed Then
                'New project? Clear the undo list:
                UndoList.Clear()

                OnPropertyChanged("Project")

                If value IsNot Nothing Then
                    SelectedScene = value.Children.FirstOrDefault
                End If
            End If
        End Set
    End Property

    Private Shared MySingleton As Designer = Nothing
    Public Shared ReadOnly Property Singleton As Designer
        Get
            If MySingleton Is Nothing Then
                MySingleton = New Designer()
                'MySingleton.GenerateTestProject()
            End If

            Return MySingleton
        End Get
    End Property

    Private MySelectedContainer As ArtificeObject
    Public Property SelectedContainer As ArtificeObject
        Get
            Return MySelectedContainer
        End Get
        Set(value As ArtificeObject)
            Dim changed As Boolean = value IsNot MySelectedContainer
            MySelectedContainer = value
            If changed Then
                If value IsNot Nothing Then
                    SelectedLayer = value.GetAncestor(Of Layer)()
                End If

                OnPropertyChanged("SelectedContainer")
            End If
        End Set
    End Property

    Private MySelectedLayer As ArtificeLayerBase
    Public Property SelectedLayer As ArtificeLayerBase
        Get
            Return MySelectedLayer
        End Get
        Set(value As ArtificeLayerBase)
            Dim changed As Boolean = value IsNot MySelectedLayer
            MySelectedLayer = value
            If changed Then
                If SelectedContainer IsNot Nothing AndAlso value IsNot Nothing AndAlso Not value.IsAncestorOf(SelectedContainer) Then
                    SelectedContainer = Nothing
                End If

                If value IsNot Nothing AndAlso SelectedScene IsNot Nothing Then
                    If value.Is(Of Layer)() Then
                        SelectedContainer = value.As(Of Layer)().GetKeyFrame(SelectedScene.Frame)
                    End If
                End If

                OnPropertyChanged("SelectedLayer")
            End If
        End Set
    End Property

    Private MySelectedObjects As New List(Of ArtificeObject)
    Public ReadOnly Property SelectedObjects As ArtificeObject()
        Get
            Return MySelectedObjects.ToArray()
        End Get
    End Property

    Public Property SelectedObject As ArtificeObject
        Get
            Return MySelectedObjects.FirstOrDefault
        End Get
        Set(value As ArtificeObject)
            If value Is Nothing AndAlso MySelectedObjects.Any() Then
                SelectedPoint = Nothing
                MySelectedObjects.Clear()
                OnPropertyChanged("SelectedObjects")
                Return
            End If

            SetSelection(value)
        End Set
    End Property

    Private MySelectedPoint As VectorPoint
    Public Property SelectedPoint As VectorPoint
        Get
            Return MySelectedPoint
        End Get
        Set(value As VectorPoint)
            Dim changed As Boolean = value IsNot MySelectedPoint
            MySelectedPoint = value
            If changed Then
                PointEditMode = True
                OnPropertyChanged("SelectedPoint")
            End If
        End Set
    End Property

    Private MySelectedScene As Scene = Nothing
    Public Property SelectedScene As Scene
        Get
            Return MySelectedScene
        End Get
        Set(value As Scene)
            Dim changed As Boolean = value IsNot MySelectedScene

            MySelectedScene = value

            If changed Then
                OnPropertyChanged("SelectedScene")
                OnPropertyChanged("SelectedSceneId")

                'We fire LoopPlayback changed so that any controls bound to LoopPlayback update with the scene change appropriately:
                OnPropertyChanged("LoopPlayback")
            End If
        End Set
    End Property

    Public Property SelectedSceneId As Integer
        Get
            If SelectedScene Is Nothing Then Return 0

            Return SelectedScene.Id
        End Get
        Set(value As Integer)
            Dim changed As Boolean = False

            If SelectedScene Is Nothing Then
                changed = True
            Else
                changed = value <> SelectedScene.Id
            End If

            If changed Then
                SelectedScene = Project.GetChildById(value)
            End If
        End Set
    End Property

    Private MySelectedTool As ToolBase = Nothing
    Public Property SelectedTool As ToolBase
        Get
            Return MySelectedTool
        End Get
        Set(value As ToolBase)
            Dim changed As Boolean = value IsNot MySelectedTool

            MySelectedTool = value

            If changed Then OnPropertyChanged("SelectedTool")
        End Set
    End Property
#End Region

#Region "Constructor"
    'Prevent anything from creating an instance of this class:
    Private Sub New()

    End Sub
#End Region

#Region "AddSelection"
    ''' <summary>
    ''' Adds the given ArtificeObject to the collection of selected objects
    ''' </summary>
    ''' <param name="obj">The individual ArtificeObject to add</param>
    ''' <remarks></remarks>
    Public Sub AddSelection(obj As ArtificeObject)
        If obj Is Nothing Then Return

        MySelectedObjects.Add(obj)

        OnPropertyChanged("SelectedObjects")
    End Sub

    ''' <summary>
    ''' Adds the given ArtificeObjects to the collection fo selected objects
    ''' </summary>
    ''' <param name="obj">An array of ArtificeObjects to add</param>
    ''' <remarks></remarks>
    Public Sub AddSelection(obj() As ArtificeObject)
        If obj Is Nothing Then Return

        MySelectedObjects.AddRange(obj)

        OnPropertyChanged("SelectedObjects")
    End Sub
#End Region

#Region "AddUndo"
    ''' <summary>
    ''' Adds the given undo action to the undo list
    ''' </summary>
    ''' <param name="ub">The undo action to add</param>
    ''' <remarks></remarks>
    Public Sub AddUndo(ub As UndoBase)
        'If we've been undoing (and there are items that can be redone),
        'but we're making a change, we need to blow away all those items we already undid
        'and add this new change to the top of the list:
        If UndoIndex < UndoList.Count - 1 Then
            UndoList.RemoveRange(UndoIndex + 1, UndoList.Count - UndoIndex - 1)
        End If

        'Are we in an UndoBatch?
        If InUndoBatch Then
            CType(UndoList.Last, UndoBatch).AddUndo(ub)
        Else
            'Add it to the list:
            UndoList.Add(ub)
            UndoIndex = UndoList.Count - 1
        End If

        'Fire our fake "Undo" property changed event to force the UI to update itself:
        OnPropertyChanged("Undo")
    End Sub
#End Region

#Region "EndUndoBatch"
    ''' <summary>
    ''' Closes the current UndoBatch
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub EndUndoBatch()
        InUndoBatch = False

        'Did we create an empty batch?
        'If so, let's remove it from the list because it's pointless:
        If CType(UndoList.Last, UndoBatch).IsEmpty Then
            UndoList.RemoveAt(UndoList.Count - 1)
            UndoIndex = UndoList.Count - 1
        End If

        'Fire our fake "Undo" property changed event to force the UI to update itself:
        OnPropertyChanged("Undo")
    End Sub
#End Region

#Region "ExportToSVG"
    ''' <summary>
    ''' Exports the currently-selected project to an SVG file
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub ExportToSVG(fileName As String)
        Dim fs As New IO.StreamWriter(fileName, False)
        Dim svg As XElement = <svg version="1.1"></svg>

        svg.@viewBox = (Project.StageWidth / -2).ToString() & " " & (Project.StageHeight / -2).ToString() & " " & Project.StageWidth.ToString() & " " & Project.StageHeight.ToString()

        svg.Add(SelectedScene.ToSVG())

        fs.WriteLine("<?xml version=""1.0"" standalone=""no"" ?>")
        fs.WriteLine("")
        fs.Write(svg.ToString())

        fs.Close()
    End Sub
#End Region

#Region "GetSelectionBounds"
    ''' <summary>
    ''' Returns the unioned bounds of the current selected objects
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSelectionBounds() As RectangleF
        Dim r As New RectangleF

        If SelectedObjects.Any AndAlso SelectedObjects.First.Is(Of ArtificeMoveable)() Then
            Dim minX As Single = Single.MaxValue
            Dim minY As Single = Single.MaxValue
            Dim maxX As Single = Single.MinValue
            Dim maxY As Single = Single.MinValue

            For Each vm As ArtificeMoveable In SelectedObjects
                Dim b As RectangleF = vm.GetBounds

                minX = Math.Min(minX, vm.X.Delta + b.Left)
                minY = Math.Min(minY, vm.Y.Delta + b.Top)
                maxX = Math.Max(maxX, vm.X.Delta + b.Right)
                maxY = Math.Max(maxY, vm.Y.Delta + b.Bottom)
            Next

            'r = New Point((maxX - minX) / 2 + minX, (maxY - minY) / 2 + minY)
            r = RectangleF.FromLTRB(minX, minY, maxX, maxY)
        End If

        Return r
    End Function
#End Region

#Region "MyProject_PropertyChanged"
    ''' <summary>
    ''' Handles internal property changes on the current Project, so as to bubble them to any external objects
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MyProject_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles MyProject.PropertyChanged
        'If the frame has changed, make sure our selected container is the appropriate keyframe given the selected layer:
        If sender.GetType() Is GetType(Scene) AndAlso e.PropertyName = "Frame" AndAlso sender Is SelectedScene AndAlso SelectedLayer IsNot Nothing AndAlso SelectedLayer.Is(Of Layer)() Then
            SelectedContainer = SelectedLayer.As(Of Layer)().GetKeyFrame(SelectedScene.Frame)
        End If

        'Raise this event on me. Other classes can bind to this event and do useful things:
        OnPropertyChanged(sender, e)
    End Sub
#End Region

#Region "Redo"
    ''' <summary>
    ''' Redoes the action at the current position in the undo list
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Redo()
        If UndoIndex >= UndoList.Count - 1 Then Return

        UndoIndex += 1

        UndoList(UndoIndex).Redo()

        'Fire our fake "Undo" property changed event to force the UI to update itself:
        OnPropertyChanged("Undo")
    End Sub
#End Region

#Region "RemoveSelection"
    ''' <summary>
    ''' Removes the given object from the collection of selected objects
    ''' </summary>
    ''' <param name="obj">The object to remove from the selection</param>
    ''' <remarks></remarks>
    Public Sub RemoveSelection(obj As ArtificeObject)
        MySelectedObjects.Remove(obj)
        OnPropertyChanged("SelectedObjects")
    End Sub
#End Region

#Region "SetSelection"
    ''' <summary>
    ''' Sets the collection of selected objects to the given object, removing any previously-selected objects
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <remarks></remarks>
    Public Sub SetSelection(obj As ArtificeObject)
        If obj Is Nothing Then
            If MySelectedObjects.Any() Then
                MySelectedObjects.Clear()

                OnPropertyChanged("SelectedObjects")
            End If

            Return
        End If

        If SelectedObjects.Count = 1 AndAlso SelectedObjects.First Is obj Then Return

        SetSelection(New ArtificeObject() {obj})
    End Sub

    ''' <summary>
    ''' Sets the collection of selected objects to the given objects, removing any previously-selected objects
    ''' </summary>
    ''' <param name="objs"></param>
    ''' <remarks></remarks>
    Public Sub SetSelection(objs() As ArtificeObject)
        MySelectedObjects.Clear()
        MySelectedObjects.AddRange(objs)

        OnPropertyChanged("SelectedObjects")

        If MySelectedObjects.Count = 1 Then
            If SelectedObject.Is(Of Layer)() Then
                SelectedLayer = SelectedObject
            Else
                SelectedLayer = SelectedObject.GetAncestor(Of Layer)()
            End If
        End If
    End Sub
#End Region

#Region "StartUndoBatch"
    ''' <summary>
    ''' Creates a new UndoBatch
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StartUndoBatch()
        'Add the UndoBatch to the top of the undo list:
        Dim ub As New UndoBatch()

        AddUndo(ub)

        'We're now in batch mode; calls to AddUndo will add to this batch instead of the list
        'until EndUndoBatch is called
        InUndoBatch = True
    End Sub
#End Region

#Region "Undo"
    ''' <summary>
    ''' Undoes the current action at the UndoIndex
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Undo()
        'If our buffer is empty or our UndoIndex < 0 then we shouldn't be here
        If UndoList.Count = 0 OrElse UndoIndex < 0 Then Return

        'Undo the action at the current index:
        UndoList(UndoIndex).Undo()

        'Go back one in the list:
        UndoIndex -= 1

        'Let everyone know that our fake "Undo" property is changed
        '(we do this to make the UI update itself and gray out menus, etc.)
        OnPropertyChanged("Undo")
    End Sub
#End Region

End Class
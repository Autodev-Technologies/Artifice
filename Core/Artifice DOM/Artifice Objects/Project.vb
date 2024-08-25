Imports System.ComponentModel
Imports System.Drawing.Drawing2D

''' <summary>
''' The root object in the Artifice DOM
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class Project
    Inherits ArtificeObject

#Region "Properties"

    Private MyDirty As Boolean = False
    ''' <summary>
    ''' Returns True if there have been changes to the DOM since the last save, False otherwise
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)> _
    Public ReadOnly Property Dirty As Boolean
        Get
            Return MyDirty
        End Get
    End Property

    Private MyFileName As String = ""
    ''' <summary>
    ''' The internal filename for this Artifice project
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)> _
    Public ReadOnly Property FileName As String
        Get
            Return MyFileName
        End Get
    End Property

    Private MyFramesPerSecond As Integer = 24
    ''' <summary>
    ''' The Frames per Second (FPS) of this animation
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Design"), DisplayName("Frames per Second"), DefaultValue(24), Description("The Frames per Second (FPS) of this animation")> _
    Public Property FramesPerSecond As Integer
        Get
            Return MyFramesPerSecond
        End Get
        Set(value As Integer)
            Dim changed As Boolean = value <> MyFramesPerSecond

            MyFramesPerSecond = value

            If changed Then
                OnPropertyChanged("FramesPerSecond")
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property Icon As Image
        Get
            Return MyBase.Icon
        End Get
    End Property

    Private MyStageHeight As Integer = 600
    ''' <summary>
    ''' The height of this project's stage, in Pixels
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Appearance"), DisplayName("Stage Height"), Description("The height of this project's stage, in Pixels"), DefaultValue(600)> _
    Public Property StageHeight As Integer
        Get
            Return MyStageHeight
        End Get
        Set(value As Integer)
            value = Math.Max(16, value)

            Dim changed As Boolean = value <> MyStageHeight

            MyStageHeight = value

            If changed Then
                OnPropertyChanged("StageHeight")
            End If
        End Set
    End Property

    Private MyStageWidth As Integer = 800
    ''' <summary>
    ''' The width of this project's stage, in Pixels
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Appearance"), DisplayName("Stage Width"), Description("The width of this project's stage, in Pixels"), DefaultValue(800)> _
    Public Property StageWidth As Integer
        Get
            Return MyStageWidth
        End Get
        Set(value As Integer)
            value = Math.Max(16, value)

            Dim changed As Boolean = value <> MyStageWidth

            MyStageWidth = value

            If changed Then
                OnPropertyChanged("StageWidth")
            End If
        End Set
    End Property

#End Region

#Region "FromFile"
    ''' <summary>
    ''' Creates a Project object by loading it from the given file
    ''' </summary>
    ''' <param name="fileName">The .Artifice file to load</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FromFile(fileName As String) As Project
        Dim el As XElement = XElement.Load(fileName)

        Dim vp As Project = Project.FromXElement(el)

        vp.MyFileName = fileName

        vp.MyDirty = False

        Return vp
    End Function
#End Region

#Region "OnLoad"
    Protected Overrides Sub OnLoad(el As XElement)
        MyBase.OnLoad(el)

        ResetIdCounter(el.@LastId)
        MyFramesPerSecond = el.@FramesPerSecond
        MyStageWidth = el.@StageWidth
        MyStageHeight = el.@StageHeight

        'Reset the dirty flag:
        MyDirty = False
    End Sub
#End Region

#Region "OnSave"
    Protected Overrides Sub OnSave(el As XElement)
        MyBase.OnSave(el)

        el.@LastId = LastId.ToString()
        el.@FramesPerSecond = FramesPerSecond.ToString()
        el.@StageWidth = StageWidth.ToString()
        el.@StageHeight = StageHeight.ToString()
    End Sub
#End Region

#Region "Save"
    ''' <summary>
    ''' Saves the Project to the project file
    ''' </summary>
    ''' <remarks></remarks>
    Sub Save()
        Dim projectElement As XElement = ToXElement()

        projectElement.Save(FileName)

        'Reset the Dirty flag
        MyDirty = False
    End Sub
#End Region

#Region "Save"
    ''' <summary>
    ''' Saves the Project to the given file
    ''' </summary>
    ''' <param name="newFileName">The name of the new file to save to</param>
    ''' <remarks></remarks>
    Sub SaveAs(newFileName As String)
        MyFileName = newFileName
        Save()
    End Sub
#End Region

#Region "Project_PropertyChanged"
    ''' <summary>
    ''' Internally responds to bubbled change events and alters the Dirty flag accordingly
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Project_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged
        If Not MyDirty Then
            MyDirty = True
            OnPropertyChanged("Dirty")
        End If
    End Sub
#End Region

End Class
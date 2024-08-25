Imports System.IO

''' <summary>
''' A single item in the global ImageCache
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class ImageCacheItem
    Inherits PropertyChanger    

    Private WithEvents MyImageWatcher As New FileSystemWatcher

#Region "Properties"
    Private MyFileName As String
    Public ReadOnly Property FileName As String
        Get
            Return MyFileName
        End Get
    End Property

    Private MyImage As Image = Nothing
    Public ReadOnly Property Image As Image
        Get
            Return MyImage
        End Get
    End Property
#End Region

#Region "Constructor"
    Public Sub New(fileName As String)
        MyFileName = fileName
        LoadImage()
        MyImageWatcher.Path = IO.Path.GetDirectoryName(fileName)
        MyImageWatcher.Filter = IO.Path.GetFileName(fileName)
        MyImageWatcher.NotifyFilter = NotifyFilters.LastWrite
        MyImageWatcher.EnableRaisingEvents = True
    End Sub
#End Region

#Region "Destructor"
    Protected Overrides Sub Finalize()
        MyBase.Finalize()

        'Cleanup:
        If MyImage IsNot Nothing Then MyImage.Dispose()
    End Sub
#End Region

#Region "LoadImage"
    ''' <summary>
    ''' Loads the image into the cache
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadImage()
        If MyImage IsNot Nothing Then MyImage.Dispose()

        MyImage = System.Drawing.Image.FromFile(MyFileName)

        OnPropertyChanged("Image")
    End Sub
#End Region

#Region "MyImageWatcher_Changed"
    Private Sub MyImageWatcher_Changed(sender As Object, e As FileSystemEventArgs) Handles MyImageWatcher.Changed
        'If our image has changed, reload it:
        LoadImage()
    End Sub
#End Region

End Class

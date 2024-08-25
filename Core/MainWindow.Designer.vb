<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RevertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportPNGMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SingleFrameAsSVGToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UndoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RedoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator()
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DuplicateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectallToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SceneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddLayerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator()
        Me.RenameLayerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteLayerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripSeparator()
        Me.NewSceneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteSceneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ObjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArtificeObjectMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CreateGroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UngroupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator()
        Me.BringToFrontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BringForwardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendBackwardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendToBackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripSeparator()
        Me.FlipHorizontalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FlipVerticalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem13 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyToNextKeyframeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToPreviousKeyframeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConvertToVectorObjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlayAudioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HighQualityRenderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusBar = New System.Windows.Forms.StatusStrip()
        Me.StatusInformation = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusBarPosition = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LeftRightSplitter = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DesignerTools = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.PointEditModeButton = New Artifice.ToolStripBindableButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.FillColorButton = New Artifice.ToolStripColorButton()
        Me.LineColorButton = New Artifice.ToolStripColorButton()
        Me.ZoomSelectorButton = New System.Windows.Forms.ToolStripDropDownButton()
        Me.FitZoomMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActualSize100ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ZoomInToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZoomOutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CenterStageHorizontalButton = New System.Windows.Forms.ToolStripButton()
        Me.CenterStageVerticalButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.AlignLeftButton = New System.Windows.Forms.ToolStripButton()
        Me.AlignHorizontalCenterButton = New System.Windows.Forms.ToolStripButton()
        Me.AlignRightButton = New System.Windows.Forms.ToolStripButton()
        Me.AlignTopButton = New System.Windows.Forms.ToolStripButton()
        Me.AlignVerticalCenterButton = New System.Windows.Forms.ToolStripButton()
        Me.AlignBottomButton = New System.Windows.Forms.ToolStripButton()
        Me.HideDesignTimeline = New System.Windows.Forms.ToolStripButton()
        Me.SurfaceTimelineSplitter = New System.Windows.Forms.SplitContainer()
        Me.SceneSurface1 = New Artifice.SceneSurface()
        Me.DesignerTimeline = New Artifice.Timeline()
        Me.TimelineToolStrip = New System.Windows.Forms.ToolStrip()
        Me.NewLayer = New System.Windows.Forms.ToolStripButton()
        Me.NewFolder = New System.Windows.Forms.ToolStripButton()
        Me.DeleteLayer = New System.Windows.Forms.ToolStripButton()
        Me.NewSceneButton = New System.Windows.Forms.ToolStripButton()
        Me.SceneList = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ViewTools = New System.Windows.Forms.ToolStrip()
        Me.ToolStripBindableButton1 = New Artifice.ToolStripBindableButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.Hide = New System.Windows.Forms.ToolStripButton()
        Me.ProjectTree = New System.Windows.Forms.TreeView()
        Me.ProjectTreeImages = New System.Windows.Forms.ImageList(Me.components)
        Me.EffectsPanel = New System.Windows.Forms.Panel()
        Me.EffectList = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.EffectsToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.AddEffectButton = New System.Windows.Forms.ToolStripDropDownButton()
        Me.RemoveEffectButton = New System.Windows.Forms.ToolStripButton()
        Me.Properties = New System.Windows.Forms.PropertyGrid()
        Me.Opener = New System.Windows.Forms.OpenFileDialog()
        Me.Saver = New System.Windows.Forms.SaveFileDialog()
        Me.SVGFileName = New System.Windows.Forms.SaveFileDialog()
        Me.ImageExportFilename = New System.Windows.Forms.SaveFileDialog()
        Me.PropPanel = New System.Windows.Forms.Panel()
        Me.HierPanel = New System.Windows.Forms.Panel()
        Me.MainMenu.SuspendLayout()
        Me.ArtificeObjectMenu.SuspendLayout()
        Me.StatusBar.SuspendLayout()
        CType(Me.LeftRightSplitter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LeftRightSplitter.Panel1.SuspendLayout()
        Me.LeftRightSplitter.Panel2.SuspendLayout()
        Me.LeftRightSplitter.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.DesignerTools.SuspendLayout()
        CType(Me.SurfaceTimelineSplitter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SurfaceTimelineSplitter.Panel1.SuspendLayout()
        Me.SurfaceTimelineSplitter.Panel2.SuspendLayout()
        Me.SurfaceTimelineSplitter.SuspendLayout()
        Me.TimelineToolStrip.SuspendLayout()
        Me.ViewTools.SuspendLayout()
        Me.EffectsPanel.SuspendLayout()
        Me.EffectsToolStrip.SuspendLayout()
        Me.PropPanel.SuspendLayout()
        Me.HierPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu
        '
        Me.MainMenu.AutoSize = False
        Me.MainMenu.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.SceneToolStripMenuItem, Me.ObjectToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MainMenu.Size = New System.Drawing.Size(939, 30)
        Me.MainMenu.TabIndex = 0
        Me.MainMenu.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.RevertToolStripMenuItem, Me.ToolStripMenuItem1, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveasToolStripMenuItem, Me.ToolStripMenuItem2, Me.ExportToolStripMenuItem, Me.ToolStripMenuItem3, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 26)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.NewToolStripMenuItem.Text = "&New Project"
        '
        'RevertToolStripMenuItem
        '
        Me.RevertToolStripMenuItem.Name = "RevertToolStripMenuItem"
        Me.RevertToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.RevertToolStripMenuItem.Text = "&Revert to Last Save..."
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(178, 6)
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.OpenToolStripMenuItem.Text = "&Open..."
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.SaveToolStripMenuItem.Text = "&Save"
        '
        'SaveasToolStripMenuItem
        '
        Me.SaveasToolStripMenuItem.Name = "SaveasToolStripMenuItem"
        Me.SaveasToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.SaveasToolStripMenuItem.Text = "Save &as..."
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(178, 6)
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportPNGMenuItem, Me.SingleFrameAsSVGToolStripMenuItem})
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.ExportToolStripMenuItem.Text = "&Export..."
        '
        'ExportPNGMenuItem
        '
        Me.ExportPNGMenuItem.Name = "ExportPNGMenuItem"
        Me.ExportPNGMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.ExportPNGMenuItem.Text = "Artwork as PNG"
        '
        'SingleFrameAsSVGToolStripMenuItem
        '
        Me.SingleFrameAsSVGToolStripMenuItem.Name = "SingleFrameAsSVGToolStripMenuItem"
        Me.SingleFrameAsSVGToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.SingleFrameAsSVGToolStripMenuItem.Text = "Artwork as SVG"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(178, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoToolStripMenuItem, Me.RedoToolStripMenuItem, Me.ToolStripMenuItem7, Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.DuplicateToolStripMenuItem, Me.DeleteToolStripMenuItem, Me.ToolStripMenuItem8, Me.SelectallToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 26)
        Me.EditToolStripMenuItem.Text = "E&dit"
        '
        'UndoToolStripMenuItem
        '
        Me.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
        Me.UndoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.UndoToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.UndoToolStripMenuItem.Text = "&Undo"
        '
        'RedoToolStripMenuItem
        '
        Me.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem"
        Me.RedoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
        Me.RedoToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.RedoToolStripMenuItem.Text = "&Redo"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(163, 6)
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Image = Global.Artifice.My.Resources.Resources.cut
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.CutToolStripMenuItem.Text = "C&ut"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Image = Global.Artifice.My.Resources.Resources.copy
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.CopyToolStripMenuItem.Text = "&Copy"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Image = Global.Artifice.My.Resources.Resources.paste
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.PasteToolStripMenuItem.Text = "&Paste"
        '
        'DuplicateToolStripMenuItem
        '
        Me.DuplicateToolStripMenuItem.Name = "DuplicateToolStripMenuItem"
        Me.DuplicateToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.DuplicateToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.DuplicateToolStripMenuItem.Text = "Duplica&te"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Image = Global.Artifice.My.Resources.Resources.Remove
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.DeleteToolStripMenuItem.Text = "&Delete"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(163, 6)
        '
        'SelectallToolStripMenuItem
        '
        Me.SelectallToolStripMenuItem.Name = "SelectallToolStripMenuItem"
        Me.SelectallToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SelectallToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.SelectallToolStripMenuItem.Text = "Select &all"
        '
        'SceneToolStripMenuItem
        '
        Me.SceneToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddLayerToolStripMenuItem, Me.AddFolderToolStripMenuItem, Me.ToolStripMenuItem5, Me.RenameLayerToolStripMenuItem, Me.DeleteLayerToolStripMenuItem, Me.ToolStripMenuItem10, Me.NewSceneToolStripMenuItem, Me.DeleteSceneToolStripMenuItem})
        Me.SceneToolStripMenuItem.Name = "SceneToolStripMenuItem"
        Me.SceneToolStripMenuItem.Size = New System.Drawing.Size(50, 26)
        Me.SceneToolStripMenuItem.Text = "&Scene"
        '
        'AddLayerToolStripMenuItem
        '
        Me.AddLayerToolStripMenuItem.Image = Global.Artifice.My.Resources.Resources.Layer
        Me.AddLayerToolStripMenuItem.Name = "AddLayerToolStripMenuItem"
        Me.AddLayerToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.AddLayerToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.AddLayerToolStripMenuItem.Text = "Add Layer"
        '
        'AddFolderToolStripMenuItem
        '
        Me.AddFolderToolStripMenuItem.Image = Global.Artifice.My.Resources.Resources.FolderClosed
        Me.AddFolderToolStripMenuItem.Name = "AddFolderToolStripMenuItem"
        Me.AddFolderToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.AddFolderToolStripMenuItem.Text = "Add Folder"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(164, 6)
        '
        'RenameLayerToolStripMenuItem
        '
        Me.RenameLayerToolStripMenuItem.Name = "RenameLayerToolStripMenuItem"
        Me.RenameLayerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.RenameLayerToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.RenameLayerToolStripMenuItem.Text = "Rename Layer"
        '
        'DeleteLayerToolStripMenuItem
        '
        Me.DeleteLayerToolStripMenuItem.Image = Global.Artifice.My.Resources.Resources.Remove
        Me.DeleteLayerToolStripMenuItem.Name = "DeleteLayerToolStripMenuItem"
        Me.DeleteLayerToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.DeleteLayerToolStripMenuItem.Text = "Delete Layer"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(164, 6)
        '
        'NewSceneToolStripMenuItem
        '
        Me.NewSceneToolStripMenuItem.Image = Global.Artifice.My.Resources.Resources.Scene
        Me.NewSceneToolStripMenuItem.Name = "NewSceneToolStripMenuItem"
        Me.NewSceneToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.NewSceneToolStripMenuItem.Text = "New Artwork"
        '
        'DeleteSceneToolStripMenuItem
        '
        Me.DeleteSceneToolStripMenuItem.Image = Global.Artifice.My.Resources.Resources.Remove
        Me.DeleteSceneToolStripMenuItem.Name = "DeleteSceneToolStripMenuItem"
        Me.DeleteSceneToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.DeleteSceneToolStripMenuItem.Text = "Delete Artwork"
        '
        'ObjectToolStripMenuItem
        '
        Me.ObjectToolStripMenuItem.DropDown = Me.ArtificeObjectMenu
        Me.ObjectToolStripMenuItem.Name = "ObjectToolStripMenuItem"
        Me.ObjectToolStripMenuItem.Size = New System.Drawing.Size(54, 26)
        Me.ObjectToolStripMenuItem.Text = "&Object"
        '
        'ArtificeObjectMenu
        '
        Me.ArtificeObjectMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateGroupToolStripMenuItem, Me.UngroupToolStripMenuItem, Me.ToolStripMenuItem6, Me.BringToFrontToolStripMenuItem, Me.BringForwardToolStripMenuItem, Me.SendBackwardToolStripMenuItem, Me.SendToBackToolStripMenuItem, Me.ToolStripMenuItem9, Me.FlipHorizontalToolStripMenuItem, Me.FlipVerticalToolStripMenuItem, Me.ToolStripMenuItem13, Me.CopyToNextKeyframeToolStripMenuItem, Me.CopyToPreviousKeyframeToolStripMenuItem, Me.ConvertToVectorObjectToolStripMenuItem})
        Me.ArtificeObjectMenu.Name = "ArtificeObjectMenu"
        Me.ArtificeObjectMenu.OwnerItem = Me.ObjectToolStripMenuItem
        Me.ArtificeObjectMenu.Size = New System.Drawing.Size(218, 264)
        '
        'CreateGroupToolStripMenuItem
        '
        Me.CreateGroupToolStripMenuItem.Name = "CreateGroupToolStripMenuItem"
        Me.CreateGroupToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.CreateGroupToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.CreateGroupToolStripMenuItem.Text = "Create &Group"
        '
        'UngroupToolStripMenuItem
        '
        Me.UngroupToolStripMenuItem.Name = "UngroupToolStripMenuItem"
        Me.UngroupToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.UngroupToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.UngroupToolStripMenuItem.Text = "&Ungroup"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(214, 6)
        '
        'BringToFrontToolStripMenuItem
        '
        Me.BringToFrontToolStripMenuItem.Name = "BringToFrontToolStripMenuItem"
        Me.BringToFrontToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.BringToFrontToolStripMenuItem.Text = "Bring to &Front"
        '
        'BringForwardToolStripMenuItem
        '
        Me.BringForwardToolStripMenuItem.Name = "BringForwardToolStripMenuItem"
        Me.BringForwardToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.BringForwardToolStripMenuItem.Text = "Bring Forward"
        '
        'SendBackwardToolStripMenuItem
        '
        Me.SendBackwardToolStripMenuItem.Name = "SendBackwardToolStripMenuItem"
        Me.SendBackwardToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.SendBackwardToolStripMenuItem.Text = "Send Backward"
        '
        'SendToBackToolStripMenuItem
        '
        Me.SendToBackToolStripMenuItem.Name = "SendToBackToolStripMenuItem"
        Me.SendToBackToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.SendToBackToolStripMenuItem.Text = "Send to &Back"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(214, 6)
        '
        'FlipHorizontalToolStripMenuItem
        '
        Me.FlipHorizontalToolStripMenuItem.Name = "FlipHorizontalToolStripMenuItem"
        Me.FlipHorizontalToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.FlipHorizontalToolStripMenuItem.Text = "Flip &Horizontal"
        '
        'FlipVerticalToolStripMenuItem
        '
        Me.FlipVerticalToolStripMenuItem.Name = "FlipVerticalToolStripMenuItem"
        Me.FlipVerticalToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.FlipVerticalToolStripMenuItem.Text = "Flip &Vertical"
        '
        'ToolStripMenuItem13
        '
        Me.ToolStripMenuItem13.Name = "ToolStripMenuItem13"
        Me.ToolStripMenuItem13.Size = New System.Drawing.Size(214, 6)
        '
        'CopyToNextKeyframeToolStripMenuItem
        '
        Me.CopyToNextKeyframeToolStripMenuItem.Name = "CopyToNextKeyframeToolStripMenuItem"
        Me.CopyToNextKeyframeToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.CopyToNextKeyframeToolStripMenuItem.Text = "Copy to &Next Keyframe"
        '
        'CopyToPreviousKeyframeToolStripMenuItem
        '
        Me.CopyToPreviousKeyframeToolStripMenuItem.Name = "CopyToPreviousKeyframeToolStripMenuItem"
        Me.CopyToPreviousKeyframeToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.CopyToPreviousKeyframeToolStripMenuItem.Text = "Copy to &Previous Keyframe"
        '
        'ConvertToVectorObjectToolStripMenuItem
        '
        Me.ConvertToVectorObjectToolStripMenuItem.Name = "ConvertToVectorObjectToolStripMenuItem"
        Me.ConvertToVectorObjectToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.ConvertToVectorObjectToolStripMenuItem.Text = "Convert to Vector &Object"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PlayAudioToolStripMenuItem, Me.HighQualityRenderToolStripMenuItem})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(61, 26)
        Me.OptionsToolStripMenuItem.Text = "O&ptions"
        '
        'PlayAudioToolStripMenuItem
        '
        Me.PlayAudioToolStripMenuItem.Name = "PlayAudioToolStripMenuItem"
        Me.PlayAudioToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.PlayAudioToolStripMenuItem.Text = "&Mute Audio"
        '
        'HighQualityRenderToolStripMenuItem
        '
        Me.HighQualityRenderToolStripMenuItem.Name = "HighQualityRenderToolStripMenuItem"
        Me.HighQualityRenderToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.HighQualityRenderToolStripMenuItem.Text = "High Quality &Render"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 26)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.AboutToolStripMenuItem.Text = "&About Autodev Artifice..."
        '
        'StatusBar
        '
        Me.StatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusInformation, Me.StatusBarPosition})
        Me.StatusBar.Location = New System.Drawing.Point(0, 551)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(939, 22)
        Me.StatusBar.TabIndex = 1
        Me.StatusBar.Text = "StatusStrip1"
        '
        'StatusInformation
        '
        Me.StatusInformation.Name = "StatusInformation"
        Me.StatusInformation.Size = New System.Drawing.Size(824, 17)
        Me.StatusInformation.Spring = True
        Me.StatusInformation.Text = "Ready"
        Me.StatusInformation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusBarPosition
        '
        Me.StatusBarPosition.AutoSize = False
        Me.StatusBarPosition.Name = "StatusBarPosition"
        Me.StatusBarPosition.Size = New System.Drawing.Size(100, 17)
        Me.StatusBarPosition.Text = "0, 0"
        '
        'LeftRightSplitter
        '
        Me.LeftRightSplitter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LeftRightSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.LeftRightSplitter.Location = New System.Drawing.Point(0, 30)
        Me.LeftRightSplitter.Name = "LeftRightSplitter"
        '
        'LeftRightSplitter.Panel1
        '
        Me.LeftRightSplitter.Panel1.Controls.Add(Me.SplitContainer1)
        '
        'LeftRightSplitter.Panel2
        '
        Me.LeftRightSplitter.Panel2.Controls.Add(Me.ViewTools)
        Me.LeftRightSplitter.Size = New System.Drawing.Size(939, 521)
        Me.LeftRightSplitter.SplitterDistance = 895
        Me.LeftRightSplitter.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DesignerTools)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SurfaceTimelineSplitter)
        Me.SplitContainer1.Size = New System.Drawing.Size(895, 521)
        Me.SplitContainer1.SplitterDistance = 44
        Me.SplitContainer1.TabIndex = 1
        '
        'DesignerTools
        '
        Me.DesignerTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DesignerTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.DesignerTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator6, Me.PointEditModeButton, Me.ToolStripSeparator1, Me.FillColorButton, Me.LineColorButton, Me.ZoomSelectorButton, Me.ToolStripSeparator4, Me.CenterStageHorizontalButton, Me.CenterStageVerticalButton, Me.ToolStripSeparator5, Me.AlignLeftButton, Me.AlignHorizontalCenterButton, Me.AlignRightButton, Me.AlignTopButton, Me.AlignVerticalCenterButton, Me.AlignBottomButton, Me.HideDesignTimeline})
        Me.DesignerTools.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.DesignerTools.Location = New System.Drawing.Point(0, 0)
        Me.DesignerTools.Name = "DesignerTools"
        Me.DesignerTools.Size = New System.Drawing.Size(44, 521)
        Me.DesignerTools.Stretch = True
        Me.DesignerTools.TabIndex = 0
        Me.DesignerTools.Text = "ToolStrip1"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(42, 6)
        '
        'PointEditModeButton
        '
        Me.PointEditModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PointEditModeButton.Image = Global.Artifice.My.Resources.Resources.PointEdit
        Me.PointEditModeButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PointEditModeButton.Name = "PointEditModeButton"
        Me.PointEditModeButton.Size = New System.Drawing.Size(42, 20)
        Me.PointEditModeButton.Text = "Toggle Point Edit Mode"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(42, 6)
        '
        'FillColorButton
        '
        Me.FillColorButton.Color = System.Drawing.Color.Blue
        Me.FillColorButton.Display = Artifice.ToolStripColorButton.DisplayMode.Fill
        Me.FillColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.FillColorButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.FillColorButton.Name = "FillColorButton"
        Me.FillColorButton.Size = New System.Drawing.Size(42, 4)
        Me.FillColorButton.Text = "Fill Color"
        '
        'LineColorButton
        '
        Me.LineColorButton.Color = System.Drawing.Color.Blue
        Me.LineColorButton.Display = Artifice.ToolStripColorButton.DisplayMode.Line
        Me.LineColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.LineColorButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.LineColorButton.Name = "LineColorButton"
        Me.LineColorButton.Size = New System.Drawing.Size(42, 4)
        Me.LineColorButton.Text = "Line Color"
        '
        'ZoomSelectorButton
        '
        Me.ZoomSelectorButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ZoomSelectorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ZoomSelectorButton.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FitZoomMenuItem, Me.ActualSize100ToolStripMenuItem, Me.ToolStripMenuItem11, Me.ZoomInToolStripMenuItem, Me.ZoomOutToolStripMenuItem})
        Me.ZoomSelectorButton.Image = Global.Artifice.My.Resources.Resources.Zoom
        Me.ZoomSelectorButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ZoomSelectorButton.Name = "ZoomSelectorButton"
        Me.ZoomSelectorButton.Size = New System.Drawing.Size(42, 20)
        Me.ZoomSelectorButton.Text = "Fit"
        '
        'FitZoomMenuItem
        '
        Me.FitZoomMenuItem.Name = "FitZoomMenuItem"
        Me.FitZoomMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.FitZoomMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.FitZoomMenuItem.Text = "&Fit"
        '
        'ActualSize100ToolStripMenuItem
        '
        Me.ActualSize100ToolStripMenuItem.Name = "ActualSize100ToolStripMenuItem"
        Me.ActualSize100ToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.ActualSize100ToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.ActualSize100ToolStripMenuItem.Text = "&Actual Size (100%)"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(242, 6)
        '
        'ZoomInToolStripMenuItem
        '
        Me.ZoomInToolStripMenuItem.Name = "ZoomInToolStripMenuItem"
        Me.ZoomInToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.W), System.Windows.Forms.Keys)
        Me.ZoomInToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.ZoomInToolStripMenuItem.Text = "Zoom &In"
        '
        'ZoomOutToolStripMenuItem
        '
        Me.ZoomOutToolStripMenuItem.Name = "ZoomOutToolStripMenuItem"
        Me.ZoomOutToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.ZoomOutToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.ZoomOutToolStripMenuItem.Text = "Zoom &Out"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(42, 6)
        '
        'CenterStageHorizontalButton
        '
        Me.CenterStageHorizontalButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CenterStageHorizontalButton.Image = Global.Artifice.My.Resources.Resources.CenterStageHorizontal
        Me.CenterStageHorizontalButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CenterStageHorizontalButton.Name = "CenterStageHorizontalButton"
        Me.CenterStageHorizontalButton.Size = New System.Drawing.Size(42, 20)
        Me.CenterStageHorizontalButton.Text = "Center to Stage Horizontally"
        '
        'CenterStageVerticalButton
        '
        Me.CenterStageVerticalButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CenterStageVerticalButton.Image = Global.Artifice.My.Resources.Resources.CenterStageVertictal
        Me.CenterStageVerticalButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CenterStageVerticalButton.Name = "CenterStageVerticalButton"
        Me.CenterStageVerticalButton.Size = New System.Drawing.Size(42, 20)
        Me.CenterStageVerticalButton.Text = "Center to Stage Vertically"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(42, 6)
        '
        'AlignLeftButton
        '
        Me.AlignLeftButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AlignLeftButton.Image = Global.Artifice.My.Resources.Resources.AlignLeft
        Me.AlignLeftButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AlignLeftButton.Name = "AlignLeftButton"
        Me.AlignLeftButton.Size = New System.Drawing.Size(42, 20)
        Me.AlignLeftButton.Text = "Align Left Edges"
        '
        'AlignHorizontalCenterButton
        '
        Me.AlignHorizontalCenterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AlignHorizontalCenterButton.Image = Global.Artifice.My.Resources.Resources.AlignHorizontalCenter
        Me.AlignHorizontalCenterButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AlignHorizontalCenterButton.Name = "AlignHorizontalCenterButton"
        Me.AlignHorizontalCenterButton.Size = New System.Drawing.Size(42, 20)
        Me.AlignHorizontalCenterButton.Text = "Align Horizontal Centers"
        '
        'AlignRightButton
        '
        Me.AlignRightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AlignRightButton.Image = Global.Artifice.My.Resources.Resources.AlignRight
        Me.AlignRightButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AlignRightButton.Name = "AlignRightButton"
        Me.AlignRightButton.Size = New System.Drawing.Size(42, 20)
        Me.AlignRightButton.Text = "Align Right Edges"
        '
        'AlignTopButton
        '
        Me.AlignTopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AlignTopButton.Image = Global.Artifice.My.Resources.Resources.AlignTop
        Me.AlignTopButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AlignTopButton.Name = "AlignTopButton"
        Me.AlignTopButton.Size = New System.Drawing.Size(42, 20)
        Me.AlignTopButton.Text = "Align Top Edges"
        '
        'AlignVerticalCenterButton
        '
        Me.AlignVerticalCenterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AlignVerticalCenterButton.Image = Global.Artifice.My.Resources.Resources.AlignVerticalCenter
        Me.AlignVerticalCenterButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AlignVerticalCenterButton.Name = "AlignVerticalCenterButton"
        Me.AlignVerticalCenterButton.Size = New System.Drawing.Size(42, 20)
        Me.AlignVerticalCenterButton.Text = "Align Vertical Centers"
        '
        'AlignBottomButton
        '
        Me.AlignBottomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AlignBottomButton.Image = Global.Artifice.My.Resources.Resources.AlignBottom
        Me.AlignBottomButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AlignBottomButton.Name = "AlignBottomButton"
        Me.AlignBottomButton.Size = New System.Drawing.Size(42, 20)
        Me.AlignBottomButton.Text = "Align Bottom Edges"
        '
        'HideDesignTimeline
        '
        Me.HideDesignTimeline.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.HideDesignTimeline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.HideDesignTimeline.Image = CType(resources.GetObject("HideDesignTimeline.Image"), System.Drawing.Image)
        Me.HideDesignTimeline.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HideDesignTimeline.Name = "HideDesignTimeline"
        Me.HideDesignTimeline.Size = New System.Drawing.Size(42, 19)
        Me.HideDesignTimeline.Text = "Hide"
        '
        'SurfaceTimelineSplitter
        '
        Me.SurfaceTimelineSplitter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SurfaceTimelineSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SurfaceTimelineSplitter.Location = New System.Drawing.Point(0, 0)
        Me.SurfaceTimelineSplitter.Name = "SurfaceTimelineSplitter"
        Me.SurfaceTimelineSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SurfaceTimelineSplitter.Panel1
        '
        Me.SurfaceTimelineSplitter.Panel1.Controls.Add(Me.SceneSurface1)
        Me.SurfaceTimelineSplitter.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        '
        'SurfaceTimelineSplitter.Panel2
        '
        Me.SurfaceTimelineSplitter.Panel2.Controls.Add(Me.DesignerTimeline)
        Me.SurfaceTimelineSplitter.Panel2.Controls.Add(Me.TimelineToolStrip)
        Me.SurfaceTimelineSplitter.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.SurfaceTimelineSplitter.Size = New System.Drawing.Size(847, 521)
        Me.SurfaceTimelineSplitter.SplitterDistance = 370
        Me.SurfaceTimelineSplitter.TabIndex = 0
        '
        'SceneSurface1
        '
        Me.SceneSurface1.AutoFit = True
        Me.SceneSurface1.ContextMenuStrip = Me.ArtificeObjectMenu
        Me.SceneSurface1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SceneSurface1.Location = New System.Drawing.Point(0, 0)
        Me.SceneSurface1.Name = "SceneSurface1"
        Me.SceneSurface1.PanOffset = New System.Drawing.Point(0, 0)
        Me.SceneSurface1.Size = New System.Drawing.Size(844, 370)
        Me.SceneSurface1.TabIndex = 1
        Me.SceneSurface1.Text = "SceneSurface1"
        Me.SceneSurface1.Zoom = 1.0!
        '
        'DesignerTimeline
        '
        Me.DesignerTimeline.AllowDrop = True
        Me.DesignerTimeline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DesignerTimeline.Location = New System.Drawing.Point(0, 25)
        Me.DesignerTimeline.Name = "DesignerTimeline"
        Me.DesignerTimeline.Size = New System.Drawing.Size(844, 122)
        Me.DesignerTimeline.TabIndex = 1
        Me.DesignerTimeline.Text = "Timeline1"
        '
        'TimelineToolStrip
        '
        Me.TimelineToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewLayer, Me.NewFolder, Me.DeleteLayer, Me.NewSceneButton, Me.SceneList, Me.ToolStripLabel2})
        Me.TimelineToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.TimelineToolStrip.Name = "TimelineToolStrip"
        Me.TimelineToolStrip.Size = New System.Drawing.Size(844, 25)
        Me.TimelineToolStrip.TabIndex = 0
        Me.TimelineToolStrip.Text = "ToolStrip1"
        '
        'NewLayer
        '
        Me.NewLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NewLayer.Enabled = False
        Me.NewLayer.Image = Global.Artifice.My.Resources.Resources.Layer
        Me.NewLayer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewLayer.Name = "NewLayer"
        Me.NewLayer.Size = New System.Drawing.Size(23, 22)
        Me.NewLayer.Text = "Add Layer"
        Me.NewLayer.ToolTipText = "Add Layer (Ctrl+L)"
        '
        'NewFolder
        '
        Me.NewFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NewFolder.Enabled = False
        Me.NewFolder.Image = Global.Artifice.My.Resources.Resources.FolderClosed
        Me.NewFolder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewFolder.Name = "NewFolder"
        Me.NewFolder.Size = New System.Drawing.Size(23, 22)
        Me.NewFolder.Text = "Add Folder"
        '
        'DeleteLayer
        '
        Me.DeleteLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.DeleteLayer.Enabled = False
        Me.DeleteLayer.Image = Global.Artifice.My.Resources.Resources.Remove
        Me.DeleteLayer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.DeleteLayer.Name = "DeleteLayer"
        Me.DeleteLayer.Size = New System.Drawing.Size(23, 22)
        Me.DeleteLayer.Text = "Remove Selected Layer/Folder"
        '
        'NewSceneButton
        '
        Me.NewSceneButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.NewSceneButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NewSceneButton.Image = Global.Artifice.My.Resources.Resources.Scene
        Me.NewSceneButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewSceneButton.Name = "NewSceneButton"
        Me.NewSceneButton.Size = New System.Drawing.Size(23, 22)
        Me.NewSceneButton.Text = "Add New Artwork Page"
        '
        'SceneList
        '
        Me.SceneList.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.SceneList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SceneList.Name = "SceneList"
        Me.SceneList.Size = New System.Drawing.Size(121, 25)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(95, 22)
        Me.ToolStripLabel2.Text = "Current Artwork:"
        '
        'ViewTools
        '
        Me.ViewTools.AutoSize = False
        Me.ViewTools.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ViewTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ViewTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripBindableButton1, Me.ToolStripButton2, Me.ToolStripButton3, Me.Hide})
        Me.ViewTools.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.ViewTools.Location = New System.Drawing.Point(0, 0)
        Me.ViewTools.Name = "ViewTools"
        Me.ViewTools.Size = New System.Drawing.Size(40, 521)
        Me.ViewTools.Stretch = True
        Me.ViewTools.TabIndex = 1
        Me.ViewTools.Text = "ToolStrip1"
        '
        'ToolStripBindableButton1
        '
        Me.ToolStripBindableButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripBindableButton1.Image = Global.Artifice.My.Resources.Resources.Effect
        Me.ToolStripBindableButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripBindableButton1.Name = "ToolStripBindableButton1"
        Me.ToolStripBindableButton1.Size = New System.Drawing.Size(38, 20)
        Me.ToolStripBindableButton1.Text = "View Effects List"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = Global.Artifice.My.Resources.Resources.GenericObject
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(38, 20)
        Me.ToolStripButton2.Text = "View The Properties"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = Global.Artifice.My.Resources.Resources.FolderOpen
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(38, 20)
        Me.ToolStripButton3.Text = "View the List"
        '
        'Hide
        '
        Me.Hide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Hide.Image = Global.Artifice.My.Resources.Resources.Remove
        Me.Hide.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Hide.Name = "Hide"
        Me.Hide.Size = New System.Drawing.Size(38, 19)
        Me.Hide.Text = "Hide"
        '
        'ProjectTree
        '
        Me.ProjectTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProjectTree.HideSelection = False
        Me.ProjectTree.ImageIndex = 0
        Me.ProjectTree.ImageList = Me.ProjectTreeImages
        Me.ProjectTree.Location = New System.Drawing.Point(0, 0)
        Me.ProjectTree.Name = "ProjectTree"
        Me.ProjectTree.SelectedImageIndex = 0
        Me.ProjectTree.ShowPlusMinus = False
        Me.ProjectTree.ShowRootLines = False
        Me.ProjectTree.Size = New System.Drawing.Size(161, 328)
        Me.ProjectTree.TabIndex = 1
        '
        'ProjectTreeImages
        '
        Me.ProjectTreeImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.ProjectTreeImages.ImageSize = New System.Drawing.Size(16, 16)
        Me.ProjectTreeImages.TransparentColor = System.Drawing.Color.Transparent
        '
        'EffectsPanel
        '
        Me.EffectsPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EffectsPanel.Controls.Add(Me.EffectList)
        Me.EffectsPanel.Controls.Add(Me.EffectsToolStrip)
        Me.EffectsPanel.Location = New System.Drawing.Point(718, 33)
        Me.EffectsPanel.Name = "EffectsPanel"
        Me.EffectsPanel.Size = New System.Drawing.Size(161, 153)
        Me.EffectsPanel.TabIndex = 2
        Me.EffectsPanel.Visible = False
        '
        'EffectList
        '
        Me.EffectList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.EffectList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EffectList.HideSelection = False
        Me.EffectList.Location = New System.Drawing.Point(0, 25)
        Me.EffectList.MultiSelect = False
        Me.EffectList.Name = "EffectList"
        Me.EffectList.Size = New System.Drawing.Size(161, 128)
        Me.EffectList.SmallImageList = Me.ProjectTreeImages
        Me.EffectList.TabIndex = 1
        Me.EffectList.UseCompatibleStateImageBehavior = False
        Me.EffectList.View = System.Windows.Forms.View.SmallIcon
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Added Effect"
        Me.ColumnHeader1.Width = 150
        '
        'EffectsToolStrip
        '
        Me.EffectsToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.AddEffectButton, Me.RemoveEffectButton})
        Me.EffectsToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.EffectsToolStrip.Name = "EffectsToolStrip"
        Me.EffectsToolStrip.Size = New System.Drawing.Size(161, 25)
        Me.EffectsToolStrip.TabIndex = 0
        Me.EffectsToolStrip.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(42, 22)
        Me.ToolStripLabel1.Text = "Effects"
        '
        'AddEffectButton
        '
        Me.AddEffectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.AddEffectButton.Image = Global.Artifice.My.Resources.Resources.Effect
        Me.AddEffectButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.AddEffectButton.Name = "AddEffectButton"
        Me.AddEffectButton.Size = New System.Drawing.Size(29, 22)
        Me.AddEffectButton.Text = "Add Effect"
        '
        'RemoveEffectButton
        '
        Me.RemoveEffectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RemoveEffectButton.Image = Global.Artifice.My.Resources.Resources.Remove
        Me.RemoveEffectButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RemoveEffectButton.Name = "RemoveEffectButton"
        Me.RemoveEffectButton.Size = New System.Drawing.Size(23, 22)
        Me.RemoveEffectButton.Text = "Remove Effect"
        '
        'Properties
        '
        Me.Properties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Properties.Location = New System.Drawing.Point(0, 0)
        Me.Properties.Name = "Properties"
        Me.Properties.Size = New System.Drawing.Size(161, 321)
        Me.Properties.TabIndex = 0
        '
        'Opener
        '
        Me.Opener.Filter = "Autodev Artifice Project Files|*.art|All Files|*.*"
        '
        'Saver
        '
        Me.Saver.Filter = "Autodev Artifice Project Files|*.art|All Files|*.*"
        '
        'SVGFileName
        '
        Me.SVGFileName.Filter = "SVG Files|*.svg|All Files|*.*"
        '
        'ImageExportFilename
        '
        Me.ImageExportFilename.Filter = "PNG Files|*.png|All Files|*.*"
        '
        'PropPanel
        '
        Me.PropPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PropPanel.Controls.Add(Me.Properties)
        Me.PropPanel.Location = New System.Drawing.Point(718, 64)
        Me.PropPanel.Name = "PropPanel"
        Me.PropPanel.Size = New System.Drawing.Size(161, 321)
        Me.PropPanel.TabIndex = 3
        Me.PropPanel.Visible = False
        '
        'HierPanel
        '
        Me.HierPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HierPanel.Controls.Add(Me.ProjectTree)
        Me.HierPanel.Location = New System.Drawing.Point(718, 83)
        Me.HierPanel.Name = "HierPanel"
        Me.HierPanel.Size = New System.Drawing.Size(161, 328)
        Me.HierPanel.TabIndex = 4
        Me.HierPanel.Visible = False
        '
        'MainWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(939, 573)
        Me.Controls.Add(Me.HierPanel)
        Me.Controls.Add(Me.PropPanel)
        Me.Controls.Add(Me.EffectsPanel)
        Me.Controls.Add(Me.LeftRightSplitter)
        Me.Controls.Add(Me.StatusBar)
        Me.Controls.Add(Me.MainMenu)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MainMenu
        Me.Name = "MainWindow"
        Me.Text = "Autodev Artifice"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.ArtificeObjectMenu.ResumeLayout(False)
        Me.StatusBar.ResumeLayout(False)
        Me.StatusBar.PerformLayout()
        Me.LeftRightSplitter.Panel1.ResumeLayout(False)
        Me.LeftRightSplitter.Panel2.ResumeLayout(False)
        CType(Me.LeftRightSplitter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LeftRightSplitter.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.DesignerTools.ResumeLayout(False)
        Me.DesignerTools.PerformLayout()
        Me.SurfaceTimelineSplitter.Panel1.ResumeLayout(False)
        Me.SurfaceTimelineSplitter.Panel2.ResumeLayout(False)
        Me.SurfaceTimelineSplitter.Panel2.PerformLayout()
        CType(Me.SurfaceTimelineSplitter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SurfaceTimelineSplitter.ResumeLayout(False)
        Me.TimelineToolStrip.ResumeLayout(False)
        Me.TimelineToolStrip.PerformLayout()
        Me.ViewTools.ResumeLayout(False)
        Me.ViewTools.PerformLayout()
        Me.EffectsPanel.ResumeLayout(False)
        Me.EffectsPanel.PerformLayout()
        Me.EffectsToolStrip.ResumeLayout(False)
        Me.EffectsToolStrip.PerformLayout()
        Me.PropPanel.ResumeLayout(False)
        Me.HierPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusBar As System.Windows.Forms.StatusStrip
    Friend WithEvents LeftRightSplitter As System.Windows.Forms.SplitContainer
    Friend WithEvents Properties As System.Windows.Forms.PropertyGrid
    Friend WithEvents SurfaceTimelineSplitter As System.Windows.Forms.SplitContainer
    Friend WithEvents DesignerTools As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TimelineToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents NewLayer As System.Windows.Forms.ToolStripButton
    Friend WithEvents NewFolder As System.Windows.Forms.ToolStripButton
    Friend WithEvents DeleteLayer As System.Windows.Forms.ToolStripButton
    Friend WithEvents DesignerTimeline As Artifice.Timeline
    Friend WithEvents SceneSurface1 As Artifice.SceneSurface
    Friend WithEvents StatusBarPosition As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ProjectTree As System.Windows.Forms.TreeView
    Friend WithEvents EffectsToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents Opener As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Saver As System.Windows.Forms.SaveFileDialog
    Friend WithEvents SceneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddLayerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ObjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteLayerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProjectTreeImages As System.Windows.Forms.ImageList
    Friend WithEvents EffectsPanel As System.Windows.Forms.Panel
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents AddEffectButton As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents RemoveEffectButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UndoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RedoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectallToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportPNGMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FillColorButton As Artifice.ToolStripColorButton
    Friend WithEvents LineColorButton As Artifice.ToolStripColorButton
    Friend WithEvents ArtificeObjectMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CreateGroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UngroupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BringToFrontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BringForwardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendBackwardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendToBackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ConvertToVectorObjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents NewSceneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SceneList As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents NewSceneButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents CopyToNextKeyframeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToPreviousKeyframeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZoomSelectorButton As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents FitZoomMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ActualSize100ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ZoomInToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZoomOutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RevertToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CenterStageHorizontalButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents CenterStageVerticalButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AlignLeftButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AlignHorizontalCenterButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AlignRightButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AlignTopButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AlignVerticalCenterButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents AlignBottomButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SingleFrameAsSVGToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteSceneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusInformation As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PointEditModeButton As Artifice.ToolStripBindableButton
    Friend WithEvents DuplicateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SVGFileName As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PlayAudioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HighQualityRenderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FlipHorizontalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FlipVerticalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RenameLayerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImageExportFilename As System.Windows.Forms.SaveFileDialog
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents ViewTools As ToolStrip
    Friend WithEvents ToolStripBindableButton1 As ToolStripBindableButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents Hide As ToolStripButton
    Friend WithEvents EffectList As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents PropPanel As Panel
    Friend WithEvents HierPanel As Panel
    Friend WithEvents ToolStripButton3 As ToolStripButton
    Friend WithEvents HideDesignTimeline As ToolStripButton
End Class

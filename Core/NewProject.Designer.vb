<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewProject
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewProject))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ProjectName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.StageWidth = New System.Windows.Forms.NumericUpDown()
        Me.StageHeight = New System.Windows.Forms.NumericUpDown()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.CreateButton = New System.Windows.Forms.Button()
        Me.CancelProjectButton = New System.Windows.Forms.Button()
        Me.FormatSize = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StageWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StageHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Project Name:"
        '
        'ProjectName
        '
        Me.ProjectName.Location = New System.Drawing.Point(112, 27)
        Me.ProjectName.Name = "ProjectName"
        Me.ProjectName.Size = New System.Drawing.Size(291, 20)
        Me.ProjectName.TabIndex = 1
        Me.ProjectName.Text = "Untitled Project"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Size:"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Artifice.My.Resources.Resources.Width
        Me.PictureBox1.Location = New System.Drawing.Point(99, 100)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'StageWidth
        '
        Me.StageWidth.Location = New System.Drawing.Point(121, 98)
        Me.StageWidth.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.StageWidth.Minimum = New Decimal(New Integer() {16, 0, 0, 0})
        Me.StageWidth.Name = "StageWidth"
        Me.StageWidth.Size = New System.Drawing.Size(62, 20)
        Me.StageWidth.TabIndex = 4
        Me.StageWidth.Value = New Decimal(New Integer() {16, 0, 0, 0})
        '
        'StageHeight
        '
        Me.StageHeight.Location = New System.Drawing.Point(211, 98)
        Me.StageHeight.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.StageHeight.Minimum = New Decimal(New Integer() {16, 0, 0, 0})
        Me.StageHeight.Name = "StageHeight"
        Me.StageHeight.Size = New System.Drawing.Size(66, 20)
        Me.StageHeight.TabIndex = 6
        Me.StageHeight.Value = New Decimal(New Integer() {16, 0, 0, 0})
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Artifice.My.Resources.Resources.Height
        Me.PictureBox2.Location = New System.Drawing.Point(189, 100)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 5
        Me.PictureBox2.TabStop = False
        '
        'CreateButton
        '
        Me.CreateButton.Location = New System.Drawing.Point(234, 137)
        Me.CreateButton.Name = "CreateButton"
        Me.CreateButton.Size = New System.Drawing.Size(75, 23)
        Me.CreateButton.TabIndex = 9
        Me.CreateButton.Text = "Create"
        Me.CreateButton.UseVisualStyleBackColor = True
        '
        'CancelProjectButton
        '
        Me.CancelProjectButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelProjectButton.Location = New System.Drawing.Point(315, 137)
        Me.CancelProjectButton.Name = "CancelProjectButton"
        Me.CancelProjectButton.Size = New System.Drawing.Size(75, 23)
        Me.CancelProjectButton.TabIndex = 10
        Me.CancelProjectButton.Text = "Cancel"
        Me.CancelProjectButton.UseVisualStyleBackColor = True
        '
        'FormatSize
        '
        Me.FormatSize.FormattingEnabled = True
        Me.FormatSize.Items.AddRange(New Object() {"A5", "A4", "A3", "B5", "B4", "Letter", "Postcard", "Poster", "Legal", "Tabloid"})
        Me.FormatSize.Location = New System.Drawing.Point(112, 60)
        Me.FormatSize.Name = "FormatSize"
        Me.FormatSize.Size = New System.Drawing.Size(291, 21)
        Me.FormatSize.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Size format:"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'NewProject
        '
        Me.AcceptButton = Me.CreateButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CancelProjectButton
        Me.ClientSize = New System.Drawing.Size(432, 204)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.FormatSize)
        Me.Controls.Add(Me.CancelProjectButton)
        Me.Controls.Add(Me.CreateButton)
        Me.Controls.Add(Me.StageHeight)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.StageWidth)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ProjectName)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NewProject"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Autodev Artifice Project"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StageWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StageHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ProjectName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents StageWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents StageHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents CreateButton As System.Windows.Forms.Button
    Friend WithEvents CancelProjectButton As System.Windows.Forms.Button
    Friend WithEvents FormatSize As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Timer1 As Timer
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildSelector
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChildSelector))
        Me.OkButton = New System.Windows.Forms.Button()
        Me.CancelDialog = New System.Windows.Forms.Button()
        Me.ChildList = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'OkButton
        '
        Me.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OkButton.Enabled = False
        Me.OkButton.Location = New System.Drawing.Point(140, 221)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(75, 23)
        Me.OkButton.TabIndex = 0
        Me.OkButton.Text = "OK"
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'CancelDialog
        '
        Me.CancelDialog.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelDialog.Location = New System.Drawing.Point(243, 221)
        Me.CancelDialog.Name = "CancelDialog"
        Me.CancelDialog.Size = New System.Drawing.Size(75, 23)
        Me.CancelDialog.TabIndex = 1
        Me.CancelDialog.Text = "Cancel"
        Me.CancelDialog.UseVisualStyleBackColor = True
        '
        'ChildList
        '
        Me.ChildList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ChildList.FormattingEnabled = True
        Me.ChildList.Location = New System.Drawing.Point(12, 37)
        Me.ChildList.Name = "ChildList"
        Me.ChildList.Size = New System.Drawing.Size(306, 160)
        Me.ChildList.TabIndex = 2
        '
        'ChildSelector
        '
        Me.AcceptButton = Me.OkButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CancelDialog
        Me.ClientSize = New System.Drawing.Size(330, 256)
        Me.Controls.Add(Me.ChildList)
        Me.Controls.Add(Me.CancelDialog)
        Me.Controls.Add(Me.OkButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ChildSelector"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Autodev Artifice - Child Selector"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents CancelDialog As System.Windows.Forms.Button
    Friend WithEvents ChildList As System.Windows.Forms.ListBox
End Class

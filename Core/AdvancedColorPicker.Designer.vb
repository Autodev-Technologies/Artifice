<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdvancedColorPicker
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
        Dim AdvancedColor1 As Artifice.AdvancedColor = New Artifice.AdvancedColor()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AdvancedColorPicker))
        Me.BindToHue = New System.Windows.Forms.RadioButton()
        Me.BindToSaturation = New System.Windows.Forms.RadioButton()
        Me.BindToV = New System.Windows.Forms.RadioButton()
        Me.BindToB = New System.Windows.Forms.RadioButton()
        Me.BindToG = New System.Windows.Forms.RadioButton()
        Me.BindToR = New System.Windows.Forms.RadioButton()
        Me.HBox = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SBox = New System.Windows.Forms.NumericUpDown()
        Me.VBox = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RBox = New System.Windows.Forms.NumericUpDown()
        Me.GBox = New System.Windows.Forms.NumericUpDown()
        Me.BBox = New System.Windows.Forms.NumericUpDown()
        Me.HexValue = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.OKColorPicker = New System.Windows.Forms.Button()
        Me.CancelColorPicker = New System.Windows.Forms.Button()
        Me.ABox = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Tipper = New System.Windows.Forms.ToolTip(Me.components)
        Me.RecentlyUsedColors = New System.Windows.Forms.FlowLayoutPanel()
        Me.Swatches = New System.Windows.Forms.FlowLayoutPanel()
        Me.AlphaSlider = New Artifice.HorizontalColorSlider()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CurrentPrev = New Artifice.DoubleBufferPanel()
        Me.ColorSurface1 = New Artifice.ColorSurface()
        Me.VerticalSlider = New Artifice.VerticalColorSlider()
        CType(Me.HBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ABox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BindToHue
        '
        Me.BindToHue.AutoSize = True
        Me.BindToHue.Checked = True
        Me.BindToHue.Location = New System.Drawing.Point(387, 111)
        Me.BindToHue.Name = "BindToHue"
        Me.BindToHue.Size = New System.Drawing.Size(36, 17)
        Me.BindToHue.TabIndex = 1
        Me.BindToHue.TabStop = True
        Me.BindToHue.Tag = "H"
        Me.BindToHue.Text = "H:"
        Me.BindToHue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BindToHue.UseVisualStyleBackColor = True
        '
        'BindToSaturation
        '
        Me.BindToSaturation.AutoSize = True
        Me.BindToSaturation.Location = New System.Drawing.Point(387, 134)
        Me.BindToSaturation.Name = "BindToSaturation"
        Me.BindToSaturation.Size = New System.Drawing.Size(35, 17)
        Me.BindToSaturation.TabIndex = 3
        Me.BindToSaturation.Tag = "S"
        Me.BindToSaturation.Text = "S:"
        Me.BindToSaturation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BindToSaturation.UseVisualStyleBackColor = True
        '
        'BindToV
        '
        Me.BindToV.AutoSize = True
        Me.BindToV.Location = New System.Drawing.Point(387, 157)
        Me.BindToV.Name = "BindToV"
        Me.BindToV.Size = New System.Drawing.Size(35, 17)
        Me.BindToV.TabIndex = 4
        Me.BindToV.Tag = "V"
        Me.BindToV.Text = "V:"
        Me.BindToV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BindToV.UseVisualStyleBackColor = True
        '
        'BindToB
        '
        Me.BindToB.AutoSize = True
        Me.BindToB.Location = New System.Drawing.Point(388, 255)
        Me.BindToB.Name = "BindToB"
        Me.BindToB.Size = New System.Drawing.Size(35, 17)
        Me.BindToB.TabIndex = 7
        Me.BindToB.Tag = "B"
        Me.BindToB.Text = "B:"
        Me.BindToB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BindToB.UseVisualStyleBackColor = True
        '
        'BindToG
        '
        Me.BindToG.AutoSize = True
        Me.BindToG.Location = New System.Drawing.Point(388, 232)
        Me.BindToG.Name = "BindToG"
        Me.BindToG.Size = New System.Drawing.Size(36, 17)
        Me.BindToG.TabIndex = 6
        Me.BindToG.Tag = "G"
        Me.BindToG.Text = "G:"
        Me.BindToG.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BindToG.UseVisualStyleBackColor = True
        '
        'BindToR
        '
        Me.BindToR.AutoSize = True
        Me.BindToR.Location = New System.Drawing.Point(388, 209)
        Me.BindToR.Name = "BindToR"
        Me.BindToR.Size = New System.Drawing.Size(36, 17)
        Me.BindToR.TabIndex = 5
        Me.BindToR.Tag = "R"
        Me.BindToR.Text = "R:"
        Me.BindToR.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BindToR.UseVisualStyleBackColor = True
        '
        'HBox
        '
        Me.HBox.Location = New System.Drawing.Point(429, 111)
        Me.HBox.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.HBox.Name = "HBox"
        Me.HBox.Size = New System.Drawing.Size(39, 20)
        Me.HBox.TabIndex = 10
        Me.HBox.Value = New Decimal(New Integer() {360, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(472, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "°"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SBox
        '
        Me.SBox.Location = New System.Drawing.Point(429, 134)
        Me.SBox.Name = "SBox"
        Me.SBox.Size = New System.Drawing.Size(39, 20)
        Me.SBox.TabIndex = 12
        Me.SBox.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'VBox
        '
        Me.VBox.Location = New System.Drawing.Point(429, 157)
        Me.VBox.Name = "VBox"
        Me.VBox.Size = New System.Drawing.Size(39, 20)
        Me.VBox.TabIndex = 13
        Me.VBox.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(472, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "%"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(472, 159)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(15, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "%"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RBox
        '
        Me.RBox.Location = New System.Drawing.Point(429, 209)
        Me.RBox.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.RBox.Name = "RBox"
        Me.RBox.Size = New System.Drawing.Size(39, 20)
        Me.RBox.TabIndex = 16
        Me.RBox.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'GBox
        '
        Me.GBox.Location = New System.Drawing.Point(429, 232)
        Me.GBox.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.GBox.Name = "GBox"
        Me.GBox.Size = New System.Drawing.Size(39, 20)
        Me.GBox.TabIndex = 17
        Me.GBox.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'BBox
        '
        Me.BBox.Location = New System.Drawing.Point(429, 255)
        Me.BBox.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.BBox.Name = "BBox"
        Me.BBox.Size = New System.Drawing.Size(39, 20)
        Me.BBox.TabIndex = 18
        Me.BBox.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'HexValue
        '
        Me.HexValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower
        Me.HexValue.Location = New System.Drawing.Point(404, 295)
        Me.HexValue.MaxLength = 6
        Me.HexValue.Name = "HexValue"
        Me.HexValue.Size = New System.Drawing.Size(64, 20)
        Me.HexValue.TabIndex = 19
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(384, 298)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "#"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(413, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 13)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "new"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(406, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "current"
        '
        'OKColorPicker
        '
        Me.OKColorPicker.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.OKColorPicker.Location = New System.Drawing.Point(445, 360)
        Me.OKColorPicker.Name = "OKColorPicker"
        Me.OKColorPicker.Size = New System.Drawing.Size(75, 23)
        Me.OKColorPicker.TabIndex = 23
        Me.OKColorPicker.Text = "OK"
        Me.OKColorPicker.UseVisualStyleBackColor = True
        '
        'CancelColorPicker
        '
        Me.CancelColorPicker.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelColorPicker.Location = New System.Drawing.Point(557, 360)
        Me.CancelColorPicker.Name = "CancelColorPicker"
        Me.CancelColorPicker.Size = New System.Drawing.Size(75, 23)
        Me.CancelColorPicker.TabIndex = 24
        Me.CancelColorPicker.Text = "Cancel"
        Me.CancelColorPicker.UseVisualStyleBackColor = True
        '
        'ABox
        '
        Me.ABox.Location = New System.Drawing.Point(429, 183)
        Me.ABox.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.ABox.Name = "ABox"
        Me.ABox.Size = New System.Drawing.Size(39, 20)
        Me.ABox.TabIndex = 26
        Me.ABox.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(403, 185)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(17, 13)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "A:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'RecentlyUsedColors
        '
        Me.RecentlyUsedColors.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.RecentlyUsedColors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RecentlyUsedColors.Location = New System.Drawing.Point(506, 23)
        Me.RecentlyUsedColors.Name = "RecentlyUsedColors"
        Me.RecentlyUsedColors.Size = New System.Drawing.Size(126, 100)
        Me.RecentlyUsedColors.TabIndex = 29
        Me.Tipper.SetToolTip(Me.RecentlyUsedColors, "Recently-used colors")
        '
        'Swatches
        '
        Me.Swatches.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Swatches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Swatches.Location = New System.Drawing.Point(506, 150)
        Me.Swatches.Name = "Swatches"
        Me.Swatches.Size = New System.Drawing.Size(126, 199)
        Me.Swatches.TabIndex = 31
        Me.Tipper.SetToolTip(Me.Swatches, "Colors that are remembered between sessions")
        '
        'AlphaSlider
        '
        Me.AlphaSlider.Location = New System.Drawing.Point(7, 322)
        Me.AlphaSlider.MultipleStops = False
        Me.AlphaSlider.Name = "AlphaSlider"
        Me.AlphaSlider.OwnerDraw = False
        Me.AlphaSlider.Size = New System.Drawing.Size(314, 38)
        Me.AlphaSlider.StopPosition = 0.5!
        Me.AlphaSlider.TabIndex = 25
        Me.AlphaSlider.Text = "HorizontalColorSlider1"
        Me.Tipper.SetToolTip(Me.AlphaSlider, "Alpha (Transparency)")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(551, 7)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "recent"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(543, 134)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 13)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "swatches"
        '
        'CurrentPrev
        '
        Me.CurrentPrev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CurrentPrev.Location = New System.Drawing.Point(386, 23)
        Me.CurrentPrev.Name = "CurrentPrev"
        Me.CurrentPrev.Size = New System.Drawing.Size(81, 65)
        Me.CurrentPrev.TabIndex = 8
        '
        'ColorSurface1
        '
        Me.ColorSurface1.Color = Nothing
        Me.ColorSurface1.Location = New System.Drawing.Point(12, 12)
        Me.ColorSurface1.Name = "ColorSurface1"
        Me.ColorSurface1.Size = New System.Drawing.Size(304, 304)
        Me.ColorSurface1.TabIndex = 2
        Me.ColorSurface1.Text = "ColorSurface1"
        Me.ColorSurface1.ZProperty = "H"
        '
        'VerticalSlider
        '
        AdvancedColor1.A = 255
        AdvancedColor1.AnchorProperty = "H"
        AdvancedColor1.B = 0
        AdvancedColor1.G = 0
        AdvancedColor1.H = 180
        AdvancedColor1.R = 0
        AdvancedColor1.S = 0
        AdvancedColor1.V = 0
        Me.VerticalSlider.AdvancedColor = AdvancedColor1
        Me.VerticalSlider.Location = New System.Drawing.Point(318, 7)
        Me.VerticalSlider.Name = "VerticalSlider"
        Me.VerticalSlider.Size = New System.Drawing.Size(43, 314)
        Me.VerticalSlider.SliderValue = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.VerticalSlider.TabIndex = 0
        Me.VerticalSlider.Text = "VerticalColorSlider1"
        Me.VerticalSlider.ZProperty = "H"
        '
        'AdvancedColorPicker
        '
        Me.AcceptButton = Me.OKColorPicker
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CancelColorPicker
        Me.ClientSize = New System.Drawing.Size(644, 395)
        Me.Controls.Add(Me.Swatches)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.RecentlyUsedColors)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.ABox)
        Me.Controls.Add(Me.AlphaSlider)
        Me.Controls.Add(Me.CancelColorPicker)
        Me.Controls.Add(Me.OKColorPicker)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.HexValue)
        Me.Controls.Add(Me.BBox)
        Me.Controls.Add(Me.GBox)
        Me.Controls.Add(Me.RBox)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.VBox)
        Me.Controls.Add(Me.SBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.HBox)
        Me.Controls.Add(Me.CurrentPrev)
        Me.Controls.Add(Me.BindToB)
        Me.Controls.Add(Me.BindToG)
        Me.Controls.Add(Me.BindToR)
        Me.Controls.Add(Me.BindToV)
        Me.Controls.Add(Me.BindToSaturation)
        Me.Controls.Add(Me.ColorSurface1)
        Me.Controls.Add(Me.BindToHue)
        Me.Controls.Add(Me.VerticalSlider)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AdvancedColorPicker"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Autodev Artifice - Advanced Color Picker"
        CType(Me.HBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ABox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents VerticalSlider As Artifice.VerticalColorSlider
    Friend WithEvents BindToHue As System.Windows.Forms.RadioButton
    Friend WithEvents ColorSurface1 As Artifice.ColorSurface
    Friend WithEvents BindToSaturation As System.Windows.Forms.RadioButton
    Friend WithEvents BindToV As System.Windows.Forms.RadioButton
    Friend WithEvents BindToB As System.Windows.Forms.RadioButton
    Friend WithEvents BindToG As System.Windows.Forms.RadioButton
    Friend WithEvents BindToR As System.Windows.Forms.RadioButton
    Friend WithEvents CurrentPrev As DoubleBufferPanel
    Friend WithEvents HBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents VBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents GBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents BBox As System.Windows.Forms.NumericUpDown
    Friend WithEvents HexValue As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents OKColorPicker As System.Windows.Forms.Button
    Friend WithEvents CancelColorPicker As System.Windows.Forms.Button
    Friend WithEvents AlphaSlider As Artifice.HorizontalColorSlider
    Friend WithEvents ABox As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Tipper As System.Windows.Forms.ToolTip
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents RecentlyUsedColors As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Swatches As System.Windows.Forms.FlowLayoutPanel
End Class

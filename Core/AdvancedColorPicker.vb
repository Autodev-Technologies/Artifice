Imports System.Drawing.Drawing2D
Imports System.Text

''' <summary>
''' An Adobe Photoshop-style Color Picker Dialog
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class AdvancedColorPicker

    Private Const SwatchWidth As Integer = 18
    Private Const SwatchHeight As Integer = 18
    Private Const SwatchPadding As Integer = 3

    Private Shared RememberedRecentColors As New List(Of ArtificeColor)

    Private WithEvents MyNewColor As AdvancedColor = AdvancedColor.FromHSV(180, 0, 0)

#Region "Properties"
    Private MyColor As ArtificeColor
    ''' <summary>
    ''' The currently-selected color
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Color As ArtificeColor
        Get
            Return MyColor
        End Get
        Set(value As ArtificeColor)
            MyColor = value
        End Set
    End Property
#End Region

#Region "AlphaSlider_DrawSurface"
    Private Sub AlphaSlider_DrawSurface(sender As Object, e As PaintEventArgs) Handles AlphaSlider.DrawSurface
        Dim br As New LinearGradientBrush(New Point(0, 0), New Point(e.ClipRectangle.Right, 0), System.Drawing.Color.Transparent, System.Drawing.Color.Black)

        e.Graphics.FillRectangle(br, e.ClipRectangle)

        br.Dispose()

    End Sub
#End Region

#Region "AlphaSlider_StopPositionChanged"
    Private Sub AlphaSlider_StopPositionChanged(sender As Object, e As StopPositionChangedEventArgs) Handles AlphaSlider.StopPositionChanged
        MyNewColor.A = AlphaSlider.StopPosition * 255
    End Sub
#End Region

#Region "BindingRadio_CheckedChanged"
    Private Sub BindingRadio_CheckedChanged(sender As Object, e As EventArgs) Handles BindToHue.CheckedChanged, BindToV.CheckedChanged, BindToSaturation.CheckedChanged, BindToR.CheckedChanged, BindToG.CheckedChanged, BindToB.CheckedChanged
        If CType(sender, RadioButton).Checked Then
            'MsgBox(CType(sender, RadioButton).Tag)
            ColorSurface1.ZProperty = CType(sender, RadioButton).Tag
            VerticalSlider.ZProperty = CType(sender, RadioButton).Tag
        End If
    End Sub
#End Region

#Region "CurrentPrev_MouseDown"
    Private Sub CurrentPrev_MouseDown(sender As Object, e As MouseEventArgs) Handles CurrentPrev.MouseDown
        If e.Y > CurrentPrev.ClientRectangle.Height / 2 Then
            MyNewColor.Update(Color.Color)

            MyNewColor.UpdateFromRGB()

            CurrentPrev.Refresh()
        End If
    End Sub
#End Region

#Region "CurrentPrev_Paint"
    Private Sub CurrentPrev_Paint(sender As Object, e As PaintEventArgs) Handles CurrentPrev.Paint
        Dim tbr As New TextureBrush(My.Resources.Checkerboard)

        e.Graphics.FillRectangle(tbr, New Rectangle(0, 0, CurrentPrev.ClientRectangle.Width, CurrentPrev.ClientRectangle.Height))

        tbr.Dispose()

        Dim br As New SolidBrush(MyNewColor.Color)

        e.Graphics.FillRectangle(br, New Rectangle(0, 0, CurrentPrev.ClientRectangle.Width, CurrentPrev.ClientRectangle.Height / 2))

        br.Dispose()

        Dim currentBrush As Brush = Color.GetBrush()

        e.Graphics.FillRectangle(currentBrush, New Rectangle(0, CurrentPrev.ClientRectangle.Height / 2, CurrentPrev.ClientRectangle.Width, CurrentPrev.ClientRectangle.Height / 2))

        currentBrush.Dispose()

        e.Graphics.DrawLine(Pens.Black, New Point(0, Height / 2), New Point(Width, Height / 2))
    End Sub
#End Region

#Region "HexValue_KeyDown"
    Private Sub HexValue_KeyDown(sender As Object, e As KeyEventArgs) Handles HexValue.KeyDown
        Select Case e.KeyCode
            Case Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D0, Keys.D9, _
                Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9, _
                Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.Back, Keys.Delete, Keys.Home, Keys.End

                'Do nothing
            Case Else
                e.SuppressKeyPress = True
        End Select
    End Sub
#End Region

#Region "HexValue_KeyUp"
    Private Sub HexValue_KeyUp(sender As Object, e As KeyEventArgs) Handles HexValue.KeyUp
        Try
            Dim c = System.Drawing.ColorTranslator.FromHtml("#" & HexValue.Text)

            MyNewColor.UpdateRGB(c.R, c.G, c.B)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

#Region "HexValue_LostFocus"
    Private Sub HexValue_LostFocus(sender As Object, e As EventArgs) Handles HexValue.LostFocus
        HexValue.Text = MyNewColor.ToHexString()
    End Sub
#End Region

#Region "MyNewColor_PropertyChanged"
    Private Sub MyNewColor_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles MyNewColor.PropertyChanged
        If Not HexValue.Focused Then HexValue.Text = MyNewColor.ToHexString()
        CurrentPrev.Invalidate()

        If e.PropertyName = "A" Then
            AlphaSlider.StopPosition = MyNewColor.A / 255
        End If
    End Sub
#End Region

#Region "OKColorPicker_Click"
    Private Sub OKColorPicker_Click(sender As Object, e As EventArgs) Handles OKColorPicker.Click
        MyColor = New ArtificeColor(MyNewColor.Color)

        Dim found As Boolean = False

        'Go through our list of remembered colors
        For Each c As ArtificeColor In RememberedRecentColors
            'If it's already in there, move it to the front of the list:
            If c.Equals(MyColor) Then
                RememberedRecentColors.Remove(c)
                RememberedRecentColors.Insert(0, c)
                found = True
                Exit For
            End If
        Next

        'If it wasn't in our list of remembered colors, add it at the front of the list:
        If Not found Then
            RememberedRecentColors.Insert(0, New ArtificeColor(MyNewColor.Color))
        End If
    End Sub
#End Region

#Region "OnLoad"
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        MyNewColor.A = Color.Alpha
        MyNewColor.R = Color.Red
        MyNewColor.G = Color.Green
        MyNewColor.B = Color.Blue

        MyNewColor.UpdateFromRGB()

        ColorSurface1.Color = MyNewColor
        VerticalSlider.AdvancedColor = MyNewColor

        HBox.DataBindings.Add(New Binding("Value", MyNewColor, "H"))
        SBox.DataBindings.Add(New Binding("Value", MyNewColor, "S"))
        VBox.DataBindings.Add(New Binding("Value", MyNewColor, "V"))

        ABox.DataBindings.Add(New Binding("Value", MyNewColor, "A"))

        RBox.DataBindings.Add(New Binding("Value", MyNewColor, "R"))
        GBox.DataBindings.Add(New Binding("Value", MyNewColor, "G"))
        BBox.DataBindings.Add(New Binding("Value", MyNewColor, "B"))

        AlphaSlider.StopPosition = MyNewColor.A / 255

        Dim sb As New SwatchButton()

        sb.Width = SwatchWidth
        sb.Height = SwatchHeight

        sb.Margin = New Padding(SwatchPadding)

        AddHandler sb.Click, Sub(s As Object, ce As EventArgs)
                                 Dim found As Boolean = False
                                 Dim vc As New ArtificeColor(MyNewColor.Color)

                                 For Each b As SwatchButton In Swatches.Controls
                                     If b.ArtificeColor IsNot Nothing AndAlso vc.Equals(b.ArtificeColor) Then
                                         found = True
                                         Exit For
                                     End If
                                 Next

                                 If Not found Then
                                     Dim swatch As New SwatchButton()

                                     swatch.ArtificeColor = vc
                                     swatch.Height = SwatchWidth
                                     swatch.Width = SwatchHeight
                                     swatch.Margin = New Padding(SwatchPadding)

                                     AddHandler swatch.Click, Sub(swatchObject As Object, se As EventArgs)
                                                                  MyNewColor.Update(CType(swatchObject, SwatchButton).ArtificeColor.Color)
                                                              End Sub

                                     Swatches.Controls.Add(swatch)
                                     Swatches.Controls.SetChildIndex(swatch, 1)

                                     SaveSwatches()
                                 End If
                             End Sub

        Tipper.SetToolTip(sb, "Add new color to swatch list")

        Swatches.Controls.Add(sb)

        For Each vc As ArtificeColor In RememberedRecentColors
            sb = New SwatchButton()

            sb.ArtificeColor = vc
            sb.Width = SwatchWidth
            sb.Height = SwatchHeight

            sb.Margin = New Padding(SwatchPadding)

            AddHandler sb.Click, Sub(s As Object, ce As EventArgs)
                                     MyNewColor.Update(CType(s, SwatchButton).ArtificeColor.Color)
                                 End Sub

            RecentlyUsedColors.Controls.Add(sb)
        Next

        For Each stringColor As String In My.Settings.Swatches.Split("|")
            If stringColor <> "" AndAlso IsNumeric(stringColor) Then
                sb = New SwatchButton()

                sb.ArtificeColor = New ArtificeColor(CInt(stringColor))
                sb.Width = SwatchWidth
                sb.Height = SwatchHeight

                sb.Margin = New Padding(SwatchPadding)

                AddHandler sb.Click, Sub(s As Object, ce As EventArgs)
                                         MyNewColor.Update(CType(s, SwatchButton).ArtificeColor.Color)
                                     End Sub

                Swatches.Controls.Add(sb)
            End If
        Next

    End Sub
#End Region

#Region "SaveSwatches"
    Private Sub SaveSwatches()
        Dim saveString As New StringBuilder
        For Each b As SwatchButton In Swatches.Controls
            If b.ArtificeColor IsNot Nothing Then
                If saveString.Length > 0 Then saveString.Append("|")
                saveString.Append(b.ArtificeColor.Color.ToArgb().ToString())
            End If
        Next
        My.Settings.Swatches = saveString.ToString()
        My.Settings.Save()
    End Sub
#End Region

End Class
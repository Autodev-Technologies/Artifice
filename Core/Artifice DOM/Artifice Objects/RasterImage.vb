Imports System.ComponentModel
Imports System.Drawing.Drawing2D

''' <summary>
''' A skewable raster image
''' </summary>
''' <remarks>Created by Autodev. Inherits from VectorObject to take advantage of the VectorPoint logic</remarks>
Public Class RasterImage
    Inherits VectorObject

#Region "Properties"

    Public Overrides ReadOnly Property Icon As Image
        Get
            Return My.Resources.Image
        End Get
    End Property

    Private MyImageFile As String
    ''' <summary>
    ''' The filename of the Image to display
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Image"), Description("The filename of the Image to display")> _
    Public Property ImageFile As String
        Get
            Return MyImageFile
        End Get
        Set(value As String)
            Dim changed As Boolean = value <> MyImageFile

            MyImageFile = value

            If changed Then
                If Not Children.Any Then CalculatePoints()
                OnPropertyChanged("ImageFile")
            End If
        End Set
    End Property

#End Region

#Region "CalculatePoints"
    ''' <summary>
    ''' Internally caculates the anchor points for this image
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculatePoints()
        Dim i As Image = Designer.Images(ImageFile)

        Dim gp As New GraphicsPath

        gp.AddRectangle(New Rectangle(i.Width / -2, i.Height / -2, i.Width, i.Height))

        SetPath(gp)
    End Sub
#End Region

#Region "GetRange"
    ''' <summary>
    ''' Computes the range between two points
    ''' </summary>
    ''' <param name="pt1">The start point</param>
    ''' <param name="pt2">The end point</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRange(pt1 As PointF, pt2 As PointF) As Single
        Dim rangeX As Single = Math.Abs(pt1.X - pt2.X)
        Dim rangeY As Single = Math.Abs(pt1.Y - pt2.Y)

        Return IIf(rangeX < rangeY, rangeY, rangeX)
    End Function
#End Region

#Region "OnRender"
    Protected Overrides Sub OnRender(rc As RenderContext)
        Dim bounds As RectangleF = GetBounds()

        'Rapid Image drawn-to-fit path code inspired by:
        ' https://social.msdn.microsoft.com/Forums/vstudio/en-US/33590248-5366-4c05-9ae9-8c039f04f1a8/pixel-drawing?forum=vbgeneral
        ' http://www.vbforums.com/showthread.php?700187-Code-for-a-four-point-transformation-of-an-image
        ' https://social.msdn.microsoft.com/Forums/en-US/61a00008-f8c0-4084-8e0f-3c0107f678fd/vbnet-all-points-between-2-points?forum=Vsexpressvb

        Dim source As Bitmap = Designer.Images(ImageFile)
        Dim sourceData As System.Drawing.Imaging.BitmapData = Nothing
        Dim sourceBytes((source.Width * source.Height * 4) - 1) As Byte '4 bytes per pixel, ARGB

        Dim rendered As New Bitmap(CInt(bounds.Width), CInt(bounds.Height))
        Dim renderedData As System.Drawing.Imaging.BitmapData = Nothing
        Dim renderedBytes((bounds.Width * bounds.Height * 4) - 1) As Byte '4 bytes per pixel, ARGB

        'Make our image initially transparent:
        Dim clearer As Graphics = Graphics.FromImage(rendered)

        clearer.Clear(Color.Transparent)

        clearer.Dispose()

        renderedData = rendered.LockBits(New Rectangle(0, 0, rendered.Width, rendered.Height), Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format32bppArgb)
        sourceData = source.LockBits(New Rectangle(0, 0, source.Width, source.Height), Imaging.ImageLockMode.ReadOnly, Imaging.PixelFormat.Format32bppArgb)

        'copy raw color data to managed arrays
        System.Runtime.InteropServices.Marshal.Copy(sourceData.Scan0, sourceBytes, 0, sourceBytes.Length)
        System.Runtime.InteropServices.Marshal.Copy(renderedData.Scan0, renderedBytes, 0, renderedBytes.Length)

        'Draw to fit the skew:
        Dim topLeft As New Point(Children(0).As(Of VectorPoint).X.Delta, Children(0).As(Of VectorPoint).Y.Delta)
        Dim topRight As New Point(Children(1).As(Of VectorPoint).X.Delta, Children(1).As(Of VectorPoint).Y.Delta)
        Dim bottomLeft As New Point(Children(3).As(Of VectorPoint).X.Delta, Children(3).As(Of VectorPoint).Y.Delta)
        Dim bottomRight As New Point(Children(2).As(Of VectorPoint).X.Delta, Children(2).As(Of VectorPoint).Y.Delta)
        Dim range As Integer

        Dim currentLeft As PointF = topLeft
        Dim currentRight As PointF = topRight

        'Cacluate the left point delta amounts:
        range = GetRange(currentLeft, bottomLeft)

        Dim leftDX As Single = (bottomLeft.X - currentLeft.X) / range
        Dim leftDY As Single = (bottomLeft.Y - currentLeft.Y) / range

        'Cacluate the right point delta amounts:
        range = GetRange(currentRight, bottomRight)

        Dim rightDX As Single = (bottomRight.X - currentRight.X) / range
        Dim rightDY As Single = (bottomRight.Y - currentRight.Y) / range


        While CInt(currentLeft.X) <> bottomLeft.X OrElse CInt(currentLeft.Y) <> bottomLeft.Y _
            OrElse CInt(currentRight.X) <> bottomRight.X OrElse CInt(currentRight.Y) <> bottomRight.Y

            Dim dx, dy, cx, cy As Single

            range = GetRange(currentLeft, currentRight)

            dx = (currentRight.X - currentLeft.X) / range
            dy = (currentRight.Y - currentLeft.Y) / range

            cx = currentLeft.X
            cy = currentLeft.Y

            For i As Integer = 1 To range
                Dim renderedIndex As Integer = (CInt(cy - bounds.Top) * rendered.Width * 4) + (CInt(cx - bounds.Left) * 4)

                ' We know what percentage of this line we're on, so let's start with that:
                Dim percentageX As Single = 0

                If currentRight.X <> currentLeft.X Then percentageX = ((cx - currentLeft.X) / (currentRight.X - currentLeft.X))

                'The top line y value will be the top line range * percentageX * dy, I think:
                Dim topY As Single = topLeft.Y + percentageX * (topRight.Y - topLeft.Y)
                Dim bottomY As Single = bottomLeft.Y + percentageX * (bottomRight.Y - bottomLeft.Y)

                Dim sourceX As Integer = source.Width * percentageX
                Dim sourceY As Integer

                If bottomY = topY Then
                    sourceY = 0
                Else
                    sourceY = Math.Min(Math.Abs(source.Height * (cy - topY) / (bottomY - topY)), source.Height)
                End If

                Dim sourceIndex = (sourceY * source.Width * 4) + sourceX * 4

                If renderedIndex < renderedBytes.Count AndAlso sourceIndex < sourceBytes.Count Then
                    renderedBytes(renderedIndex) = sourceBytes(sourceIndex)
                    renderedBytes(renderedIndex + 1) = sourceBytes(sourceIndex + 1)
                    renderedBytes(renderedIndex + 2) = sourceBytes(sourceIndex + 2)
                    renderedBytes(renderedIndex + 3) = 255
                End If

                'I write the next pixel just to try and seal up gaps because of floating-point errors
                'Is this called "stitching"? I'm not much of a graphics person. :(
                If renderedIndex + 4 < renderedBytes.Count AndAlso sourceIndex + 4 < sourceBytes.Count AndAlso i < range - 1 Then
                    renderedBytes(renderedIndex + 4) = sourceBytes(sourceIndex + 4)
                    renderedBytes(renderedIndex + 5) = sourceBytes(sourceIndex + 5)
                    renderedBytes(renderedIndex + 6) = sourceBytes(sourceIndex + 6)
                    renderedBytes(renderedIndex + 7) = 255
                End If

                cx += dx
                cy += dy
            Next

            If CInt(currentLeft.X) <> bottomLeft.X Then currentLeft.X += leftDX
            If CInt(currentLeft.Y) <> bottomLeft.Y Then currentLeft.Y += leftDY
            If CInt(currentRight.X) <> bottomRight.X Then currentRight.X += rightDX
            If CInt(currentRight.Y) <> bottomRight.Y Then currentRight.Y += rightDY
        End While


        'copy modified bytes from managed array back to raw color data
        System.Runtime.InteropServices.Marshal.Copy(renderedBytes, 0, renderedData.Scan0, renderedBytes.Length)

        rendered.UnlockBits(renderedData)
        source.UnlockBits(sourceData)

        rc.Graphics.DrawImage(rendered, bounds)

        'If Not Designer.Exporting AndAlso Designer.SelectedObjects.Contains(Me) Then
        '    Dim outline As New Pen(Brushes.Red, 1.0)

        '    'Draw our points if we're selected and in point editing mode:
        '    If Designer.PointEditMode Then
        '        For c As Integer = 0 To Children.Count - 1
        '            Dim child As VectorPoint = Children(c)

        '            bounds = child.GetBounds()

        '            If Designer.SelectedPoint Is child Then
        '                rc.Graphics.FillEllipse(Brushes.Red, bounds.X, bounds.Y, bounds.Width, bounds.Height)
        '            Else
        '                rc.Graphics.DrawEllipse(outline, bounds.X, bounds.Y, bounds.Width, bounds.Height)
        '            End If
        '        Next
        '    End If

        '    outline.Dispose()
        'End If

        'We have to explicitly call Render on all our children because the base class (VectorObject) 
        'will screw up our image if we call MyBase.Render
        For Each child As ArtificeObject In Children
            child.Render(rc)
        Next
    End Sub

#End Region

End Class

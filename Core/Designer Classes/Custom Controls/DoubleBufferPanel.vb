''' <summary>
''' A Panel object with an OptimizedDoubleBuffer
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class DoubleBufferPanel
    Inherits Panel

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
    End Sub
End Class

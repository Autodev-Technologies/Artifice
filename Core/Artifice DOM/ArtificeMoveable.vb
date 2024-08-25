Imports System.ComponentModel

''' <summary>
''' The abstract base class for all visual objects that have a location
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public MustInherit Class ArtificeMoveable
    Inherits ArtificeObject

#Region "Properties"

    Private WithEvents MyOpacity As New LerpableSingle(1.0)
    ''' <summary>
    ''' The opacity with which this object is drawn
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Appearance"), Description("The opacity with which this object is drawn")> _
    Public Property Opacity As LerpableSingle
        Get
            Return MyOpacity
        End Get
        Set(value As LerpableSingle)
            Dim changed As Boolean = Not MyOpacity.Equals(value)

            MyOpacity = value

            If changed Then
                OnPropertyChanged("Opacity")
            End If
        End Set
    End Property

    'We make this protected so VectorPoints can manually manage it
    Protected MyTransformedBounds As RectangleF
    ''' <summary>
    ''' The bounds of this object, in Device coordinates
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Set after each Render, this caches the transformed bounds of this object to assist with object location at design time</remarks>
    <Browsable(False)> _
    Public ReadOnly Property TransformedBounds As RectangleF
        Get
            Return MyTransformedBounds
        End Get
    End Property

    Private MyTransformedLocation As PointF
    ''' <summary>
    ''' The location of this object, in Device coordinates
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Set after each Render, this caches the transformed location of this object to assist with object rotation at design time</remarks>
    Public ReadOnly Property TransformedLocation As PointF
        Get
            Return MyTransformedLocation
        End Get
    End Property

    Private WithEvents MyX As New LerpableSingle(0.0)
    ''' <summary>
    ''' The Lerpable X-Coordinate of this object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Position"), Description("The Lerpable X-Coordinate of this object")> _
    Public Property X As LerpableSingle
        Get
            Return MyX
        End Get
        Set(value As LerpableSingle)
            Dim changed As Boolean = Not MyX.Equals(value)

            MyX = value

            If changed Then
                OnPropertyChanged("X")
            End If
        End Set
    End Property

    Private WithEvents MyY As New LerpableSingle(0.0)
    ''' <summary>
    ''' The Lerpable Y-Coordinated of this object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Position"), Description("The Lerpable Y-Coordinate of this object")> _
    Public Property Y As LerpableSingle
        Get
            Return MyY
        End Get
        Set(value As LerpableSingle)
            Dim changed As Boolean = Not MyY.Equals(value)

            MyY = value

            If changed Then
                OnPropertyChanged("Y")
            End If
        End Set
    End Property
#End Region

#Region "Flip"
    Public Overrides Sub Flip(flipX As Boolean, flipY As Boolean)
        Dim b As RectangleF = GetBounds()
        For Each child As ArtificeObject In Children
            If child.Is(Of ArtificeMoveable)() Then
                Dim vm As ArtificeMoveable = child.As(Of ArtificeMoveable)()

                'When we flip, we flip around the actual center of the object (which might not be 0, 0 of the parent):

                If flipX Then
                    Dim newX As Single = b.Center().X + (b.Center().X - vm.X.Value)
                    Designer.AddUndo(New PropertyChangeUndo(vm.X, "Value", vm.X.Value, newX))
                    vm.X.Value = newX
                End If

                If flipY Then
                    Dim newY As Single = b.Center().Y + (b.Center().Y - vm.Y.Value)
                    Designer.AddUndo(New PropertyChangeUndo(vm.Y, "Value", vm.Y.Value, newY))
                    vm.Y.Value = newY
                End If
            Else
                child.Flip(flipX, flipY)
            End If
        Next
    End Sub
#End Region

#Region "MyOpacity_PropertyChanged"
    ''' <summary>
    ''' Handles the change of our Opacity property's Value member to ensure that it's bubbled up through the hierarchy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MyOpacity_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles MyOpacity.PropertyChanged
        If e.PropertyName = "Value" Then OnPropertyChanged("Opacity")
    End Sub
#End Region

#Region "MyX_PropertyChanged"
    ''' <summary>
    ''' Handles the change of our X property's Value member to ensure that it's bubbled up through the hierarchy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MyX_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles MyX.PropertyChanged
        If e.PropertyName = "Value" Then OnPropertyChanged("X")
    End Sub
#End Region

#Region "MyY_PropertyChanged"
    ''' <summary>
    ''' Handles the change of our X property's Value member to ensure that it's bubbled up through the hierarchy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MyY_PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles MyY.PropertyChanged
        If e.PropertyName = "Value" Then OnPropertyChanged("Y")
    End Sub
#End Region

#Region "OnAfterRender"
    Protected Overrides Sub OnAfterRender(rc As RenderContext)
        Dim b As RectangleF = GetBounds()

        'Calculate and cache my transformed bounds:
        Dim points() As Point = New Point() {New Point(b.X, b.Y), New Point(b.Right, b.Y), New Point(b.Right, b.Bottom), New Point(b.X, b.Bottom)}

        Dim locPoints() As PointF = New PointF() {New PointF(0, 0)}

        rc.Graphics.TransformPoints(Drawing2D.CoordinateSpace.Device, Drawing2D.CoordinateSpace.World, points)
        rc.Graphics.TransformPoints(Drawing2D.CoordinateSpace.Device, Drawing2D.CoordinateSpace.World, locPoints)

        MyTransformedLocation = locPoints(0)

        Dim minX As Integer = points(0).X
        Dim minY As Integer = points(0).Y
        Dim maxX As Integer = points(0).X
        Dim maxY As Integer = points(0).Y

        For Each p As Point In points
            minX = Math.Min(minX, p.X)
            minY = Math.Min(minY, p.Y)
            maxX = Math.Max(maxX, p.X)
            maxY = Math.Max(maxY, p.Y)
        Next

        MyTransformedBounds = New RectangleF(minX, minY, (maxX - minX), (maxY - minY))

        'Do whatever our base class would have done
        MyBase.OnAfterRender(rc)

        'Remove our transformed opacity from the stack:
        rc.OpacityStack.Pop()
    End Sub
#End Region

#Region "OnBeforeRender"
    Protected Overrides Sub OnBeforeRender(rc As RenderContext)
        MyBase.OnBeforeRender(rc)

        'Restrict our opacity to the range 0.0 to 1.0:
        Dim transformedOpacity As Single = Math.Min(1.0, Math.Max(0.0, MyOpacity.Delta))

        'If an ancestor had set an opacity, alter ours by theirs:
        If rc.OpacityStack.Count > 0 Then
            transformedOpacity *= rc.OpacityStack.Peek
        End If

        'Push our opacity onto the stack:
        rc.OpacityStack.Push(transformedOpacity)

        'Translate our origion to the position of our object:
        rc.Graphics.TranslateTransform(X.Delta, Y.Delta)
    End Sub
#End Region

#Region "OnGetBounds"
    Protected Overrides Function OnGetBounds() As RectangleF
        'By default return a 10x10 rectangle centered on our coordinates:
        'Return New RectangleF(X.Delta - 5, Y.Delta - 5, 10, 10)
        Return New RectangleF(-5, -5, 10, 10)
    End Function
#End Region


End Class

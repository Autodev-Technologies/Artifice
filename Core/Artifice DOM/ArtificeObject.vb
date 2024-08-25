Imports System.Reflection
Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Text

''' <summary>
''' The base class for all objects in the Artifice DOM
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public MustInherit Class ArtificeObject
    Inherits PropertyChanger
    Implements ICloneable

    'The internal Id counter
    Private Shared MyNextId As Integer = 1

    'So that we don't have to keep querying the metadata, we'll keep a cache of all lerpable properties
    'for each type dervied from ArtificeObject:
    Private Shared CachedLerpables As New Dictionary(Of Type, List(Of PropertyInfo))

#Region "Properties"

    Private MyChildren As New List(Of ArtificeObject)
    ''' <summary>
    ''' A read-only list of the children of this object
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Public ReadOnly Property Children As ArtificeObject()
        Get
            Return MyChildren.ToArray()
        End Get
    End Property

    ''' <summary>
    ''' An internally modifiable list of this object's children
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Protected ReadOnly Property ChildrenList As List(Of ArtificeObject)
        Get
            Return MyChildren
        End Get
    End Property

    ''' <summary>
    ''' The instance of the Designer singleton
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Protected ReadOnly Property Designer As Designer
        Get
            Return Global.Artifice.Designer.Singleton
        End Get
    End Property

    Private MyEffects As New List(Of EffectBase)
    ''' <summary>
    ''' A list of effects applied to this ArtificeObject
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Public ReadOnly Property Effects As List(Of EffectBase)
        Get
            Return MyEffects
        End Get
    End Property

    ''' <summary>
    ''' The visual icon used when displaying this object in lists
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Public Overridable ReadOnly Property Icon As Image
        Get
            Return My.Resources.GenericObject
        End Get
    End Property

    Private Property MyId As Integer
    ''' <summary>
    ''' This object's internal identifier
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <[ReadOnly](True), Category("Design"), Description("The internal identifier of this object")>
    Public ReadOnly Property Id As Integer
        Get
            Return MyId
        End Get
    End Property

    ''' <summary>
    ''' The most recent id assigned to a ArtificeObject in this project
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared ReadOnly Property LastId As Integer
        Get
            Return MyNextId
        End Get
    End Property

    Private MyName As String = ""
    ''' <summary>
    ''' The name of this ArtificeObject
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Design"), DisplayName("(Name)"), Description("The name of this object")>
    Public Property Name As String
        Get
            Return MyName
        End Get
        Set(value As String)
            Dim changed As Boolean = MyName <> value.Trim()

            MyName = value.Trim()

            If changed Then OnPropertyChanged("Name")
        End Set
    End Property

    ''' <summary>
    ''' Returns the next sibling of this ArtificeObject, or Nothing if this is the last
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Public ReadOnly Property [Next] As ArtificeObject
        Get
            If Parent Is Nothing Then Return Nothing

            Dim i As Integer = Parent.MyChildren.IndexOf(Me)

            If i = Parent.MyChildren.Count() - 1 Then Return Nothing

            Return Parent.MyChildren(i + 1)
        End Get
    End Property

    ''' <summary>
    ''' Returns the next available id for this project
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property NextId As Integer
        Get
            MyNextId += 1

            Return MyNextId
        End Get
    End Property

    Private MyParent As ArtificeObject
    ''' <summary>
    ''' Returns the parent of this ArtificeObject, or Nothing if this ArtificeObject is unassigned or is a Project
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Public ReadOnly Property Parent As ArtificeObject
        Get
            Return MyParent
        End Get
    End Property

    ''' <summary>
    ''' Returns the position of this ArtificeObject with respect to its siblings
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Public ReadOnly Property ParentIndex As Integer
        Get
            If MyParent Is Nothing Then Return -1
            Return MyParent.MyChildren.IndexOf(Me)
        End Get
    End Property

    ''' <summary>
    ''' Returns the previous sibling of this ArtificeObject, or Nothing if this is the first
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Public ReadOnly Property Previous As ArtificeObject
        Get
            If Parent Is Nothing Then Return Nothing

            Dim i As Integer = Parent.MyChildren.IndexOf(Me)

            If i = 0 Then Return Nothing

            Return Parent.MyChildren(i - 1)
        End Get
    End Property

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructs a new ArtificeObject
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        MyId = NextId

        ' Use reflection to get a list of all Lerpable properties for this type, and cache it:
        ' (we do this to avoid having to use Reflection during Lerping)
        If Not CachedLerpables.ContainsKey(Me.GetType()) Then
            Dim lerpables As New List(Of PropertyInfo)

            For Each pi As PropertyInfo In Me.GetType().GetProperties
                If pi.PropertyType Is GetType(LerpableColor) OrElse pi.PropertyType Is GetType(LerpableInteger) OrElse pi.PropertyType Is GetType(LerpableSingle) Then
                    lerpables.Add(pi)
                End If
            Next

            CachedLerpables.Add(Me.GetType(), lerpables)
        End If
    End Sub
#End Region

#Region "AddAfter"
    ''' <summary>
    ''' Adds the given ArtificeObject to this object's parent, after this object in the list of children
    ''' </summary>
    ''' <param name="sibling">The sibling to add</param>
    ''' <remarks></remarks>
    Public Sub AddAfter(sibling As ArtificeObject)
        If MyParent Is Nothing Then Throw New Exception("You cannot add a sibling at this level")

        'If this sibling has a parent, remove it from the parent's list:
        sibling.Remove()

        'Add it after me
        MyParent.MyChildren.Insert(MyParent.MyChildren.IndexOf(Me) + 1, sibling)

        'Set it's parent to my parent
        sibling.MyParent = MyParent

        'Fire off events:
        MyParent.OnChildAdded()
        MyParent.OnPropertyChanged("Children")
    End Sub
#End Region

#Region "AddBefore"
    ''' <summary>
    ''' Adds the given ArtificeObject to this object's parent, before this object in the list of children
    ''' </summary>
    ''' <param name="sibling">The sibling to add</param>
    ''' <remarks></remarks>
    Public Sub AddBefore(sibling As ArtificeObject)
        If MyParent Is Nothing Then Throw New Exception("You cannot add a sibling at this level")

        'If this sibling has a parent, remove it from the parent's list:
        sibling.Remove()

        'Add it before me
        MyParent.MyChildren.Insert(MyParent.MyChildren.IndexOf(Me), sibling)

        'Set it's parent to my parent
        sibling.MyParent = MyParent

        'Fire off events:
        MyParent.OnChildAdded()
        MyParent.OnPropertyChanged("Children")
    End Sub
#End Region

#Region "AddChild"
    ''' <summary>
    ''' Adds the given object to this ArtificeObject's children, at the end of the list
    ''' </summary>
    ''' <param name="child">The child to be added</param>
    ''' <remarks></remarks>
    Public Sub AddChild(child As ArtificeObject)
        'If this child already has a parent, remove it from its parent's list
        child.Remove()

        'Add it to my children:
        MyChildren.Add(child)

        'Set its parent to me:
        child.MyParent = Me

        'Fire events:
        OnChildAdded()
        OnPropertyChanged("Children")
    End Sub
#End Region

#Region "AddEffect"
    ''' <summary>
    ''' Adds an effect to this ArtificeObject
    ''' </summary>
    ''' <param name="e">The effect to add</param>
    ''' <remarks></remarks>
    Public Sub AddEffect(e As EffectBase)
        'If this effect belongs to another ArtificeObject, remove it:
        e.Remove()

        'Set its parent to Me
        e.MyParent = Me

        'Add it to my list of effects:
        Effects.Add(e)

        'Fire events:
        OnPropertyChanged("Effects")
    End Sub
#End Region

#Region "Animate"
    ''' <summary>
    ''' Animates the current object (and it's children), against the given AnimationContext
    ''' </summary>
    ''' <param name="context">The context to animate against</param>
    ''' <remarks></remarks>
    Public Sub Animate(context As AnimationContext)
        Dim effectsStartIndex As Integer = context.Effects.Count()

        'Animate my effects:
        For Each e As EffectBase In Effects
            e.Animate(context)
        Next

        'Add my effects to the context from this point:
        context.Effects.AddRange(Me.Effects)

        'Let my effects to any pre-processing:
        For Each e As EffectBase In context.Effects
            e.OnBeforeAnimate(context, Me)
        Next

        'Are we in the scope of a Keyframe (i.e. a ArtificeMovable)?
        If context.KeyFrame IsNot Nothing Then
            Dim doppelganger As ArtificeObject = Nothing

            If context.KeyFrame.Next IsNot Nothing Then
                'Next find out if this object exists in the next keyframe (i.e. it has a clone with the same id):
                doppelganger = context.KeyFrame.Next.GetChildById(Me.Id)
            End If

            For Each pi As PropertyInfo In Me.GetType().GetProperties()
                If pi.PropertyType.Name.StartsWith("Lerpable") Then
                    'Reset our delta value to our default value:
                    Dim v = pi.GetValue(Me)

                    'Are we allowed to lerp this property?
                    If v.Lerpable Then
                        'Reset delta to the original value:
                        v.Reset()

                        'If we have a clone, lerp this property between it's value and its clone's equivalent:
                        If doppelganger IsNot Nothing Then
                            v.Lerp(pi.GetValue(doppelganger).Value, context.LerpAmount)
                        End If
                    End If
                End If
            Next
        End If

        'Let descendent classes do their thing:
        OnAnimate(context)

        'Let my effects do any post-processing:
        For Each e As EffectBase In context.Effects
            e.OnAfterAnimate(context, Me)
        Next

        If Me.Is(Of Layer)() Then
            'Only animate the keyframe for this frame in this layer:
            Dim kf As KeyFrame = Me.As(Of Layer).GetKeyFrame(context.Frame)

            If kf IsNot Nothing Then kf.Animate(context)
        Else
            'Animate my children:
            For Each child As ArtificeObject In Children
                child.Animate(context)
            Next
        End If

        'Remove my effects from the effects list in the Render Context:
        If Effects.Count > 0 Then
            context.Effects.RemoveRange(effectsStartIndex, Effects.Count)
        End If

        'Let descendent classes do their thing:
        OnAfterAnimate(context)
    End Sub
#End Region

#Region "As"
    ''' <summary>
    ''' Returns this instance CType'd to the given type
    ''' </summary>
    ''' <typeparam name="T">The type (derived from ArtificeObject) to cast it to</typeparam>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function [As](Of T As ArtificeObject)() As T
        Return CType(Me, T)
    End Function
#End Region

#Region "BringForward"
    ''' <summary>
    ''' Moves this object one step up its Parent's list of children
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub BringForward()
        If Parent Is Nothing OrElse Parent.MyChildren.Last Is Me Then Return

        MoveInParent(ParentIndex + 1)
    End Sub
#End Region

#Region "BringToFront"
    ''' <summary>
    ''' Brings this object to the top of its Parent's list of children
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub BringToFront()
        If Parent Is Nothing Then Return

        MoveInParent(Parent.MyChildren.Count - 1)
    End Sub
#End Region

#Region "Clone"
    ''' <summary>
    ''' Makes a copy of this ArtificeObject, automatically duplicating any Lerpables.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim cl As ArtificeObject = Activator.CreateInstance(Me.GetType())

        'Copy my Id and Name:
        cl.MyId = Me.Id
        cl.Name = Me.Name

        'Clone all my lerpables:
        For Each pi As PropertyInfo In Me.GetType().GetProperties()
            If pi.PropertyType.Name.StartsWith("Lerpable") Then
                Dim source = pi.GetValue(Me)
                Dim dest = pi.GetValue(cl)

                'dest.Value = source.Value
                dest.Copy(source)
            End If
        Next

        'Clone my children:
        For Each child As ArtificeObject In Children
            cl.AddChild(child.Clone())
        Next

        'Clone my effects:
        For Each effect As EffectBase In Effects
            cl.AddEffect(effect.Clone())
        Next

        'Let descendent classes do any other special cloning they need to:
        OnClone(cl)

        'Return the clone:
        Return cl
    End Function
#End Region

#Region "ExportAudio"
    ''' <summary>
    ''' Exports audio from this ArtificeObject into the provided audio mixer
    ''' </summary>
    ''' <param name="context">The context to export with</param>    
    ''' <remarks>This only gets used by the Sound class presently</remarks>
    Public Sub ExportAudio(context As ExportAudioContext)
        'Let descendant classes figure out what they need to do:
        OnExportAudio(context)
    End Sub
#End Region

#Region "Flip"
    Public Overridable Sub Flip(flipX As Boolean, flipY As Boolean)
        'Do nothing (like child classes handle this)
    End Sub
#End Region

#Region "FromXElement"
    ''' <summary>
    ''' Returns a new ArtificeObject from the provided XElement
    ''' </summary>
    ''' <param name="el">The XElement containing the ArtificeObject definition</param>
    ''' <param name="uniqueIds">True to assign unique ids to the resulting objects, False to use the ids from the XElement.</param>
    ''' <returns>A new ArtificeObject</returns>
    ''' <remarks></remarks>
    Public Shared Function FromXElement(el As XElement, Optional uniqueIds As Boolean = False) As ArtificeObject
        'By making the element name the same as our local class name, 
        'we can easily create a new instance:
        Dim t As Type = Type.GetType("Artifice." & el.Name.LocalName)
        Dim vo As ArtificeObject = Activator.CreateInstance(t)

        If uniqueIds Then
            vo.MyId = NextId()
        Else
            vo.MyId = CInt(el.@Id)
        End If

        vo.MyName = el.@Name

        'Load Lerpables:
        For Each pi As PropertyInfo In CachedLerpables(t)

            'This probably isn't the most efficient way to do this,
            'but it's easier than the convoluted way to figuring out if something is
            'dervied from a generic type (see http://stackoverflow.com/questions/4425657/reflection-over-inherited-generic-types
            'for just how confusing this is)
            If el.Attribute(XName.Get(pi.Name)) IsNot Nothing Then
                If pi.PropertyType Is GetType(LerpableSingle) Then
                    Dim ls As LerpableSingle = CType(pi.GetValue(vo), LerpableSingle)

                    ls.Value = Single.Parse(el.Attribute(XName.Get(pi.Name)).Value)
                    ls.Lerpable = Boolean.Parse(el.Attribute(XName.Get(pi.Name & ".Lerpable")).Value)
                ElseIf pi.PropertyType Is GetType(LerpableInteger) Then
                    Dim li As LerpableInteger = CType(pi.GetValue(vo), LerpableInteger)

                    li.Value = Integer.Parse(el.Attribute(XName.Get(pi.Name)).Value)
                    li.Lerpable = Boolean.Parse(el.Attribute(XName.Get(pi.Name & ".Lerpable")).Value)
                ElseIf pi.PropertyType Is GetType(LerpableColor) Then
                    Dim lc As LerpableColor = CType(pi.GetValue(vo), LerpableColor)

                    lc.Value = ArtificeColor.Parse(el.Attribute(XName.Get(pi.Name)).Value)
                    lc.Lerpable = Boolean.Parse(el.Attribute(XName.Get(pi.Name & ".Lerpable")).Value)
                End If
            End If
        Next

        'Let descendent classes load anything else they care about:
        vo.OnLoad(el)

        'Load all my children:
        For Each child As XElement In el.Elements
            Dim ArtificeChild As ArtificeObject = FromXElement(child, uniqueIds)

            'We keep effects and the rest of the objects in two separate lists:
            If ArtificeChild.Is(Of EffectBase)() Then
                ArtificeChild.MyParent = vo
                vo.Effects.Add(ArtificeChild)
            Else
                vo.AddChild(ArtificeChild)
            End If
        Next

        vo.OnAfterLoad()

        Return vo
    End Function
#End Region

#Region "GetAncestor"
    ''' <summary>
    ''' Finds the first ancestor of a specific type start from this object
    ''' </summary>
    ''' <typeparam name="T">The type you are searching for</typeparam>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAncestor(Of T As ArtificeObject)() As T
        If Parent Is Nothing Then Return Nothing

        If Parent.Is(Of T)() Then Return Parent

        Return Parent.GetAncestor(Of T)()
    End Function
#End Region

#Region "GetBounds"
    ''' <summary>
    ''' Returns the bounds, in World coordinates, of this object
    ''' </summary>
    ''' <returns>A RectangleF containing the bounds of this object</returns>
    ''' <remarks></remarks>
    Public Function GetBounds() As RectangleF
        Dim r As RectangleF = OnGetBounds()

        Return r
    End Function
#End Region

#Region "GetChildById"
    ''' <summary>
    ''' Returns the child ArtificeObject with the given id, or Nothing if no object is found
    ''' </summary>
    ''' <param name="id">The id to search for</param>
    ''' <returns></returns>
    ''' <remarks>This method is used by the Lerping algorithms to find an object's copy in another KeyFrame</remarks>
    Public Function GetChildById(id As Integer) As ArtificeObject
        Dim child As ArtificeObject = Nothing

        For Each e As EffectBase In Effects
            If e.Id = id Then Return e
        Next

        For Each c As ArtificeObject In Children
            If c.Id = id Then Return c

            child = c.GetChildById(id)

            If child IsNot Nothing Then Exit For
        Next

        Return child
    End Function
#End Region

#Region "GetClosest"
    ''' <summary>
    ''' Finds the closest object of the given type, searching from this object up the Artifice DOM
    ''' </summary>
    ''' <typeparam name="T">The type you are searching for</typeparam>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetClosest(Of T As ArtificeObject)() As T
        If Me.Is(Of T)() Then Return Me

        Return GetAncestor(Of T)()
    End Function
#End Region

#Region "HitTest"
    ''' <summary>
    ''' Returns the ArtificeObject found at the given coordinates
    ''' </summary>
    ''' <param name="x">The x coordinate to search</param>
    ''' <param name="y">The y coordinate to search</param>
    ''' <param name="rc">The render context to transform the points against</param>
    ''' <param name="transformedLocation">The original x, y coordinates transformed to the World coordinates of the returned object (if any)</param>
    ''' <returns>The ArtificeObject at the given coordinates, or Nothing if none are found</returns>
    ''' <remarks></remarks>
    Public Function HitTest(x As Single, y As Single, rc As RenderContext, ByRef transformedLocation As Point) As ArtificeMoveable
        Dim result As ArtificeMoveable = Nothing

        If (Not Designer.SelectedContainer Is Me AndAlso Not Designer.SelectedContainer.IsAncestorOf(Me) AndAlso Not IsAncestorOf(Designer.SelectedContainer)) OrElse (Designer.SelectedContainer.IsAncestorOf(Me) AndAlso Not Designer.SelectedContainer Is Parent) Then
            Return Nothing
        End If


        'Push the current graphic state:
        rc.PushGraphicsState()

        'Let descendent classes provide any transforms required:
        OnBeforeRender(rc)

        'Let descendent classes actually perform the hit test:
        result = OnHitTest(x, y, rc, transformedLocation)

        'Any cleanup required by descendent classes:
        OnAfterRender(rc)

        'Return our graphic state to the previous one:
        rc.PopGraphicsState()

        'Return the result
        Return result
    End Function
#End Region

#Region "Is"
    ''' <summary>
    ''' Returns True if this instance is of type T (or is class derived from T).
    ''' </summary>
    ''' <typeparam name="T">The type (derived from ArtificeObject) to test for</typeparam>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function [Is](Of T As ArtificeObject)() As Boolean
        Return Me.GetType() Is GetType(T) OrElse Me.GetType().IsSubclassOf(GetType(T))
    End Function
#End Region

#Region "IsAncestorOf"
    ''' <summary>
    ''' Returns True if this object is an ancestor of the given object
    ''' </summary>
    ''' <param name="vo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsAncestorOf(vo As ArtificeObject) As Boolean
        Dim curr As ArtificeObject = vo.Parent

        While curr IsNot Nothing
            If curr Is Me Then Return True

            curr = curr.Parent
        End While

        Return False
    End Function
#End Region

#Region "MoveInParent"
    Public Sub MoveInParent(newIndex As Integer)
        If Parent Is Nothing Then Return

        Dim oldIndex As Integer = ParentIndex

        Parent.MyChildren.Remove(Me)
        Parent.MyChildren.Insert(newIndex, Me)

        Parent.OnPropertyChanged("Children")
    End Sub
#End Region

#Region "OnAfterAnimate"
    ''' <summary>
    ''' Called after this ArtificeObject has been animated
    ''' </summary>
    ''' <param name="ac">The animation context used to animate this object</param>
    ''' <remarks>Handled by derived classes</remarks>
    Protected Overridable Sub OnAfterAnimate(ac As AnimationContext)

    End Sub
#End Region

#Region "OnAfterLoad"
    ''' <summary>
    ''' Called after this ArtificeObject has been loaded from XML
    ''' </summary>
    ''' <remarks>Handled by derived classes</remarks>
    Protected Overridable Sub OnAfterLoad()

    End Sub
#End Region

#Region "OnAfterRender"
    ''' <summary>
    ''' Called after this ArtificeObject has been rendered
    ''' </summary>
    ''' <param name="rc">The render context used to render this ArtificeObject</param>
    ''' <remarks>Handled by derived classes</remarks>
    Protected Overridable Sub OnAfterRender(rc As RenderContext)

    End Sub
#End Region

#Region "OnAnimate"
    ''' <summary>
    ''' Called when this ArtificeObject needs to be animated
    ''' </summary>
    ''' <param name="ac">The animation context used to animate this object</param>
    ''' <remarks>Handled by derived classes</remarks>
    Protected Overridable Sub OnAnimate(ac As AnimationContext)

    End Sub
#End Region

#Region "OnBeforeRender"
    ''' <summary>
    ''' Called before this ArtificeObject has been rendered
    ''' </summary>
    ''' <param name="rc">The render context used to render this ArtificeObject</param>
    ''' <remarks>Handled by derived classes</remarks>
    Protected Overridable Sub OnBeforeRender(rc As RenderContext)

    End Sub
#End Region

#Region "OnChildAdded"
    ''' <summary>
    ''' Called when a child has been added to this ArtificeObject
    ''' </summary>
    ''' <remarks>Handled by derived classes</remarks>
    Protected Overridable Sub OnChildAdded()

    End Sub
#End Region

#Region "OnChildRemoved"
    ''' <summary>
    ''' Called when a child has been removed to this ArtificeObject
    ''' </summary>
    ''' <remarks>Handled by derived classes</remarks>
    Protected Overridable Sub OnChildRemoved()

    End Sub
#End Region

#Region "OnClone"
    ''' <summary>
    ''' Called when a clone of this object has been created, to allow additional fields to be copied by derived classes
    ''' </summary>
    ''' <param name="clone">The clone of this object</param>
    ''' <remarks>Handled by derived classes</remarks>
    Protected Overridable Sub OnClone(clone As ArtificeObject)

    End Sub
#End Region

#Region "OnExportAudio"
    ''' <summary>
    ''' OnExportAudio
    ''' </summary>
    ''' <param name="context">The context to export with</param>    
    ''' <remarks>Handled by derived classes</remarks>
    Protected Overridable Sub OnExportAudio(context As ExportAudioContext)
        For Each child As ArtificeObject In Children
            child.ExportAudio(context)
        Next
    End Sub
#End Region

#Region "OnGetBounds"
    ''' <summary>
    ''' Called internally to have descendant classes calculate their bounds.
    ''' </summary>
    ''' <returns>A RectangleF representing the bounds of this object, in World units</returns>
    ''' <remarks>By default, returns the union of all children bounds</remarks>
    Protected Overridable Function OnGetBounds() As RectangleF
        Dim r As New RectangleF

        For Each child As ArtificeObject In Children
            r = RectangleF.Union(r, child.GetBounds())
        Next

        Return r
    End Function
#End Region

#Region "OnHitTest"
    ''' <summary>
    ''' Called internally so descendant classes can perform a hit test
    ''' </summary>
    ''' <param name="x">The x coordinate, in World units</param>
    ''' <param name="y">The y coordinate, in World units</param>
    ''' <param name="rc">The render context used to transform units</param>
    ''' <param name="transformedLocation">The original coordinates, transformed into World units</param>
    ''' <returns>The ArtificeObject at the given location, or Nothing if none is found</returns>
    ''' <remarks>By default automatically calls HitTest for each child</remarks>
    Protected Overridable Function OnHitTest(x As Single, y As Single, rc As RenderContext, ByRef transformedLocation As Point) As ArtificeMoveable
        Dim match As ArtificeMoveable = Nothing

        For ci As Integer = Children.Count - 1 To 0 Step -1
            Dim child As ArtificeObject = Children(ci)

            match = child.HitTest(x, y, rc, transformedLocation)

            If match IsNot Nothing Then Exit For
        Next

        Return match
    End Function
#End Region

#Region "OnLoad"
    ''' <summary>
    ''' Called internally to allow derived classes to load special fields from Xml
    ''' </summary>
    ''' <param name="el">The XElement to load this ArtificeObject's properties from</param>
    ''' <remarks>Hanlded by derived classes</remarks>
    Protected Overridable Sub OnLoad(el As XElement)

    End Sub
#End Region

#Region "OnRender"
    ''' <summary>
    ''' Called when a ArtificeObject needs to render itself
    ''' </summary>
    ''' <param name="rc">The render context to use for rendering</param>
    ''' <remarks>By default calls Render on this object's children.</remarks>
    Protected Overridable Sub OnRender(rc As RenderContext)
        For Each child As ArtificeObject In Children
            child.Render(rc)
        Next
    End Sub
#End Region

#Region "OnSave"
    ''' <summary>
    ''' Called when descendant classes need to write special data to Xml
    ''' </summary>
    ''' <param name="el">The XElement to write properties to</param>
    ''' <remarks>Handled by derived classes</remarks>
    Protected Overridable Sub OnSave(el As XElement)

    End Sub
#End Region

#Region "OnSaveAsSVG"
    ''' <summary>
    ''' Called when descendant classes are needed to export to SVG
    ''' </summary>
    ''' <returns>An array of XElement objects corresponding to SVG nodes</returns>
    ''' <remarks>By default this calls ToSVG for each child</remarks>
    Protected Overridable Function OnSaveAsSVG() As XElement()
        Dim results As New List(Of XElement)

        For Each child As ArtificeObject In Children
            results.AddRange(child.ToSVG())
        Next

        Return results.ToArray()
    End Function
#End Region

#Region "Remove"
    ''' <summary>
    ''' Removes this ArtificeObject from its parent
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub Remove()
        If MyParent Is Nothing Then Return

        MyParent.RemoveChild(Me)
        MyParent = Nothing

        If Designer.SelectedObjects.Contains(Me) Then
            Designer.RemoveSelection(Me)
        End If
    End Sub
#End Region

#Region "RemoveChild"
    ''' <summary>
    ''' Removes the given child from this ArtificeObject's list of children
    ''' </summary>
    ''' <param name="child">The ArtificeObject to remove</param>
    ''' <remarks></remarks>
    Public Sub RemoveChild(child As ArtificeObject)
        If Not MyChildren.Contains(child) Then Return

        MyChildren.Remove(child)
        child.MyParent = Nothing

        OnChildRemoved()

        OnPropertyChanged("Children")
    End Sub
#End Region

#Region "Render"
    ''' <summary>
    ''' Renders this ArtificeObject
    ''' </summary>
    ''' <param name="rc">The render context to use for rendering</param>
    ''' <remarks></remarks>
    Public Sub Render(rc As RenderContext)
        'Here we push our graphics state so that any transformations can be done at this level in the tree and not effect other branches
        rc.PushGraphicsState()

        Dim effectsStartIndex As Integer = rc.Effects.Count()

        'Add my effects to the context from this point:
        rc.Effects.AddRange(Me.Effects)

        'Let my derived classes do any pre-rendering stuff:
        OnBeforeRender(rc)

        'Let my effects do any pre-rendering stuff:
        For Each e As EffectBase In rc.Effects
            e.OnBeforeRender(rc, Me)
        Next

        'Get my derived classes to actually render themselves:
        OnRender(rc)

        'Let my effects do any post-rendering stuff:
        For Each e As EffectBase In rc.Effects
            e.OnAfterRender(rc, Me)
        Next

        'Let my derived classes do any post-rendering stuff
        OnAfterRender(rc)

        'Remove my effects from the context:
        If Effects.Count > 0 Then
            rc.Effects.RemoveRange(effectsStartIndex, Effects.Count - 1)
        End If

        'Pop that graphics state and return it to the previous!
        rc.PopGraphicsState()
    End Sub
#End Region

#Region "ResetIdCounter"
    ''' <summary>
    ''' Resets the internal id counter to the given value
    ''' </summary>
    ''' <param name="newValue">The new value to reset the id counter to</param>
    ''' <remarks>This can only be called by ArtificeObjects of type Project</remarks>
    Protected Sub ResetIdCounter(newValue As Integer)
        If Not Me.Is(Of Project)() Then Throw New Exception("This can only be called by objects of type Project")

        SyncLock Me
            MyNextId = newValue
        End SyncLock
    End Sub
#End Region

#Region "SendBackward"
    ''' <summary>
    ''' Moves this object one step lower in its Parent's list of children
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SendBackward()
        If Parent Is Nothing OrElse Parent.MyChildren.First Is Me Then Return

        MoveInParent(ParentIndex - 1)
    End Sub
#End Region

#Region "SendToBack"
    ''' <summary>
    ''' Sends this ArtificeObject to the bottom of its parents list
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SendToBack()
        If Parent Is Nothing Then Return

        MoveInParent(0)
    End Sub
#End Region

#Region "ToSVG"
    ''' <summary>
    ''' Returns this ArtificeObject in SVG format
    ''' </summary>
    ''' <returns>An array of XElements containing valid SVG nodes (or an empty list)</returns>
    ''' <remarks></remarks>
    Public Function ToSVG() As XElement()
        Dim results As New List(Of XElement)
        Dim children() As XElement = OnSaveAsSVG()

        If children IsNot Nothing Then results.AddRange(children)

        Return results.ToArray()
    End Function
#End Region

#Region "ToXElement"
    ''' <summary>
    ''' Returns this ArtificeObject as an XElement for writing to a .Artifice file
    ''' </summary>
    ''' <returns>An XElement representation of this ArtificeObject</returns>
    ''' <remarks></remarks>
    Public Function ToXElement() As XElement
        Dim el As New XElement(Me.GetType().Name)

        el.@Id = MyId
        el.@Name = MyName

        'Write out lerpables:
        For Each pi As PropertyInfo In CachedLerpables(Me.GetType())

            'This probably isn't the most efficient way to do this,
            'but it's easier than the convoluted way to figuring out if something is
            'dervied from a generic type (see http://stackoverflow.com/questions/4425657/reflection-over-inherited-generic-types
            'for just how confusing this is)
            If pi.PropertyType Is GetType(LerpableSingle) Then
                Dim ls As LerpableSingle = CType(pi.GetValue(Me), LerpableSingle)

                el.Add(New XAttribute(XName.Get(pi.Name), ls.Value.ToString()))
                el.Add(New XAttribute(XName.Get(pi.Name & ".Lerpable"), ls.Lerpable.ToString()))
            ElseIf pi.PropertyType Is GetType(LerpableInteger) Then
                Dim li As LerpableInteger = CType(pi.GetValue(Me), LerpableInteger)

                el.Add(New XAttribute(XName.Get(pi.Name), li.Value.ToString()))
                el.Add(New XAttribute(XName.Get(pi.Name & ".Lerpable"), li.Lerpable.ToString()))
            ElseIf pi.PropertyType Is GetType(LerpableColor) Then
                Dim lc As LerpableColor = CType(pi.GetValue(Me), LerpableColor)

                el.Add(New XAttribute(XName.Get(pi.Name), lc.Value.Color.ToArgb()))
                el.Add(New XAttribute(XName.Get(pi.Name & ".Lerpable"), lc.Lerpable.ToString()))
            End If
        Next

        'Let descendent classes write out anything else they care about:
        OnSave(el)

        'Write out any effects:
        For Each e As EffectBase In Effects
            el.Add(e.ToXElement())
        Next

        'Write out all the children:
        For Each child As ArtificeObject In Children
            el.Add(child.ToXElement())
        Next

        Return el
    End Function
#End Region

#Region "ToString"
    ''' <summary>
    ''' Converts this object to a string
    ''' </summary>
    ''' <returns>A string describing this object</returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Dim s As New StringBuilder()

        If Name <> "" Then
            s.Append(Name)
        Else
            s.Append(Me.GetType().Name)
        End If

        s.Append(" (")

        s.Append(Id)

        s.Append(")")

        Return s.ToString()
    End Function
#End Region

#Region "Translate"
    ''' <summary>
    ''' Translates the given point from Device space to World space
    ''' </summary>
    ''' <param name="pt">The point to convert</param>
    ''' <param name="rc">The render context used to transform the points</param>
    ''' <returns>The transformed point, in World coordinates</returns>
    ''' <remarks></remarks>
    Public Function Translate(pt As Point, rc As RenderContext) As Point
        Dim ancestors As New List(Of ArtificeMoveable)
        Dim curr As ArtificeObject = Parent

        rc.PushGraphicsState()

        'Build our list backwards so that the oldest ancestor is first:
        While curr IsNot Nothing AndAlso curr.Is(Of ArtificeMoveable)()
            ancestors.Insert(0, curr)
            curr = curr.Parent
        End While

        'Make our ancestors transform to their location first:
        For Each a As ArtificeMoveable In ancestors
            a.OnBeforeRender(rc)
        Next

        OnBeforeRender(rc)

        Dim points() As Point = New Point() {pt}

        rc.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, points)

        OnAfterRender(rc)

        'Go backwards through our list and undo our ancestors graphics state changes:
        ancestors.Reverse()
        For Each p As ArtificeMoveable In ancestors
            p.OnAfterRender(rc)
        Next

        rc.PopGraphicsState()

        Return points(0)

        ''Since we're hierarchical, let's recursively call our parents first in order that all their
        ''transforms will happen in the correct order:
        'If Parent IsNot Nothing AndAlso Parent.GetType().IsSubclassOf(GetType(ArtificeMoveable)) Then
        '    pt = CType(Parent, ArtificeMoveable).Translate(pt, rc)
        'End If

        'OnBeforeRender(rc)

        ''Now transform the points to my space:
        'Dim points(1) As Point
        'points(0) = New Point(pt)

        'rc.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, points)

        ''Return the transformed points:
        'Return points(0)
    End Function
#End Region

#Region "ArtificeObject_PropertyChanged"
    ''' <summary>
    ''' Internally bubbles all events up through the hierarchy
    ''' </summary>
    ''' <param name="sender">The object that fired the original event</param>
    ''' <param name="e">The details of the PropertyChangedEvent</param>
    ''' <remarks></remarks>
    Private Sub ArtificeObject_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles Me.PropertyChanged
        If Parent IsNot Nothing Then Parent.OnPropertyChanged(sender, e)
    End Sub
#End Region

End Class
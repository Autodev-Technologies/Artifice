''' <summary>
''' The base class for all custom-drawn controls in the designer
''' </summary>
''' <remarks>Created by Autodev</remarks>
Public Class DesignerControlBase

    'For simplicity sake, we expose the Designer singleton object
    Protected ReadOnly Property Designer As Designer
        Get
            Return Global.Artifice.Designer.Singleton
        End Get
    End Property

End Class

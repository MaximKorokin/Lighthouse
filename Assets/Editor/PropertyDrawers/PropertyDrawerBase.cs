using UnityEditor;
using UnityEngine.UIElements;

public abstract class PropertyDrawerBase : PropertyDrawer
{
    protected VisualElement RootContainer { get; private set; }
    protected SerializedProperty Property { get; private set; }

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        Property = property;
        RootContainer = new VisualElement();
        RedrawRootContainer();
        return RootContainer;
    }

    protected abstract void RedrawRootContainer();
}

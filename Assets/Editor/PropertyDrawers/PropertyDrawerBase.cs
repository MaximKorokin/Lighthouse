using UnityEditor;
using UnityEngine.UIElements;

public abstract class PropertyDrawerBase : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var rootContainer = new VisualElement();
        RedrawRootContainer(rootContainer, property);
        return rootContainer;
    }

    protected abstract void RedrawRootContainer(VisualElement rootContainer, SerializedProperty property);
}

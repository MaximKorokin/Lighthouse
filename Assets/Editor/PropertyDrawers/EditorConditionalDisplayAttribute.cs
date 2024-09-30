using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(ConditionalDisplayAttribute))]
public class EditorConditionalDisplayAttribute : PropertyDrawerBase
{
    private bool _attached = false;
    private string _propertyName;

    protected override void RedrawRootContainer()
    {
        RootContainer.Clear();
        _propertyName = Property.name;

        var conditionalDispalyAttribute = attribute as ConditionalDisplayAttribute;

        var property = Property.serializedObject.FindProperty(conditionalDispalyAttribute.FieldPath);

        // Display property if condition is met
        if (property != null && AreEqual(property, conditionalDispalyAttribute.EqualityObject))
        {
            var propertyField = new PropertyField();
            propertyField.BindProperty(Property);
            RootContainer.Add(propertyField);
        }

        // Really weird recursive nesting things happen when registering callbacks more than once
        // As the object may be reused for more than one VisualElement, it will lead to something bad...
        if (!_attached)
        {
            RootContainer.RegisterCallback<AttachToPanelEvent>(OnAttach);
        }
    }

    private void OnAttach(AttachToPanelEvent e)
    {
        _attached = true;
        RootContainer.UnregisterCallback<AttachToPanelEvent>(OnAttach);
        var conditionalDispalyAttribute = attribute as ConditionalDisplayAttribute;

        var element = RootContainer.parent.parent.Children().FirstOrDefault(x => x.name.Contains(conditionalDispalyAttribute.FieldPath)) as PropertyField;
        element?.RegisterValueChangeCallback(e =>
        {
            // Property may be Disposed
            if (Property.serializedObject != e.changedProperty.serializedObject)
            {
                Property = e.changedProperty.serializedObject.FindProperty(_propertyName);
            }
            RedrawRootContainer();
        });
    }

    private bool AreEqual(SerializedProperty property, object equalityObject)
    {
        return property.propertyType switch
        {
            SerializedPropertyType.Integer => property.intValue == (int)equalityObject,
            SerializedPropertyType.Boolean => property.boolValue == (bool)equalityObject,
            SerializedPropertyType.Float => property.floatValue == (float)equalityObject,
            SerializedPropertyType.String => property.stringValue == (string)equalityObject,
            SerializedPropertyType.LayerMask => property.intValue == (int)equalityObject,
            SerializedPropertyType.Enum => property.intValue == (int)equalityObject,
            _ => false
        };
    }
}

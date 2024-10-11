using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(ConditionalDisplayAttribute))]
public class EditorConditionalDisplayAttribute : PropertyDrawerBase
{
    private bool _attached = false;
    private string _propertyName;

    protected override void RedrawRootContainer(VisualElement rootContainer, SerializedProperty property)
    {
        rootContainer.Clear();
        _propertyName = property.name;

        var conditionalDispalyAttribute = attribute as ConditionalDisplayAttribute;

        var conditionProperty = property.serializedObject.FindProperty(conditionalDispalyAttribute.FieldPath);

        // Display property if condition is met
        if (conditionProperty != null && AreEqual(conditionProperty, conditionalDispalyAttribute.EqualityObject))
        {
            var propertyField = new PropertyField();
            propertyField.BindProperty(property);
            rootContainer.Add(propertyField);
        }

        // Really weird recursive nesting things happen when registering callbacks more than once
        // As the object may be reused for more than one VisualElement, it will lead to something bad...
        if (!_attached)
        {
            rootContainer.RegisterCallback<AttachToPanelEvent>(OnAttach);
        }

        void OnAttach(AttachToPanelEvent e)
        {
            _attached = true;
            rootContainer.UnregisterCallback<AttachToPanelEvent>(OnAttach);
            var conditionalDispalyAttribute = attribute as ConditionalDisplayAttribute;

            var element = rootContainer.parent.parent.Children().FirstOrDefault(x => x.name.Contains(conditionalDispalyAttribute.FieldPath)) as PropertyField;
            element?.RegisterValueChangeCallback(e =>
            {
                // Property may be Disposed
                if (property.serializedObject != e.changedProperty.serializedObject)
                {
                    property = e.changedProperty.serializedObject.FindProperty(_propertyName);
                }
                RedrawRootContainer(rootContainer, property);
            });
        }
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

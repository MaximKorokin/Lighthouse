using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Stats))]
public class EditorStats : PropertyDrawerBase
{
    private const string StatsFieldName = "_stats";

    protected override void RedrawRootContainer(VisualElement rootContainer, SerializedProperty property)
    {
        rootContainer.Clear();
        var foldout = new Foldout { text = "Stats" };
        rootContainer.Add(foldout);

        var statsProperty = property.FindPropertyRelative(StatsFieldName);
        var statProperties = Enumerable.Range(0, statsProperty.arraySize).Select(i => statsProperty.GetArrayElementAtIndex(i)).ToArray();
        statProperties.ForEach((x, i) => foldout.Add(CreateStatPropertyContainer(rootContainer, property, statsProperty, x, i)));

        var unusedStats = Enum.GetNames(typeof(StatName)).Except(statProperties.Select(x => EditorStat.GetStatName(x))).ToList();

        if (foldout.value && unusedStats.Count > 0)
        {
            var statsCreationElement = CreateStatsCreationElement(rootContainer, property, statsProperty, unusedStats);
            foldout.RegisterValueChangedCallback(x => statsCreationElement.style.display = x.newValue ? DisplayStyle.Flex: DisplayStyle.None);
            rootContainer.Add(statsCreationElement);
        }
    }

    private VisualElement CreateStatPropertyContainer(VisualElement rootContainer, SerializedProperty property, SerializedProperty stats, SerializedProperty stat, int index)
    {
        var container = new VisualElement();
        container.style.flexDirection = FlexDirection.Row;

        var statDeleteButton = new Button(() =>
        {
            stats.DeleteArrayElementAtIndex(index);
            stats.serializedObject.ApplyModifiedProperties();
            RedrawRootContainer(rootContainer, property);
        });
        statDeleteButton.text = "-";
        statDeleteButton.style.width = 25;

        container.Add(statDeleteButton);
        var propertField = new PropertyField();
        propertField.BindProperty(stat);
        propertField.style.width = Length.Percent(100);
        container.Add(propertField);
        return container;
    }

    private VisualElement CreateStatsCreationElement(VisualElement rootContainer, SerializedProperty property, SerializedProperty stats, List<string> statsNames)
    {
        var statCreateElement = new VisualElement();
        statCreateElement.style.flexDirection = FlexDirection.Row;
        statCreateElement.style.marginLeft = new Length() { value = 15 };

        var statsPopup = new PopupField<string>(statsNames, 0);
        var statCreateButton = new Button(() =>
        {
            stats.InsertArrayElementAtIndex(stats.arraySize);
            var stat = stats.GetArrayElementAtIndex(stats.arraySize - 1);
            EditorStat.SetStatName(stat, statsPopup.value);
            stats.serializedObject.ApplyModifiedProperties();
            RedrawRootContainer(rootContainer, property);
        });
        statCreateButton.text = "+";
        statCreateButton.style.width = 25;

        statCreateElement.Add(statCreateButton);
        statCreateElement.Add(statsPopup);

        return statCreateElement;
    }
}

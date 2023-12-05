using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Stats))]
public class EditorStats : PropertyDrawer
{
    private const string StatsFieldName = "_stats";
    private VisualElement _rootContainer;
    private SerializedProperty _statsProperty;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        _statsProperty = property;
        _rootContainer = new VisualElement();
        return CreateStatsElement();
    }

    private VisualElement CreateStatsElement()
    {
        _rootContainer.Clear();
        var foldout = new Foldout();
        foldout.text = "Stats";
        _rootContainer.Add(foldout);

        var statsProperty = _statsProperty.FindPropertyRelative(StatsFieldName);
        var statProperties = Enumerable.Range(0, statsProperty.arraySize).Select(i => statsProperty.GetArrayElementAtIndex(i)).ToArray();
        statProperties.ForEach((x, i) => foldout.Add(CreateStatPropertyContainer(statsProperty, x, i)));

        var unusedStats = Enum.GetNames(typeof(StatName)).Except(statProperties.Select(x => EditorStat.GetStatName(x))).ToList();

        if (foldout.value && unusedStats.Count > 0)
        {
            var statsCreationElement = CreateStatsCreationElement(statsProperty, unusedStats);
            foldout.RegisterValueChangedCallback(x => statsCreationElement.style.display = x.newValue ? DisplayStyle.Flex: DisplayStyle.None);
            _rootContainer.Add(statsCreationElement);
        }
        return _rootContainer;
    }

    private VisualElement CreateStatPropertyContainer(SerializedProperty stats, SerializedProperty stat, int index)
    {
        var container = new VisualElement();
        container.style.flexDirection = FlexDirection.Row;

        var statDeleteButton = new Button(() =>
        {
            stats.DeleteArrayElementAtIndex(index);
            stats.serializedObject.ApplyModifiedProperties();
            CreateStatsElement();
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

    private VisualElement CreateStatsCreationElement(SerializedProperty stats, List<string> statsNames)
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
            CreateStatsElement();
        });
        statCreateButton.text = "+";
        statCreateButton.style.width = 25;

        statCreateElement.Add(statCreateButton);
        statCreateElement.Add(statsPopup);

        return statCreateElement;
    }
}

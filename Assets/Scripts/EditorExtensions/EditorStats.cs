using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Stats))]
public class EditorStats : PropertyDrawer
{
    private const string StatsFieldName = "_stats";

    private int _selectedPopupStat;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty statsPropertyProvider() => property.FindPropertyRelative(StatsFieldName);

        EditorGUI.BeginProperty(position, label, property);

        var foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        if (property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true))
        {
            var allStats = Enum.GetNames(typeof(StatName));
            var possibleStats = allStats.ToList();

            var statsProperty = statsPropertyProvider();
            EditorGUI.indentLevel++;
            var statRect = new Rect(
                position.x,
                position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
                0,
                EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
            for (int i = 0; i < statsProperty.arraySize; i++)
            {
                var statProperty = statsProperty.GetArrayElementAtIndex(i);
                possibleStats.Remove(EditorStat.GetStatName(statProperty));
                EditorGUI.PropertyField(statRect, statProperty, GUIContent.none);
                if (GUI.Button(new Rect(statRect.x + 250, statRect.y, 20, statRect.height), "-"))
                {
                    statsProperty.DeleteArrayElementAtIndex(i);
                    i = Math.Max(0, i - 1);
                }
                else
                {
                    statRect.y += EditorGUI.GetPropertyHeight(statProperty) + EditorGUIUtility.standardVerticalSpacing;
                }
            }
            
            if (possibleStats.Count > 0)
            {
                var buttonRect = new Rect(position.x + 15, statRect.y + EditorGUIUtility.standardVerticalSpacing, 20, EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
                var popupRect = new Rect(buttonRect.position + Vector2.right * 25, new Vector2(150, EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing));
                _selectedPopupStat = EditorGUI.Popup(popupRect, _selectedPopupStat, possibleStats.ToArray());
                if (GUI.Button(buttonRect, "+"))
                {
                    statsProperty.InsertArrayElementAtIndex(statsProperty.arraySize);
                    EditorStat.SetStatName(statsProperty.GetArrayElementAtIndex(statsProperty.arraySize - 1), possibleStats[_selectedPopupStat]);
                    _selectedPopupStat = 0;
                }
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty statsPropertyProvider() => property.FindPropertyRelative(StatsFieldName);

        var totalHeight = base.GetPropertyHeight(property, label);
        if (property.isExpanded)
        {
            totalHeight += IterateChildren(statsPropertyProvider)
                .Sum(x => EditorGUI.GetPropertyHeight(x) + EditorGUIUtility.standardVerticalSpacing);
            totalHeight += EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
        }

        return totalHeight;
    }

    private IEnumerable<SerializedProperty> IterateChildren(Func<SerializedProperty> propertyProvider)
    {
        var property = propertyProvider();
        property.NextVisible(true);
        var depth = property.depth;
        while (property.NextVisible(false) && property.depth == depth)
        {
            Debug.Log(property.displayName);
            yield return property;
        }
    }
}

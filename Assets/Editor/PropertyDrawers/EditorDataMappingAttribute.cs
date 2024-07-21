using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(DataMappingAttribute))]
public class EditorDataMappingAttribute : PropertyDrawerBase
{
    protected override void RedrawRootContainer()
    {
        var dataMappingAttribute = attribute as DataMappingAttribute;
        
        // Get value of static Items property
        var dbItems = dataMappingAttribute.DBType.GetProperty(nameof(DataBase<IDataBaseEntry>.Items), BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).GetValue(null);

        // Define desplay name selector
        Func<IDataBaseEntry, string> displayNameSelector = prev => prev != null ? prev.ToString() : "Select value";
        // Create a popup and populate with Items casted to IDataBaseEntry
        var popup = new PopupField<IDataBaseEntry>(((IEnumerable)dbItems).Cast<IDataBaseEntry>().ToList(), 0, displayNameSelector, displayNameSelector);
        RootContainer.Add(popup);

        // Find static FindById method
        var dbFindByIdMethod = dataMappingAttribute.DBType.GetMethod(nameof(DataBase<IDataBaseEntry>.FindById), BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        // Try to find db entry using serialized property
        popup.value = (IDataBaseEntry)dbFindByIdMethod.Invoke(null, new object[] { Property.stringValue });

        popup.RegisterValueChangedCallback(x =>
        {
            // Serialize id of selected db entry
            Property.stringValue = x.newValue.Id;
            Property.serializedObject.ApplyModifiedProperties();
        });
    }
}

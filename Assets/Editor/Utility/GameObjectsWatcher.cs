using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class GameObjectsWatcher
{
    private static readonly Dictionary<int, GameObject> _previousGameObjects = new();

    static GameObjectsWatcher()
    {
        EditorApplication.update += OnEditorUpdate;
        CacheGameObjects();
    }

    private static void OnEditorUpdate()
    {
        foreach (var obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
        {
            if (!_previousGameObjects.ContainsKey(obj.GetInstanceID()))
            {
                InitializeMarkedFields(obj);
            }
        }

        CacheGameObjects();
    }

    private static void CacheGameObjects()
    {
        _previousGameObjects.Clear();
        foreach (var obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
        {
            _previousGameObjects[obj.GetInstanceID()] = obj;
        }
    }

    private static void InitializeMarkedFields(GameObject obj)
    {
        foreach (var component in obj.GetComponents<MonoBehaviour>())
        {
            var fields = component.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            foreach (var field in fields)
            {
                if (Attribute.IsDefined(field, typeof(InitialEditorValueAttribute)))
                {
                    var initialValue = field.GetCustomAttribute<InitialEditorValueAttribute>().Value;
                    field.SetValue(component, initialValue);
                    EditorUtility.SetDirty(component);
                }
            }
        }
    }
}

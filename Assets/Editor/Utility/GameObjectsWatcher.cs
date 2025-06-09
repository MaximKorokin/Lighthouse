using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class GameObjectsWatcher
{
    private static readonly HashSet<int> _previousGameObjects = new();

    static GameObjectsWatcher()
    {
        EditorApplication.update += OnEditorUpdate;

        EditorSceneManager.activeSceneChangedInEditMode += (s1, s2) =>
        {
            CacheGameObjects();
        };

        CacheGameObjects();
    }

    private static void OnEditorUpdate()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode) return;

        foreach (var obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
        {
            if (!_previousGameObjects.Contains(obj.GetInstanceID()))
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
            _previousGameObjects.Add(obj.GetInstanceID());
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

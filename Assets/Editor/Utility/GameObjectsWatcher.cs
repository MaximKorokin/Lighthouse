using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class GameObjectsWatcher
{
    private static readonly HashSet<int> _previousGameObjects = new();

    private static bool _isSceneChanging = false;

    static GameObjectsWatcher()
    {
        EditorApplication.update += OnEditorUpdate;

        EditorSceneManager.sceneOpening += (path, mode) => _isSceneChanging = true;
        EditorSceneManager.sceneOpened += (scene, mode) =>
        {
            _isSceneChanging = false;
            CacheGameObjects();
        };

        CacheGameObjects();
    }

    private static void OnEditorUpdate()
    {
        if (_isSceneChanging) return;

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

using System.IO;
using UnityEngine;

public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var resources = Resources.LoadAll<T>("");
                if (resources.Length == 0)
                {
                    Logger.Error($"No resources of type {typeof(T)} found for {nameof(ScriptableObjectSingleton<T>)}");
                    throw new FileNotFoundException();
                }
                else if (resources.Length > 1)
                {
                    Logger.Warn($"More than one resource of type {typeof(T)} found for {nameof(ScriptableObjectSingleton<T>)}");
                }
                _instance = resources[0];
            }
            return _instance;
        }
    }
}

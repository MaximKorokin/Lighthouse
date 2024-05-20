using System;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigsManager
{
    private static readonly Dictionary<Config, HashSet<Action<string>>> _listeners = new();

    public static void SetValue(Config config, string value)
    {
        if (PlayerPrefs.GetString(config.ToString()) != value.ToString() && _listeners.ContainsKey(config))
        {
            _listeners[config].ForEach(x => x(value));
        }
        PlayerPrefs.SetString(config.ToString(), value.ToString());
    }

    public static string GetValue(Config config)
    {
        return PlayerPrefs.GetString(config.ToString());
    }

    public static void SetChangeListener(Config config, Action<object> action)
    {
        if (_listeners.ContainsKey(config))
        {
            _listeners[config].Add(action);
        }
        else
        {
            _listeners[config] = new HashSet<Action<string>> { action };
        }
    }

    public static void RemoveChangeListener(Config config, Action<string> action)
    {
        if (_listeners.ContainsKey(config))
        {
            _listeners[config].Remove(action);
        }
    }
}

public enum Config
{
    DebugMode,
    FpsCounter,
    AudioVolume,
    Language
}

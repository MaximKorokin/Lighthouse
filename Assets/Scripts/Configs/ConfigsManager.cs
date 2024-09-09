using System;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigsManager
{
    private static readonly Dictionary<ConfigKey, HashSet<Action<string>>> _listeners = new();

    public static void SetValue(ConfigKey config, string value)
    {
        if (PlayerPrefs.GetString(config.ToString()) != value.ToString() && _listeners.ContainsKey(config))
        {
            _listeners[config].ForEach(x => x(value));
        }
        PlayerPrefs.SetString(config.ToString(), value.ToString());
    }

    public static string GetValue(ConfigKey config)
    {
        return PlayerPrefs.GetString(config.ToString());
    }

    public static void SetChangeListener(ConfigKey config, Action<string> action)
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

    public static void RemoveChangeListener(ConfigKey config, Action<string> action)
    {
        if (_listeners.TryGetValue(config, out var actions))
        {
            actions.Remove(action);
        }
    }
}

public enum ConfigKey
{
    DebugMode = 0,
    FpsCounter = 1,
    AudioVolume = 2,
    Language = 3,

    ViewHPVisualization = 100,
    ViewHPChangeVisualization = 101,

    ViewLevelingSystem = 110,

    ViewPauseButton = 120,
}

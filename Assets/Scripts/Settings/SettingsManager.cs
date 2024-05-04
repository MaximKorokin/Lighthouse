using System;
using System.Collections.Generic;
using UnityEngine;

public static class SettingsManager
{
    private static readonly Dictionary<Setting, HashSet<Action<string>>> _listeners = new();

    public static void SetValue(Setting setting, string value)
    {
        if (PlayerPrefs.GetString(setting.ToString()) != value.ToString() && _listeners.ContainsKey(setting))
        {
            _listeners[setting].ForEach(x => x(value));
        }
        PlayerPrefs.SetString(setting.ToString(), value.ToString());
    }

    public static string GetValue(Setting setting)
    {
        return PlayerPrefs.GetString(setting.ToString());
    }

    public static void SetSettingChangeListener(Setting setting, Action<object> action)
    {
        if (_listeners.ContainsKey(setting))
        {
            _listeners[setting].Add(action);
        }
        else
        {
            _listeners[setting] = new HashSet<Action<string>> { action };
        }
    }

    public static void RemoveSettingChangeListener(Setting setting, Action<string> action)
    {
        if (_listeners.ContainsKey(setting))
        {
            _listeners[setting].Remove(action);
        }
    }
}

public enum Setting
{
    DebugMode,
    FpsCounter,
    AudioVolume,
    Language
}

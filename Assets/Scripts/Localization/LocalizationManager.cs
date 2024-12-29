using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LocalizationManager
{
    public static SystemLanguage Language { get; private set; } = Application.systemLanguage;

    private static readonly Dictionary<object, (string Text, Action<string> LocalizeAction)> _languageChangeListeners = new();

    static LocalizationManager()
    {
        var configLanguage = LocalizationData.FindLanguage(Constants.LanguageNameLocalizationKey, ConfigsManager.Observable.Get(ConfigKey.Language));
        if (configLanguage != SystemLanguage.Unknown)
        {
            ChangeLanguage(configLanguage);
        }
        ConfigsManager.Observable.SetChangeListener(ConfigKey.Language, x => ChangeLanguage(LocalizationData.FindLanguage(Constants.LanguageNameLocalizationKey, x.ToString())));
    }

    public static void ChangeLanguage(SystemLanguage language)
    {
        Language = language;
        _languageChangeListeners.Keys.ForEach(Localize);
    }

    public static void SetLanguageChangeListener(object listenerObject, string text, Action<string> action)
    {
        _languageChangeListeners[listenerObject] = (text, action);
        Localize(listenerObject);
    }

    public static void RemoveLanguageChangeListener(object listenerObject)
    {
        _languageChangeListeners.Remove(listenerObject);
    }

    private static void Localize(object listenerObject)
    {
        if (listenerObject == null)
        {
            Logger.Error($"Trying to localize text for null object");
            return;
        }
        var (text, localizeAction) = _languageChangeListeners[listenerObject];
        var keys = ParsingUtils.ParseLocalizationKeys(text).ToArray();
        foreach (var key in keys)
        {
            text = text.Replace(key, LocalizationData.GetLocalizedValue(Language, key));
        }
        localizeAction?.Invoke(text);
    }
}

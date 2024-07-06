using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizatonData", menuName = "ScriptableObjects/Localization/LocalizatonData", order = 3)]
public class LocalizationData : ScriptableObjectSingleton<LocalizationData>
{
    [SerializeField]
    private TextAsset _localizationData;

    private Dictionary<string, Dictionary<SystemLanguage, string>> _localizationDictionary;

    private void OnEnable()
    {
        UpdateData();
    }

    private void OnValidate()
    {
        UpdateData();
    }

    private void UpdateData()
    {
        if (_localizationData == null)
        {
            Logger.Warn("Localization data is not assigned");
            return;
        }

        var data = _localizationData.text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        var languages = ParsingUtils.ParseCsvLine(data.First()).Skip(1).Select(x =>
        {
            if (Enum.TryParse<SystemLanguage>(x, true, out var result))
            {
                return result;
            }
            Logger.Error($"{x} language is not recognized");
            return SystemLanguage.Unknown;
        }).ToList();
        _localizationDictionary = data.Skip(1)
            .Select(x => ParsingUtils.ParseCsvLine(x).ToArray())
            .ToDictionary(
                x => GetProcessedKey(x[0]),
                x => languages.ToDictionary(y => y, y => x[languages.IndexOf(y) + 1]));
    }

    public static string GetLocalizedValue(SystemLanguage language, string key)
    {
        if (Instance._localizationDictionary == null)
        {
            Instance.UpdateData();
        }

        var actualKey = GetProcessedKey(key[2..(key.Length - 1)]);
        if (Instance._localizationDictionary.ContainsKey(actualKey) && Instance._localizationDictionary[actualKey].ContainsKey(language))
        {
            return Instance._localizationDictionary[actualKey][language];
        }
        return key;
    }

    public static SystemLanguage FindLanguage(string key, string value)
    {
        key = GetProcessedKey(key);
        if (Instance._localizationDictionary.ContainsKey(key))
        {
            var result = Instance._localizationDictionary[key].Where(x => x.Value == value).ToArray();
            if (result.Length > 0)
            {
                return result[0].Key;
            }
        }
        return SystemLanguage.Unknown;
    }

    // case invariant
    private static string GetProcessedKey(string key) => key.ToLower();
}

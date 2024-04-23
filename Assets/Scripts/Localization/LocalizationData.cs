using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizatonData", menuName = "ScriptableObjects/Localization/LocalizatonData", order = 3)]
public class LocalizationData : ScriptableObjectSingleton<LocalizationData>
{
    [SerializeField]
    [TextArea]
    private string _localizationData;

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
        var data = _localizationData.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);
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
                x => x[0].ToLower(),
                x => languages.ToDictionary(y => y, y => x[languages.IndexOf(y) + 1]));
    }

    public static string GetLocalizedValue(SystemLanguage language, string key)
    {
        var actualKey = key[2..(key.Length - 1)].ToLower();
        if (Instance._localizationDictionary.ContainsKey(actualKey) && Instance._localizationDictionary[actualKey].ContainsKey(language))
        {
            return Instance._localizationDictionary[actualKey][language];
        }
        return key;
    }
}

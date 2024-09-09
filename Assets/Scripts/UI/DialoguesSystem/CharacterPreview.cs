using System;
using UnityEngine;

[Serializable]
public class CharacterPreview : IDataBaseEntry
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public string DisplayName { get; private set; }
    [field: SerializeField]
    public Color Color { get; private set; }
    [field: SerializeField]
    public Sprite Icon { get; private set; }

    [field: SerializeField]
    [field: HideInInspector]
    public string Id { get; set; }

    public override string ToString()
    {
        return $"{LocalizationData.GetLocalizedValue(SystemLanguage.English, (string.IsNullOrWhiteSpace(DisplayName) ? Name : DisplayName))} ({LocalizationData.GetLocalizedValue(SystemLanguage.English, Name)})";
    }
}

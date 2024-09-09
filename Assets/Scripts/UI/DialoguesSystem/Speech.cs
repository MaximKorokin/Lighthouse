using System;
using UnityEngine;

[Serializable]
public class Speech
{
    [field: SerializeField]
    [field: DataMapping(typeof(CharactersPreviewsDataBase))]
    public string CharacterPreviewId { get; private set; }
    [field: SerializeField]
    [field: TextArea]
    public string Text { get; private set; }
    [field: SerializeField]
    public TypingSpeed TypingSpeed { get; private set; }
}

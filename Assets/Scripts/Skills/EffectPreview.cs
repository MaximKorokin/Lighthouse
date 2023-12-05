using System;
using UnityEngine;

[Serializable]
public class EffectPreview
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: TextArea]
    [field: SerializeField]
    public string Description { get; private set; }
    [field: SerializeField]
    public Sprite Sprite { get; private set; }
}
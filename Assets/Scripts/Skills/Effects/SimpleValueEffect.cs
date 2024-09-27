using UnityEngine;

public abstract class SimpleValueEffect : Effect
{
    [field: SerializeField]
    public float Value { get; private set; }
}

using UnityEngine;

public abstract class SimpleEffect : Effect
{
    [field: SerializeField]
    public float Value { get; private set; }
}

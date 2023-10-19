using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }

    public abstract void Invoke(WorldObject source, WorldObject target);
}

using UnityEngine;

/// <summary>
/// Due to its "static" nature, Effect should not store any inner state
/// </summary>
public abstract class Effect : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: TextArea]
    [field: SerializeField]
    public string Description { get; private set; }
    [field: SerializeField]
    public Sprite Sprite { get; private set; }

    public abstract void Invoke(CastState castState);

    public void Invoke(WorldObject source) => Invoke(new CastState(source));
}

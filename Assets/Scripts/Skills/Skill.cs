using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill", order = 1)]
public class Skill : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public Effect[] Effects { get; private set; }
    [field: SerializeField]
    public float Cooldown { get; private set; }

    public void Invoke(WorldObject source) => Invoke(source, source);

    public void Invoke(WorldObject source, WorldObject target)
    {
        foreach (var effect in Effects)
        {
            effect.Invoke(new CastState(source, source, target));
        }
    }
}

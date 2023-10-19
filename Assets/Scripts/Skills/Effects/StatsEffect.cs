using UnityEngine;

[CreateAssetMenu(fileName = "StatEffect", menuName = "ScriptableObjects/Effects/StatEffect", order = 1)]
public class StatsEffect : Effect
{
    [field: SerializeField]
    public Stats StatsDelta { get; private set; }

    public override void Invoke(WorldObject source, WorldObject target)
    {
        target.ModifyStats(StatsDelta);
    }
}

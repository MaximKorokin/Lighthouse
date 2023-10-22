using UnityEngine;

[CreateAssetMenu(fileName = "AreaEffect", menuName = "ScriptableObjects/Effects/AreaEffect", order = 1)]
public class AreaEffect : PeriodicEffect
{
    [field: SerializeField]
    public float Radius { get; private set; }

    public override void Invoke(WorldObject source, WorldObject target)
    {
        foreach (var worldObject in Physics2DUtils.GetWorldObjectsInRadius(source.transform.position, Radius))
        {
            base.Invoke(source, worldObject);
        }
    }
}

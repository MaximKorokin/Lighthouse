using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AreaEffect", menuName = "ScriptableObjects/Effects/AreaEffect", order = 1)]
public class AreaEffect : PeriodicEffect
{
    [field: SerializeField]
    public float Radius { get; private set; }

    public override void Invoke(WorldObject source, WorldObject target)
    {
        foreach (var worldObject in Physics2D.OverlapCircleAll(source.transform.position, Radius)
            .Select(c => c.GetComponent<WorldObject>()).Where(w => w != null))
        {
            base.Invoke(source, worldObject);
        }
    }
}

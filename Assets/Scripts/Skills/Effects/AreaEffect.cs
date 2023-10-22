using UnityEngine;

[CreateAssetMenu(fileName = "AreaEffect", menuName = "ScriptableObjects/Effects/AreaEffect", order = 1)]
public class AreaEffect : PeriodicEffect
{
    [field: SerializeField]
    public float Radius { get; private set; }

    public override void Invoke(CastState castState)
    {
        foreach (var worldObject in Physics2DUtils.GetWorldObjectsInRadius(castState.Source.transform.position, Radius))
        {
            castState.Target = worldObject;
            base.Invoke(castState);
        }
    }
}

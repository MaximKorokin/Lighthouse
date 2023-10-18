using UnityEngine;

public class ChaseController : TriggerController
{
    [field: SerializeField]
    public float StopDistance { get; set; }
    private WorldObject Target;

    private void Update()
    {
        if (Target == null)
        {
            return;
        }
        var direction = Target.transform.position - transform.position;
        // sqrt is much slower than sqr
        if (direction.sqrMagnitude > StopDistance * StopDistance)
        {
            WorldObject.Move(direction);
        }
        else
        {
            Manipulator.Manipulate(Target);
            WorldObject.Stop();
        }
    }

    protected override void Trigger(WorldObject worldObject)
    {
        if (Target == null)
        {
            Target = worldObject;
        }
    }
}

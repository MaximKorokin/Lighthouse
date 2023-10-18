using UnityEngine;

public class StraightMovingController : TriggerController
{
    [field: SerializeField]
    public Vector2 Direction { get; set; }

    private void Update()
    {
        WorldObject.Move(Direction);
    }

    protected override void Trigger(WorldObject worldObject)
    {
        if (Manipulator.IsValidTarget(worldObject))
        {
            Manipulator.Manipulate(worldObject);
        }
    }
}

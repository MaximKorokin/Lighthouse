using UnityEngine;

public abstract class ProjectileManipulator : ManipulatorBase
{
    public override void Manipulate(WorldObject worldObject)
    {
        var destroyableWorldObject = worldObject as DestroyableWorldObject;
        Debug.Log($"Projectile {name} hit {destroyableWorldObject.name}");
    }

    public void SetDestination(WorldObject target, float yaw)
    {
        var controller = GetComponent<ControllerBase>();
        if (controller is StraightMovingController straightMovingController)
        {
            straightMovingController.Direction = Quaternion.AngleAxis(yaw, Vector3.forward) * target.transform.position.normalized;
        }
        else if (controller is ChaseController chaseController)
        {
            chaseController.Target = target;
        }
    }
}

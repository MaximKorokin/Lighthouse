using UnityEngine;

public class ProjectileEffect : EndingEffect
{
    [field: SerializeField]
    public ProjectileActor Projectile { get; private set; }
    [field: SerializeField]
    public int PierceAmount { get; private set; }

    public override void Invoke(CastState castState)
    {
        if (castState.InitialSource is not DestroyableWorldObject destroyable || destroyable.IsAlive)
        {
            var projectile = Object.Instantiate(Projectile, castState.Source.transform.position + (Vector3)castState.Source.VisualPositionOffset, Quaternion.identity);
            projectile.SetProjectileEffect(this, castState);
            projectile.GetComponent<StraightMovingController>().SetTargetPosition(castState.GetTargetPosition());
        }
    }
}

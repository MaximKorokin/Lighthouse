using UnityEngine;

public class ProjectileEffect : EndingEffect
{
    [field: SerializeField]
    public ProjectileActor Projectile { get; private set; }
    [field: SerializeField]
    public int PierceAmount { get; private set; }

    public override void Invoke(CastState castState)
    {
        var projectile = Object.Instantiate(Projectile, castState.Source.transform.position + (Vector3)castState.Source.VisualPositionOffset, Quaternion.identity);
        projectile.SetProjectileEffect(this, castState);
        projectile.GetComponent<StraightMovingController>().SetTargetPosition(castState.GetTargetPosition());
    }
}

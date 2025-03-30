using UnityEngine;

public class ProjectileEffect : EndingEffect
{
    [field: SerializeField]
    public ProjectileActor Projectile { get; private set; }
    [field: SerializeField]
    public int PierceAmount { get; private set; }

    public override void Invoke(CastState castState)
    {
        var position = (Vector2)castState.Source.transform.position + castState.Source.VisualSize * Vector2.up * 0.5f;
        var projectile = Object.Instantiate(Projectile, position, Quaternion.identity);
        projectile.SetProjectileEffect(this, castState);
        projectile.GetComponent<StraightMovingController>().SetTargetPosition(castState.GetTargetPosition());
    }
}

using System.Linq;
using UnityEngine;

public class ProjectileEffect : EndingEffect
{
    [field: SerializeField]
    public ProjectileActor Projectile { get; private set; }
    [field: SerializeField]
    public int Amount { get; private set; }
    [field: SerializeField]
    public int PierceAmount { get; private set; }
    [field: SerializeField]
    public float Spread { get; private set; }
    [field: SerializeField]
    public TargetType TargetType { get; private set; }

    /// <summary>
    /// Instanciates projectiles from prefab and inits them.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public override void Invoke(CastState castState)
    {
        CreateProjectiles(this, castState);
    }

    /// <summary>
    /// Calls Invoke method of base class. Should be called outside.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public void InvokeEffects(CastState castState)
    {
        base.Invoke(castState);
    }

    /// <summary>
    /// If <paramref name="source"/> == <paramref name="target"/> it will try to find a new target.
    /// If no target found, it will not do anything.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="yaw"></param>
    /// <param name="effect"></param>
    private static void CreateProjectiles(ProjectileEffect effect, CastState castState)
    {
        var spreadStep = 0f;
        var currentYaw = 0f;
        if (effect.Amount > 1)
        {
            spreadStep = effect.Spread / (effect.Amount - 1);
            currentYaw = -effect.Spread / 2;
        }

        for (int i = 0; i < effect.Amount; i++)
        {
            if (castState.Source == castState.Target)
            {
                var targets = Physics2DUtils.GetWorldObjectsInRadius(castState.Source.transform.position, castState.Source.ActionRange)
                    .GetValidTargets(castState.InitialSource)
                    .GetValidTargets(effect.Projectile.WorldObject)
                    .ToArray();
                if (targets.Length > 0)
                {
                    CreateAndGetController().ChooseTarget(targets, effect.TargetType, castState.Source, currentYaw);
                }
            }
            else if (castState.Target.IsValidTarget(effect.Projectile.WorldObject))
            {
                CreateAndGetController().SetTarget(castState.Target, currentYaw);
            }
            currentYaw += spreadStep;
        }

        TargetController CreateAndGetController()
        {
            var projectile = Object.Instantiate(effect.Projectile, castState.Source.transform.position, Quaternion.identity);
            projectile.SetProjectileEffect(effect, castState);
            return projectile.GetComponent<TargetController>();
        }
    }
}

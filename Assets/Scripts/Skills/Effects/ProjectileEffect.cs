using System.Linq;
using UnityEngine;

public class ProjectileEffect : EndingEffect
{
    [field: SerializeField]
    public ProjectileActor Projectile { get; private set; }
    [field: SerializeField]
    public int PierceAmount { get; private set; }
    [field: SerializeField]
    public int Amount { get; private set; }
    [field: SerializeField]
    public float AmountSpread { get; private set; }
    [field: SerializeField]
    public float RandomSpread { get; private set; }
    [field: SerializeField]
    public TargetSearchingType TargetType { get; private set; }

    /// <summary>
    /// Instanciates projectiles from prefab and inits them.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public override void Invoke(CastState castState)
    {
        CreateProjectiles(castState);
    }

    /// <summary>
    /// If <paramref name="source"/> == <paramref name="target"/> it will try to find a new target.
    /// If no target found, it will not do anything.
    /// </summary>
    private void CreateProjectiles(CastState castState)
    {
        var spreadStep = 0f;
        var currentYaw = 0f;
        if (Amount > 1)
        {
            spreadStep = AmountSpread / (Amount - 1);
            currentYaw = -AmountSpread / 2;
        }

        for (int i = 0; i < Amount; i++)
        {
            var resultingYaw = currentYaw + Random.Range(-RandomSpread, RandomSpread);
            if (castState.Source == castState.Target)
            {
                var targets = Physics2DUtils.GetWorldObjectsInRadius(castState.Source.transform.position, castState.Source.ActionRange)
                    .GetValidTargets(castState.InitialSource, FactionsRelation.Enemy)
                    .GetValidTargets(Projectile.WorldObject, FactionsRelation.Enemy)
                    .ToArray();
                if (targets.Length > 0)
                {
                    CreateAndGetController().ChooseTarget(targets, TargetType, castState.Source, resultingYaw);
                }
            }
            else if (castState.Target.IsValidTarget(Projectile.WorldObject, FactionsRelation.Enemy))
            {
                CreateAndGetController().SetTarget(castState.Target, resultingYaw);
            }
            currentYaw += spreadStep;
        }

        TargetController CreateAndGetController()
        {
            var projectile = Object.Instantiate(Projectile, castState.Source.transform.position + (Vector3)castState.Source.VisualPositionOffset, Quaternion.identity);
            projectile.SetProjectileEffect(this, castState);
            return projectile.GetComponent<TargetController>();
        }
    }
}

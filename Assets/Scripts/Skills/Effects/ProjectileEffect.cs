using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileEffect", menuName = "ScriptableObjects/Effects/ProjectileEffect", order = 1)]
public class ProjectileEffect : ComplexEffect
{
    [field: SerializeField]
    public Projectile Projectile { get; private set; }
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
        ProjectileUtils.CreateProjectiles(this, castState);
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
}

using UnityEngine;

public class Projectile : MovableWorldObject
{
    [field: SerializeField]
    public float LifeTime { get; protected set; }

    public void SetDamage(float damage)
    {

    }
}

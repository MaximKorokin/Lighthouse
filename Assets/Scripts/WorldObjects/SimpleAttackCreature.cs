using UnityEngine;

public class SimpleAttackCreature : Creature
{
    [field: SerializeField]
    public float AttackRange { get; protected set; }
}

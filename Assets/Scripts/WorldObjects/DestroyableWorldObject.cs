using System;
using UnityEngine;

public abstract class DestroyableWorldObject : WorldObject
{
    [field: SerializeField]
    public bool IsDamagable { get; protected set; } = true;
    public bool IsAlive { get; protected set; } = true;

    private float _currentHealthPoints;
    protected float CurrentHealthPoints
    {
        get => _currentHealthPoints;
        private set
        {
            _currentHealthPoints = Math.Min(value, Stats[StatName.MaxHealthPoints]);
            if (_currentHealthPoints <= 0)
            {
                DestroyWorldObject();
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        CurrentHealthPoints = Stats[StatName.MaxHealthPoints];
    }

    protected override void OnStatsModified()
    {
        base.OnStatsModified();
        if (Stats[StatName.MaxHealthPoints] < CurrentHealthPoints)
        {
            CurrentHealthPoints = Stats[StatName.MaxHealthPoints];
        }
    }

    public virtual void Damage(float damageValue)
    {
        if (!IsDamagable || damageValue <= 0)
        {
            return;
        }
        CurrentHealthPoints -= damageValue;
    }

    public virtual void Heal(float healValue)
    {
        if (healValue <= 0)
        {
            return;
        }
        CurrentHealthPoints += healValue;
    }

    public virtual void DestroyWorldObject()
    {
        IsAlive = false;
        Debug.Log(name + " destroyed");
    }
}

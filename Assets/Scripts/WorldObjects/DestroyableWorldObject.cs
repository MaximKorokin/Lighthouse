using System;
using UnityEngine;

public class DestroyableWorldObject : WorldObject
{
    public bool IsAlive { get; protected set; }

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

    public override void ModifyStats(Stats otherStats)
    {
        base.ModifyStats(otherStats);
        if (Stats[StatName.MaxHealthPoints] < CurrentHealthPoints)
        {
            CurrentHealthPoints = Stats[StatName.MaxHealthPoints];
        }
    }

    public virtual void Damage(float damageValue)
    {
        if (damageValue <= 0)
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
        Debug.Log(name + " destroyed");
        Destroy(this);
    }
}

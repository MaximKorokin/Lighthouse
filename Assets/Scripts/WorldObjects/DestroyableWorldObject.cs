using System;
using UnityEngine;

public abstract class DestroyableWorldObject : WorldObject
{
    [field: SerializeField]
    public bool IsDamagable { get; set; } = true;
    [field: SerializeField]
    public bool IsAlive { get; private set; } = true;
    [field: SerializeField]
    public Effect DestroyEffect { get; set; }

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
        if (!IsAlive)
        {
            return;
        }

        if (!IsDamagable || damageValue <= 0)
        {
            return;
        }

        CurrentHealthPoints -= damageValue;

        SetAnimatorValue(AnimatorKey.Hurt, true);
        SetAnimatorValue(AnimatorKey.HPRatio, CurrentHealthPoints / Stats[StatName.MaxHealthPoints]);
    }

    public virtual void Heal(float healValue)
    {
        if (!IsAlive)
        {
            return;
        }

        if (healValue <= 0)
        {
            return;
        }
        CurrentHealthPoints += healValue;
    }

    public virtual void DestroyWorldObject()
    {
        if (!IsAlive)
        {
            return;
        }

        SetAnimatorValue(AnimatorKey.Dead, true);

        if (DestroyEffect != null)
        {
            DestroyEffect.Invoke(new CastState(this));
        }

        IsAlive = false;
        Destroy(gameObject, 15);
    }
}

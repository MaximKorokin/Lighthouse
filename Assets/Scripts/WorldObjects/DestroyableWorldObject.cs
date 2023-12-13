using System;
using UnityEngine;

public abstract class DestroyableWorldObject : WorldObject
{
    [field: SerializeField]
    public bool IsDamagable { get; private set; } = true;
    [field: SerializeField]
    public bool IsAlive { get; private set; } = true;
    [field: SerializeField]
    public float DestroyTime { get; private set; }

    public virtual float MaxHealthPoints => Stats[StatName.MaxHealthPoints];

    private float _currentHealthPoints;
    public float CurrentHealthPoints
    {
        get => _currentHealthPoints;
        private set
        {
            var previousValue = _currentHealthPoints;
            _currentHealthPoints = Math.Min(value, Stats[StatName.MaxHealthPoints]);
            if (_currentHealthPoints <= 0)
            {
                DestroyWorldObject();
            }

            HealthPointsChanged?.Invoke(previousValue, _currentHealthPoints, MaxHealthPoints);
            SetAnimatorValue(AnimatorKey.HPRatio, CurrentHealthPoints / Stats[StatName.MaxHealthPoints]);
        }
    }

    /// <summary>
    /// Action<PreviousHP, CurrentHP, MaxHP>
    /// <typeparam name="PreviousHP"></typeparam>
    /// <typeparam name="CurrentHP"></typeparam>
    /// <typeparam name="MaxHP"></typeparam>
    /// </summary>
    public event Action<float, float, float> HealthPointsChanged;
    public event Action Destroying;

    protected override void Start()
    {
        base.Start();
        CurrentHealthPoints = Stats[StatName.MaxHealthPoints];
    }

    protected override void OnStatsModified()
    {
        base.OnStatsModified();

        var previousHP = CurrentHealthPoints;
        if (MaxHealthPoints < CurrentHealthPoints)
        {
            CurrentHealthPoints = MaxHealthPoints;
        }
        HealthPointsChanged?.Invoke(previousHP, CurrentHealthPoints, MaxHealthPoints);
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

        Destroying?.Invoke();

        IsAlive = false;

        SetAnimatorValue(AnimatorKey.Dead, true);

        Destroy(gameObject, DestroyTime);
    }
}

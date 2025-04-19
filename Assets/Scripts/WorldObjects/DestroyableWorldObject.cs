using System;
using UnityEngine;

public abstract class DestroyableWorldObject : WorldObject
{
    [field: SerializeField]
    public bool IsDamagable { get; set; } = true;
    [field: SerializeField]
    public float DestroyTime { get; set; }

    public bool IsAlive { get; private set; } = true;

    public float MaxHealthPoints => Stats[StatName.MaxHealthPoints];
    public float HPRegen => Stats[StatName.HPRegen];

    private float _currentHealthPoints;
    public float CurrentHealthPoints
    {
        get => _currentHealthPoints;
        private set
        {
            var previousValue = _currentHealthPoints;
            _currentHealthPoints = Math.Max(0, Math.Min(value, MaxHealthPoints));

            if (previousValue == _currentHealthPoints) return;

            HealthPointsChanged?.Invoke(previousValue, _currentHealthPoints, MaxHealthPoints);
            SetAnimatorValue(AnimatorKey.HPRatio, _currentHealthPoints / MaxHealthPoints);

            if (_currentHealthPoints == 0) DestroyWorldObject();
        }
    }

    /// <summary>
    /// Action<PreviousHP, CurrentHP, MaxHP>
    /// <typeparam name="PreviousHP"></typeparam>
    /// <typeparam name="CurrentHP"></typeparam>
    /// <typeparam name="MaxHP"></typeparam>
    /// </summary>
    public event Action<float, float, float> HealthPointsChanged;
    public event RefAction<float> Damaged;
    public event Action Destroying;

    protected override void Awake()
    {
        base.Awake();
        CurrentHealthPoints = MaxHealthPoints;
    }

    private void Update()
    {
        if (!IsAlive)
        {
            return;
        }
        if (HPRegen > 0 && CurrentHealthPoints < MaxHealthPoints)
        {
            CurrentHealthPoints += HPRegen * Time.deltaTime;
        }
    }

    protected override void OnStatsModified()
    {
        base.OnStatsModified();

        if (MaxHealthPoints < CurrentHealthPoints)
        {
            CurrentHealthPoints = MaxHealthPoints;
        }
    }

    public virtual void Damage(float damageValue)
    {
        if (!IsAlive || !IsDamagable) return;

        Damaged?.Invoke(ref damageValue);

        if (damageValue <= 0) return;

        CurrentHealthPoints -= damageValue;
        SetAnimatorValue(AnimatorKey.Hurt, true);
    }

    public virtual void Heal(float healValue)
    {
        if (!IsAlive || healValue <= 0) return;

        CurrentHealthPoints += healValue;
    }

    public virtual void DestroyWorldObject()
    {
        if (!IsAlive) return;

        SetAnimatorValue(AnimatorKey.IsDead, true);

        IsAlive = false;
        Destroying?.Invoke();
        Destroy(gameObject, DestroyTime);
    }

    public override void SetAnimatorValue<T>(AnimatorKey key, T value = default)
    {
        if (IsAlive)
        {
            base.SetAnimatorValue(key, value);
        }
    }
}

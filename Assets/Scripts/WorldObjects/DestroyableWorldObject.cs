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
    public float MaxShieldValue => Stats[StatName.MaxShield];
    public float HPRegen => Stats[StatName.HPRegen];
    public float ShieldRegen => Stats[StatName.ShieldRegen];

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

    private readonly CooldownCounter _shieldDelayCounter = new(0);
    private float _currentShieldValue;
    public float CurrentShieldValue
    {
        get => _currentShieldValue;
        private set
        {
            var previousValue = _currentShieldValue;
            _currentShieldValue = Math.Max(0, Math.Min(value, MaxShieldValue));

            if (previousValue == _currentShieldValue) return;

            ShieldValueChanged?.Invoke(previousValue, _currentShieldValue, MaxShieldValue);
            SetAnimatorValue(AnimatorKey.ShieldRatio, MaxShieldValue > 0 ? (_currentShieldValue / MaxShieldValue) : 0);
        }
    }

    /// <summary>
    /// Action<PreviousHP, CurrentHP, MaxHP>
    /// <typeparam name="PreviousHP"></typeparam>
    /// <typeparam name="CurrentHP"></typeparam>
    /// <typeparam name="MaxHP"></typeparam>
    /// </summary>
    public event Action<float, float, float> HealthPointsChanged;
    public event Action<float, float, float> ShieldValueChanged;
    public event Action Destroying;

    protected override void Awake()
    {
        base.Start();
        CurrentHealthPoints = MaxHealthPoints;
        CurrentShieldValue = MaxShieldValue;
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
        if (_shieldDelayCounter.IsOver() && ShieldRegen > 0 && CurrentShieldValue < MaxShieldValue)
        {
            CurrentShieldValue += ShieldRegen * Time.deltaTime;
        }
    }

    protected override void OnStatsModified()
    {
        base.OnStatsModified();

        if (MaxHealthPoints < CurrentHealthPoints)
        {
            CurrentHealthPoints = MaxHealthPoints;
        }
        
        if (MaxShieldValue < CurrentShieldValue)
        {
            CurrentShieldValue = MaxShieldValue;
        }
        _shieldDelayCounter.Cooldown = Stats[StatName.ShieldDelay];
    }

    public virtual void Damage(float damageValue)
    {
        if (!IsAlive || !IsDamagable || damageValue <= 0)
        {
            return;
        }

        _shieldDelayCounter.Reset();
        var remainingDamage = damageValue - CurrentShieldValue;
        CurrentShieldValue -= damageValue;
        if (remainingDamage <= 0)
        {
            return;
        }

        CurrentHealthPoints -= remainingDamage;
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

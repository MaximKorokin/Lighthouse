using System;
using UnityEngine;

public abstract class DestroyableWorldObject : WorldObject
{
    [field: SerializeField]
    public bool IsDamagable { get; set; } = true;
    [field: SerializeField]
    public bool IsAlive { get; private set; } = true;
    [field: SerializeField]
    public float DestroyTime { get; private set; }

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

    protected override void Start()
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
        if (HPRegen > 0)
        {
            CurrentHealthPoints += HPRegen * Time.deltaTime;
        }
        if (_shieldDelayCounter.IsOver() && ShieldRegen > 0)
        {
            CurrentShieldValue += ShieldRegen * Time.deltaTime;
        }
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
        
        var previousShield = CurrentShieldValue;
        if (MaxShieldValue < CurrentShieldValue)
        {
            CurrentShieldValue = MaxShieldValue;
        }
        ShieldValueChanged?.Invoke(previousShield, CurrentShieldValue, MaxShieldValue);
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

        SetAnimatorValue(AnimatorKey.Dead, true);

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

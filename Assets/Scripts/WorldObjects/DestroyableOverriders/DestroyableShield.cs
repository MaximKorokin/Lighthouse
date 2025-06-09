using System;
using UnityEngine;

public class DestroyableShield : DestroyableOverrider
{
    private readonly CooldownCounter _shieldDelayCounter = new(0);

    [SerializeField]
    private float _shieldRatio = 0.5f;
    [SerializeField]
    private float _regenDelay = 2;
    [SerializeField]
    private float _regenRatio = 0.2f;

    private float _maxShieldValue;
    private float _currentShieldValue;
    public float CurrentShieldValue
    {
        get => _currentShieldValue;
        private set
        {
            var previousValue = _currentShieldValue;
            _currentShieldValue = Math.Max(0, Math.Min(value, _maxShieldValue));

            if (previousValue == _currentShieldValue) return;

            ShieldValueChanged?.Invoke(previousValue, _currentShieldValue, _maxShieldValue);
            Destroyable.SetAnimatorValue(AnimatorKey.ShieldRatio, _maxShieldValue > 0 ? (_currentShieldValue / _maxShieldValue) : 0);
        }
    }

    public event Action<float, float, float> ShieldValueChanged;

    protected override void Start()
    {
        base.Start();

        Destroyable.Stats.Modified += OnDestroyableStatsModified;
        OnDestroyableStatsModified();
        CurrentShieldValue = _maxShieldValue;
        _shieldDelayCounter.Cooldown = _regenDelay;
    }

    private void OnDestroyableStatsModified()
    {
        _maxShieldValue = Destroyable.MaxHealthPoints * _shieldRatio;
    }

    private void Update()
    {
        if (_shieldDelayCounter.IsOver() && _regenRatio > 0 && CurrentShieldValue < _maxShieldValue)
        {
            CurrentShieldValue += _maxShieldValue * _regenRatio * Time.deltaTime;
        }
    }

    protected override void Damaged(ref float damageValue)
    {
        if (damageValue <= 0) return;

        if (CurrentShieldValue > 0) Destroyable.SetAnimatorValue(AnimatorKey.HurtShield, true);

        _shieldDelayCounter.Reset();
        var remainingDamage = damageValue - CurrentShieldValue;
        CurrentShieldValue -= damageValue;
        damageValue = remainingDamage;
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public class EffectorDestroyable : Effector
{
    [SerializeField]
    private EffectSettings[] _damageEffectsSettings;
    [SerializeField]
    private EffectSettings[] _destroyEffectsSettings;

    private Dictionary<Effect[], CastState> _damageEffects;
    private Dictionary<Effect[], CastState> _destroyEffects;

    protected override void Start()
    {
        base.Start();

        var worldObject = GetComponent<DestroyableWorldObject>();

        _damageEffects = _damageEffectsSettings.ToDictionary(x => x.GetEffects(), x => new CastState(worldObject, x.Cooldown));
        _destroyEffects = _destroyEffectsSettings.ToDictionary(x => x.GetEffects(), x => new CastState(worldObject, x.Cooldown));

        worldObject.HealthPointsChanged += (prev, cur, max) => { if (cur < prev) OnDamaged(); };
        worldObject.Destroying += OnDestroying;
    }

    private void OnDamaged()
    {
        _damageEffects.ForEach(x => InvokeEffects(x.Key, x.Value));
    }

    private void OnDestroying()
    {
        _destroyEffects.ForEach(x => InvokeEffects(x.Key, x.Value));
    }
}

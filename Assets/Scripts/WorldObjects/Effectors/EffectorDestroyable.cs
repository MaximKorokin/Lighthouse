using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public class EffectorDestroyable : Effector
{
    [SerializeField]
    private EffectSettings[] _damageEffectsSettings;
    [SerializeField]
    private EffectSettings[] _destroyEffectsSettings;

    private DestroyableWorldObject _worldObject;
    private Effect[] _damageEffects;
    private Effect[] _destroyEffects;

    protected override void Start()
    {
        base.Start();

        _damageEffects = _damageEffectsSettings.GetEffects();
        _destroyEffects = _destroyEffectsSettings.GetEffects();

        _worldObject = GetComponent<DestroyableWorldObject>();
        _worldObject.Damaged += OnDamaged;
        _worldObject.Destroying += OnDestroying;
    }

    private void OnDamaged()
    {
        InvokeEffects(_damageEffects, _worldObject);
    }

    private void OnDestroying()
    {
        InvokeEffects(_destroyEffects, _worldObject);
    }
}

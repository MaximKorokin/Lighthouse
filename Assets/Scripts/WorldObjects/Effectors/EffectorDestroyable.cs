using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public class EffectorDestroyable : Effector
{
    [SerializeField]
    private Effect[] _damageEffects;
    [SerializeField]
    private Effect[] _destroyEffects;

    private DestroyableWorldObject _worldObject;

    protected override void Start()
    {
        base.Start();

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

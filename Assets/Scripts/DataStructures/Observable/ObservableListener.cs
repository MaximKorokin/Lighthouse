using System;
using UnityEngine;

public abstract class ObservableListener<K, V> : MonoBehaviour where V : IEquatable<V>
{
    [field: SerializeField]
    protected K Key { get; private set; }

    protected abstract ObservableKeyValueStoreWrapper<K, V> Observable {  get; }

    protected virtual void Awake()
    {
        // Start analogue but with a guarantee that it will
        // invoke even if component is disabled
        CoroutinesHandler.StartUniqueCoroutine(this, CoroutinesUtils.WaitForSeconds(0).Then(() => OnObservableValueChanged(Observable.Get(Key))));
    }

    protected virtual void Start()
    {
        Observable.SetChangeListener(Key, OnObservableValueChanged);
    }

    protected virtual void OnDestroy()
    {
        Observable.RemoveChangeListener(Key, OnObservableValueChanged);
    }

    protected abstract void OnObservableValueChanged(V val);
}

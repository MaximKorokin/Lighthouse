using System;
using UnityEngine;

public abstract class ObservableListener<K, V> : MonoBehaviour where V : IEquatable<V>
{
    [field: SerializeField]
    protected K Key { get; private set; }

    protected abstract ObservableKeyValueStoreWrapper<K, V> Observable { get; }

    private void Awake()
    {
        BehaviourCallsMediator.RequestLateAwakeCall(0, () =>
        {
            Observable.SetChangeListener(Key, OnObservableValueChanged);
            OnObservableValueChanged(Observable.Get(Key));
        });
    }

    protected virtual void OnDestroy()
    {
        Observable.RemoveChangeListener(Key, OnObservableValueChanged);
    }

    protected abstract void OnObservableValueChanged(V val);
}

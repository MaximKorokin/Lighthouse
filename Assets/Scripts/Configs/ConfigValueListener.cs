using UnityEngine;

public abstract class ConfigValueListener : MonoBehaviour
{
    [field: SerializeField]
    protected Config Config { get; private set; }

    protected virtual void Awake()
    {
        ConfigsManager.SetChangeListener(Config, OnConfigValueChanged);
    }

    protected virtual void OnDestroy()
    {
        ConfigsManager.RemoveChangeListener(Config, OnConfigValueChanged);
    }

    protected abstract void OnConfigValueChanged(object val);
}

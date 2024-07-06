using UnityEngine;

public abstract class ConfigValueListener : MonoBehaviour
{
    [field: SerializeField]
    protected ConfigKey Config { get; private set; }

    protected virtual void Start()
    {
        ConfigsManager.SetChangeListener(Config, OnConfigValueChanged);
    }

    protected virtual void OnDestroy()
    {
        ConfigsManager.RemoveChangeListener(Config, OnConfigValueChanged);
    }

    protected abstract void OnConfigValueChanged(object val);
}

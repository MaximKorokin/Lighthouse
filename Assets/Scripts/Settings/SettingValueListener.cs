using UnityEngine;

public abstract class SettingValueListener : MonoBehaviour
{
    [field: SerializeField]
    protected Setting Setting { get; private set; }

    protected virtual void Awake()
    {
        SettingsManager.SetSettingChangeListener(Setting, OnSettingValueChanged);
    }

    protected virtual void OnDestroy()
    {
        SettingsManager.RemoveSettingChangeListener(Setting, OnSettingValueChanged);
    }

    protected abstract void OnSettingValueChanged(object val);
}

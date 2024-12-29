using UnityEngine;

[RequireComponent(typeof(LoadableUIProvider))]
public class UIStateSetter : MonoBehaviour
{
    [SerializeField]
    private UIState _state;

    private LoadableUIProvider _provider;

    private void Start()
    {
        _provider = GetComponent<LoadableUIProvider>();

        UIStateManager.Observable.SetChangeListener(_state, OnConfigValueChanged);
    }

    private void OnConfigValueChanged(bool val)
    {
        _provider.UIElement.gameObject.SetActive(val);
    }

    private void OnDestroy()
    {
        UIStateManager.Observable.RemoveChangeListener(_state, OnConfigValueChanged);
    }
}

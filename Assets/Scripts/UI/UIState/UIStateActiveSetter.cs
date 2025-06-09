using UnityEngine;

public class UIStateActiveSetter : ObservableListener<UIState, bool>
{
    [SerializeField]
    private LoadableUIProvider _provider;

    protected override ObservableKeyValueStoreWrapper<UIState, bool> Observable => UIStateManager.Observable;

    protected override void OnObservableValueChanged(bool val)
    {
        _provider.UIElement.gameObject.SetActive(val);
    }
}

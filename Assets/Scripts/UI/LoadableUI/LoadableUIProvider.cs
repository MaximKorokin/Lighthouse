using UnityEngine;

public class LoadableUIProvider : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Leave empty to use LoadableUIParent with IsMain flag")]
    private LoadableUIParent _loadableUIParent;
    [SerializeField]
    private RectTransform _uiElementPrefab;
    [SerializeField]
    private int _priority;

    private RectTransform _uiElement;
    public RectTransform UIElement { get => _uiElement == null ? (_uiElement = InitializeUIElement()) : _uiElement; }

    public bool HasLoaded => _uiElement != null;

    private RectTransform InitializeUIElement()
    {
        var uiElement = Instantiate(_uiElementPrefab);

        if (_loadableUIParent == null) LoadableUIParent.Instance.SetUIElement(uiElement, _priority);
        else _loadableUIParent.SetUIElement(uiElement, _priority);

        return uiElement;
    }
}

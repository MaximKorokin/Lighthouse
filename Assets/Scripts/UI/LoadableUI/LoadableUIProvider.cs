using UnityEngine;

public class LoadableUIProvider : MonoBehaviour
{
    [SerializeField]
    private RectTransform _uiElementPrefab;
    [SerializeField]
    private int _priority;

    private RectTransform _uiElement;
    public RectTransform UIElement { get => _uiElement == null ? (_uiElement = InitializeUIElement()) : _uiElement; }

    private RectTransform InitializeUIElement()
    {
        var uiElement = Instantiate(_uiElementPrefab);
        LoadableUIParent.SetUIElement(uiElement, _priority);
        return uiElement;
    }
}
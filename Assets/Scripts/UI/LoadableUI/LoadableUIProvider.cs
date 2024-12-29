using UnityEngine;

public class LoadableUIProvider : MonoBehaviour
{
    [SerializeField]
    private RectTransform _uiElementPrefab;

    private RectTransform _uiElement;
    public RectTransform UIElement { get => _uiElement == null ? (_uiElement = InitializeUIElement()) : _uiElement; }

    private RectTransform InitializeUIElement()
    {
        var uiElement = Instantiate(_uiElementPrefab);
        uiElement.transform.SetParent(LoadableUIParent.Instance.transform, false);
        return uiElement;
    }
}
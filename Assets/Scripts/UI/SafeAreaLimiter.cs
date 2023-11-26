using UnityEngine;

[ExecuteInEditMode]
public class SafeAreaLimiter : MonoBehaviour
{
    private Rect _safeArea;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_safeArea != Screen.safeArea)
        {
            _safeArea = Screen.safeArea;
            LimitSafeArea();
        }
    }

    private void LimitSafeArea()
    {
        var widthOffset = Screen.width - _safeArea.width;
        var heightOffset = Screen.height - _safeArea.height;
        var offsetMin = new Vector2(
            -Screen.width / 2 + _safeArea.x,
            -Screen.height / 2 + _safeArea.y);
        var offsetMax = new Vector2(
            Screen.width / 2 - widthOffset + _safeArea.x,
            Screen.height / 2 - heightOffset + _safeArea.y);
        _rectTransform.offsetMin = offsetMin;
        _rectTransform.offsetMax = offsetMax;
    }
}

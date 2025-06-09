using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class SafeAreaLimiter : MonoBehaviour
{
    [SerializeField]
    private CanvasScaler _canvasScaler;

    private Rect _safeArea;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        if (_canvasScaler == null)
        {
            Logger.Error($"{nameof(_canvasScaler)} in {nameof(SafeAreaLimiter)} is null");
        }
    }

    private void Update()
    {
        if (_safeArea != Screen.safeArea && _canvasScaler != null)
        {
            _safeArea = Screen.safeArea;
            LimitSafeArea();
        }
    }

    private void LimitSafeArea()
    {
        var scaleVector = _canvasScaler.referenceResolution / new Vector2(Screen.width, Screen.height);
        var safePos = _safeArea.position * scaleVector;
        var safeSize = _safeArea.size * scaleVector;

        _rectTransform.offsetMin = safePos;
        _rectTransform.offsetMax = safePos -
            new Vector2(_canvasScaler.referenceResolution.x - safeSize.x, _canvasScaler.referenceResolution.y - safeSize.y);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// -1 means LMB,
    /// 0 means first touch,
    /// documentation: https://docs.unity3d.com/2018.3/Documentation/ScriptReference/EventSystems.PointerEventData-pointerId.html
    /// </summary>
    private readonly int[] _allowedPointerIds = new[] { -1, 0 };

    [SerializeField]
    private Image _joystickBackground;
    [SerializeField]
    private Image _joystick;
    [SerializeField]
    private Image _joystickArea;

    public Vector2 InputVector { get; private set; }

    private void Awake()
    {
        GameManager.InputBlockChanged += OnInputBlockChanged;
    }

    private void OnDestroy()
    {
        GameManager.InputBlockChanged -= OnInputBlockChanged;
    }

    private void OnInputBlockChanged(bool blocked)
    {
        if (blocked)
        {
            DisableJoystick();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.IsControlInputBlocked || !IsAllowedPointerId(eventData))
        {
            return;
        }

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _joystickBackground.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 joystickPosition))
        {
            EnableJoystick();
            joystickPosition.x = joystickPosition.x * 2 / _joystickBackground.rectTransform.sizeDelta.x;
            joystickPosition.y = joystickPosition.y * 2 / _joystickBackground.rectTransform.sizeDelta.y;

            InputVector = (joystickPosition.sqrMagnitude > 1) ? joystickPosition.normalized : joystickPosition;

            _joystick.rectTransform.anchoredPosition = new Vector2(
                InputVector.x * (_joystickBackground.rectTransform.sizeDelta.x / 2),
                InputVector.y * (_joystickBackground.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.IsControlInputBlocked || !IsAllowedPointerId(eventData))
        {
            return;
        }

        if (!_joystickBackground.gameObject.activeSelf &&
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _joystickArea.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 joystickBackgroundPosition))
        {
            EnableJoystick();
            _joystickBackground.rectTransform.anchoredPosition = joystickBackgroundPosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!IsAllowedPointerId(eventData))
        {
            return;
        }
        DisableJoystick();
    }

    private void EnableJoystick()
    {
        if (!_joystickBackground.gameObject.activeSelf)
        {
            _joystickBackground.gameObject.SetActive(true);
        }
    }

    private void DisableJoystick()
    {
        if (!_joystickBackground.gameObject.activeSelf)
        {
            return;
        }

        _joystickBackground.gameObject.SetActive(false);
        InputVector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    private bool IsAllowedPointerId(PointerEventData eventData) => System.Array.IndexOf(_allowedPointerIds, eventData.pointerId) != -1;
}

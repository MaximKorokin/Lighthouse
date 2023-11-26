using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Image _joystickBackground;
    [SerializeField]
    private Image _joystick;
    [SerializeField]
    private Image _joystickArea;

    public Vector2 InputVector { get; private set; }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _joystickBackground.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 joystickPosition))
        {
            joystickPosition.x = joystickPosition.x * 2 / _joystickBackground.rectTransform.sizeDelta.x;
            joystickPosition.y = joystickPosition.y * 2 / _joystickBackground.rectTransform.sizeDelta.y;

            InputVector = new Vector2(joystickPosition.x, joystickPosition.y);
            InputVector = (InputVector.magnitude > 1f) ? InputVector.normalized : InputVector;

            _joystick.rectTransform.anchoredPosition = new Vector2(
                InputVector.x * (_joystickBackground.rectTransform.sizeDelta.x / 2),
                InputVector.y * (_joystickBackground.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _joystickArea.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 joystickBackgroundPosition))
        {
            _joystickBackground.gameObject.SetActive(true);
            _joystickBackground.rectTransform.anchoredPosition = new Vector2(
                joystickBackgroundPosition.x,
                joystickBackgroundPosition.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickBackground.gameObject.SetActive(false);
        InputVector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
}

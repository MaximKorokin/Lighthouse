using UnityEngine;
using UnityEngine.EventSystems;

public class TabletZenGameController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    [SerializeField]
    private TabletZenGamePlayer _player;

    private bool _isPointerDown;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = transform as RectTransform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
        SetTargetPosition(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (_isPointerDown)
        {
            SetTargetPosition(eventData);
        }
    }

    private void SetTargetPosition(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, Camera.main, out var targetLocalPoint))
        {
            _player.TargetPosition = targetLocalPoint;
        }
    }
}

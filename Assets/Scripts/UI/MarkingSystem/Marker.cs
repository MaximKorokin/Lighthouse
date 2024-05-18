using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    [SerializeField]
    private Sprite _arrowSprite;
    [SerializeField]
    private Sprite _pointSprite;

    private Image _image;
    private RectTransform _parentRect;

    public Transform MarkingTarget { get; set; }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _parentRect = transform.parent.GetComponent<RectTransform>();
    }

    private void Update()
    {
        var markerPosition = Camera.main.WorldToScreenPoint(MarkingTarget.position) - (Vector3)_parentRect.anchoredPosition - new Vector3(Screen.width, Screen.height) / 2;
        var parentBounds = new Bounds(Vector3.zero, new Vector3(_parentRect.rect.width, _parentRect.rect.height));
        transform.localPosition = parentBounds.ClosestPoint(markerPosition);

        if ((Vector2)markerPosition != (Vector2)transform.localPosition)
        {
            _image.sprite = _arrowSprite;
            var direction = (Vector2)MarkingTarget.position - (Vector2)Camera.main.transform.position;
            transform.up = direction;
        }
        else
        {
            _image.sprite = _pointSprite;
            transform.up = Vector2.up;
        }
    }
}

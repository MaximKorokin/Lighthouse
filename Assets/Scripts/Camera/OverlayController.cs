using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class OverlayController : MonoBehaviour
{
    private Image _image;

    public Sprite Sprite { get => _image.sprite; set => _image.sprite = value; }
    public Color Color { get => _image.color; set => _image.color = value; }

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
}

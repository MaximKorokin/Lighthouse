using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageSwitch : MonoBehaviour
{
    [SerializeField]
    private Sprite _sprite1;
    [SerializeField]
    private Sprite _sprite2;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = _sprite1;
    }

    public void Switch()
    {
        _image.sprite = _image.sprite == _sprite1 ? _sprite2 : _sprite1;
    }

    public void SetValue(bool val)
    {
        _image.sprite = val ? _sprite2 : _sprite1;
    }
}

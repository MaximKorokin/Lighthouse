using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageSwitchBindable : Bindable
{
    [SerializeField]
    private Sprite _sprite1;
    [SerializeField]
    private Sprite _sprite2;

    private Image _image;

    protected override void Awake()
    {
        base.Awake();
        _image = GetComponent<Image>();
        _image.sprite = _sprite1;
    }

    protected override void OnValueChanged(object value)
    {
        SetValue(Convert.ToBoolean(value));
    }

    private void SetValue(bool val)
    {
        _image.sprite = val ? _sprite2 : _sprite1;
    }
}

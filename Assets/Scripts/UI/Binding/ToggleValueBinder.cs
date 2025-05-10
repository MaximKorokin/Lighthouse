using UnityEngine;
using UnityEngine.UI;

public class ToggleValueBinder : ConfigValueBinder<bool>
{
    [SerializeField]
    private Sprite _onSprite;
    [SerializeField]
    private Sprite _offSprite;

    private Image _image;
    private Toggle _toggle;

    private void Awake()
    {
        _toggle = this.GetRequiredComponent<Toggle>();
        _image = this.GetRequiredComponent<Image>();
        _image.sprite = _onSprite;
    }

    public override bool GetCurrentValue()
    {
        return _toggle.isOn;
    }

    public override void SetValue(bool value)
    {
        _toggle.isOn = value;
        _image.sprite = value ? _onSprite : _offSprite;
    }

    public override bool ConvertToValue(string str)
    {
        return ConvertingUtils.ToBool(str);
    }
}

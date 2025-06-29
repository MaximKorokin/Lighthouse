using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectView : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TextLocalizer _title;
    [SerializeField]
    private TextLocalizer _description;

    private EffectPreview _effectPreview;

    public event Action<EffectView, EffectPreview> Clicked;

    public void Initialize(EffectPreview effect)
    {
        _effectPreview = effect;
        _image.sprite = effect.Sprite;
        _title.SetText(effect.Name);
        _description.SetText(effect.Description);
    }

    public void InvokeClicked()
    {
        Clicked?.Invoke(this, _effectPreview);
    }
}

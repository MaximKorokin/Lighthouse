using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectView : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TMP_Text _title;
    [SerializeField]
    private TMP_Text _description;

    private EffectPreview _effectPreview;

    public event Action<EffectView, EffectPreview> Clicked;

    public void Initialize(EffectPreview effect)
    {
        _effectPreview = effect;
        _image.sprite = effect.Sprite;
        LocalizationManager.SetLanguageChangeListener(_title, effect.Name, text => _title.text = text);
        LocalizationManager.SetLanguageChangeListener(_description, effect.Description, text => _description.text = text);
    }

    public void InvokeClicked()
    {
        Clicked?.Invoke(this, _effectPreview);
    }
}

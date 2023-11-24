using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EffectView : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TMP_Text _title;
    [SerializeField]
    private TMP_Text _description;

    private Effect _effect;

    public event Action<EffectView, Effect> Clicked;

    public void Initialize(Effect effect)
    {
        _effect = effect;
        _image.sprite = effect.Sprite;
        _title.text = effect.name;
        _description.text = effect.Description;
    }

    public void InvokeClicked()
    {
        Clicked?.Invoke(this, _effect);
    }
}

using System;
using TMPro;
using UnityEngine;

public class OverlayTextPool : ObjectsPool<InformationTextViewer, OverlayTextSettings>
{
    protected override void Initialize(InformationTextViewer viewer, OverlayTextSettings settings)
    {
        viewer.transform.SetParent(OverlaysParent.Instance.transform);
        viewer.transform.SetAsLastSibling();

        var rectTransform = viewer.transform as RectTransform;
        rectTransform.transform.localPosition = Vector2.zero;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector3.one;
        rectTransform.sizeDelta = Vector3.one;
        rectTransform.localScale = Vector3.one;

        viewer.gameObject.SetActive(true);

        var text = viewer.GetComponent<TMP_Text>();
        text.color = settings.Color;
        text.fontSize = settings.FontSize;
        text.font = settings.Font;
        text.fontMaterial = settings.Material != null ? settings.Material : settings.Font.material;

        viewer.SetTypingSound(settings.TypingSound);
        viewer.ViewText(settings.Text, settings.ShowTime, settings.TypingSpeed);
    }

    protected override void Deinitialize(InformationTextViewer viewer)
    {
        viewer.SetTypingSound(null);
    }
}

[Serializable]
public struct OverlayTextSettings
{
    public string Text;
    public Color Color;
    public float FontSize;
    public TMP_FontAsset Font;
    public Material Material;
    [Space]
    public AudioClip TypingSound;
    public TypingSpeed TypingSpeed;
    public float ShowTime;
}

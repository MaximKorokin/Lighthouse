using System;
using UnityEngine;

class OverlayPool : ObjectsPool<OverlayController, OverlayParameter>
{
    protected override void Initialize(OverlayController overlay, OverlayParameter parameter)
    {
        overlay.Sprite = parameter.Sprite;
        overlay.Color = parameter.Color;
        
        overlay.transform.SetParent(OverlaysParent.Instance.transform);

        var rectTransform = overlay.transform as RectTransform;
        rectTransform.transform.localPosition = Vector2.zero;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector3.one;
        rectTransform.sizeDelta = Vector3.one;
        rectTransform.localScale = Vector3.one;

        overlay.gameObject.SetActive(true);
    }

    protected override void Deinitialize(OverlayController overlay)
    {

    }
}

[Serializable]
public struct OverlayParameter
{
    public Sprite Sprite;
    public Color Color;
}

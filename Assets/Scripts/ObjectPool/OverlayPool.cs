using UnityEngine;

class OverlayPool : ObjectsPool<OverlayController, OverlaySettings>
{
    protected override void Initialize(OverlayController overlay, OverlaySettings parameter)
    {
        overlay.SetSettings(parameter);

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

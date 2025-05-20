using UnityEngine;
using UnityEngine.UI;

class OverlayPool : ObjectsPool<OverlayController, OverlaySettings>
{
    protected override void Initialize(OverlayController overlay, OverlaySettings settings)
    {
        overlay.SetSettings(settings);

        overlay.transform.SetParent(OverlaysParent.Instance.transform);
        if (settings.IsHierarchyPriority)
        {
            overlay.transform.SetAsLastSibling();
        }
        else
        {
            overlay.transform.SetAsFirstSibling();
        }

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
        if (overlay.TryGetComponent<Image>(out var image))
        {
            image.color = new(0, 0, 0, 1);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public static class CopyToExtensions
{
    public static void CopyTo(this RectTransform source, RectTransform target)
    {
        if (source == null || target == null)
        {
            return;
        }

        target.localPosition = source.localPosition;
        target.localScale = source.localScale;
        target.sizeDelta = source.sizeDelta;
        target.anchorMin = source.anchorMin;
        target.anchorMax = source.anchorMax;
        target.anchoredPosition = source.anchoredPosition;
        target.pivot = source.pivot;
    }

    public static void CopyTo(this Image source, Image target)
    {
        if (source == null || target == null)
        {
            return;
        }
        target.sprite = source.sprite;
        target.color = source.color;
        target.type = source.type;

        target.fillMethod = source.fillMethod;
        target.fillAmount = source.fillAmount;
        target.fillOrigin = source.fillOrigin;
        target.fillClockwise = source.fillClockwise;
        target.fillCenter = source.fillCenter;
    }
}

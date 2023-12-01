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
        target.localScale = source.localScale;
        target.offsetMin = source.offsetMin;
        target.offsetMax = source.offsetMax;
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

        target.fillAmount = source.fillAmount;
        target.fillOrigin = source.fillOrigin;
        target.fillMethod = source.fillMethod;
        target.fillClockwise = source.fillClockwise;
        target.fillCenter = source.fillCenter;
    }
}

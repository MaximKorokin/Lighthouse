using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public static class CopyToExtensions
{
    /// <summary>
    /// Tries to find a suitable method called CopyTo in this class and invokes it
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public static bool TryCopyTo<T>(this T source, T target) where T : Component
    {
        var sourceType = source.GetType();
        var method = typeof(CopyToExtensions)
            .GetMethods(BindingFlags.Static | BindingFlags.Public)
            .Where(x => x.Name == nameof(CopyTo))
            .FirstOrDefault(x => x.GetParameters()[0].ParameterType == sourceType);

        if (method == null) return false;

        method.Invoke(source, new object[] { source, target });

        return true;
    }

    private static bool AreValid<T>(T source, T target)
    {
        if (source == null || target == null)
        {
            return false;
        }
        return true;
    }

    public static void CopyTo(this RectTransform source, RectTransform target)
    {
        if (!AreValid(source, target))
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
        if (!AreValid(source, target))
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

    public static void CopyTo(this Animator source, Animator target)
    {
        if (!AreValid(source, target))
        {
            return;
        }

        target.runtimeAnimatorController = source.runtimeAnimatorController;
    }

    public static void CopyTo(this SpriteRenderer source, SpriteRenderer target)
    {
        if (!AreValid(source, target))
        {
            return;
        }

        target.sprite = source.sprite;
        target.color = source.color;
        target.flipX = source.flipX;
    }
}

using UnityEngine;
using UnityEngine.UI;

public static class CopyToExtensions
{
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

    public static void CopyTo(this WorldObject source, WorldObject target)
    {
        if (!AreValid(source, target))
        {
            return;
        }

        target.PositioningType = source.PositioningType;
        target.TriggeringType = source.TriggeringType;
        target.SetFaction(source.Faction);
        target.Stats.Modify(source.Stats, StatsModificationType.Assign);

        // Collider2D[]
        // Sprite
        // Animator
    }

    public static void CopyTo(this DestroyableWorldObject source, DestroyableWorldObject target)
    {
        (source as WorldObject).CopyTo(target);

        target.IsDamagable = source.IsDamagable;
        target.DestroyTime = source.DestroyTime;
    }

    public static void CopyTo(this MovableWorldObject source, MovableWorldObject target)
    {
        (source as DestroyableWorldObject).CopyTo(target);

        target.CanFlip = source.CanFlip;
        target.CanRotate = source.CanRotate;

        // Rigidbody2D
    }

    public static void CopyTo(this Item source, Item target)
    {
        (source as MovableWorldObject).CopyTo(target);

        target.InactiveTime = source.InactiveTime;
    }

    public static void CopyTo(this TemporaryWorldObject source, TemporaryWorldObject target)
    {
        (source as MovableWorldObject).CopyTo(target);

        target.LifeTime = source.LifeTime;
    }
}

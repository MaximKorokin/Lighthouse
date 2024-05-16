using UnityEditor;
using UnityEngine;

public static class EditorUtils
{
    public static Color DrawingColor
    {
        set
        {
            Gizmos.color = value;
            Handles.color = value;
        }
    }

    public static void DrawArc(Vector2 start, Vector2 end, float radius, bool withArrow)
    {
        var orientation = (end - start).x > 0 ? 1 : -1;
        DrawArcRecursive(start, end, radius, withArrow, orientation, 6);
    }

    private static void DrawArcRecursive(Vector2 start, Vector2 end, float radius, bool withArrow, int orientation, int deepness)
    {
        if (deepness-- <= 0)
        {
            Gizmos.DrawLine(start, end);
            return;
        }
        var middleDir = (end - start) / 2;
        var middlePoint = start + middleDir + middleDir.Rotate(90 * orientation).normalized * radius;

        if (withArrow)
        {
            Gizmos.DrawLine(middlePoint, middlePoint - (middleDir.normalized * 0.2f).Rotate(25));
            Gizmos.DrawLine(middlePoint, middlePoint - (middleDir.normalized * 0.2f).Rotate(-25));
        }

        DrawArcRecursive(start, middlePoint, radius / 4f, false, orientation, deepness);
        DrawArcRecursive(middlePoint, end, radius / 4f, false, orientation, deepness);
    }

    public static void DrawArrow(Vector2 start, Vector2 end, float endIndent, bool isCircleArrow)
    {
        var direction = (end - start).normalized;
        var toVector = end - direction * endIndent;
        Gizmos.DrawLine(start, toVector);
        if (isCircleArrow)
        {
            Handles.DrawWireDisc(toVector, Vector3.forward, 0.1f);
        }
        else
        {
            Gizmos.DrawLine(toVector, toVector - (direction * 0.2f).Rotate(10));
            Gizmos.DrawLine(toVector, toVector - (direction * 0.2f).Rotate(-10));
        }
    }

    public static void HorizontallyAlignIcons(this IEditorIcon[] icons, Vector3 center, int rowNumber)
    {
        var iconSize = 3.2f * Gizmos.probeSize;
        icons?.ForEach((x, i) =>
        {
            var verticalPosition = rowNumber * iconSize / 2 * Vector3.up;
            var horizontalPosition = (i - icons.Length / 2f) * iconSize * Vector3.right + iconSize / 2 * Vector3.right;
            var position = center + verticalPosition + horizontalPosition;
            Gizmos.DrawIcon(position, x.IconName, true, x.IconColor);
        });
    }

    /// <summary>
    /// It is not affected by value of <see cref="DrawingColor"/>
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="iconName"></param>
    public static void DrawArrowWithIcon(Vector3 start, Vector3 end, string iconName)
    {
        DrawingColor = MyColors.Gray;
        DrawArrow(start, end, 0, true);
        Gizmos.DrawIcon((start + end) / 2, iconName, true, MyColors.LightGray);
    }
}

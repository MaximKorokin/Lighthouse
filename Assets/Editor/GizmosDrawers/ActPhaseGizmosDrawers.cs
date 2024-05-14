using UnityEditor;
using UnityEngine;

public static class ActPhaseGizmosDrawers
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void CameraMovePhase(CameraMovePhase phase, GizmoType gizmoType)
    {
        if (phase.TransformPosition == null)
        {
            return;
        }
        EditorUtils.DrawingColor = MyColors.Gray;
        EditorUtils.DrawArrow(phase.transform.position, phase.TransformPosition.position, 0, true);
        Gizmos.DrawIcon((phase.transform.position + phase.TransformPosition.position) / 2, phase.IconName, true, MyColors.LightGray);
    }
}

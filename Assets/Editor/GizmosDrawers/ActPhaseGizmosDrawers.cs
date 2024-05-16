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
        EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.TransformPosition.position, phase.IconName);
    }
}

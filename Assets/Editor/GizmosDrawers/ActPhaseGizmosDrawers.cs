﻿using System.Linq;
using UnityEditor;

public static class ActPhaseGizmosDrawers
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void CameraMovePhase(CameraMovePhase phase, GizmoType gizmoType)
    {
        if (phase.TransformPosition == null)
        {
            return;
        }
        EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.TransformPosition.position, ArrowType.Circle, phase.IconName);
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void ActorActionPhase(ActorsActingPhase phase, GizmoType gizmoType)
    {
        if (phase.Actors == null || !phase.Actors.Any())
        {
            return;
        }
        phase.Actors.ForEach(x => EditorUtils.DrawArrowWithIcon(phase.transform.position, x.transform.position, ArrowType.Circle, phase.IconName));
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void FactionChangePhase(FactionChangingPhase phase, GizmoType gizmoType)
    {
        phase.WorldObjects
            .Where(x => x != null)
            .ForEach(x => EditorUtils.DrawArrowWithIcon(phase.transform.position, x.transform.position, ArrowType.Line, phase.IconName));
    }
}

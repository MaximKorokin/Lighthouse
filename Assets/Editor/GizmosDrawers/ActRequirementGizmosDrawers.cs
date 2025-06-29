﻿using UnityEditor;

public static class ActRequirementGizmosDrawers
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void ActEndRequirement(ActFinishRequirement requirement, GizmoType gizmoType)
    {
        if (requirement.ScenarioAct == null)
        {
            return;
        }

        EditorUtils.DrawArrowWithIcon(requirement.transform.position, requirement.ScenarioAct.transform.position, ArrowType.Arrow, requirement.IconName);
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void ActorActedRequirement(ActorActedRequirement requirement, GizmoType gizmoType)
    {
        if (requirement.Actor == null)
        {
            return;
        }
        EditorUtils.DrawArrowWithIcon(requirement.transform.position, requirement.Actor.transform.position, ArrowType.Line, requirement.IconName);
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void ActorActedRequirement(WorldObjectsDestroyRequirement requirement, GizmoType gizmoType)
    {
        if (requirement.WorldObjects == null)
        {
            return;
        }

        foreach (var worldObject in requirement.WorldObjects)
        {
            if (worldObject == null) continue;
            EditorUtils.DrawArrowWithIcon(requirement.transform.position, worldObject.transform.position, ArrowType.Line, requirement.IconName);
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void ManualInteractionControllerRequirement(ManualInteractionControllerRequirement requirement, GizmoType gizmoType)
    {
        if (requirement.Controller != null)
        {
            EditorUtils.DrawArrowWithIcon(requirement.transform.position, requirement.Controller.transform.position, ArrowType.Line, requirement.IconName);
        }
    }
}

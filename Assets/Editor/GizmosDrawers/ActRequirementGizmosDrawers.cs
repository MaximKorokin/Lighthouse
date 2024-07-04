using UnityEditor;
using UnityEngine;

public static class ActRequirementGizmosDrawers
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void ActEndRequirement(ActFinishRequirement requirement, GizmoType gizmoType)
    {
        if (requirement.ScenarioAct == null)
        {
            return;
        }

        EditorUtils.DrawArrowWithIcon(requirement.transform.position, requirement.ScenarioAct.transform.position, ArrowType.Arrow, requirement.IconName);
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void ActorActedRequirement(ActorActedRequirement requirement, GizmoType gizmoType)
    {
        if (requirement.Actor == null)
        {
            return;
        }
        EditorUtils.DrawArrowWithIcon(requirement.transform.position, requirement.Actor.transform.position, ArrowType.Line, requirement.IconName);
    }
}

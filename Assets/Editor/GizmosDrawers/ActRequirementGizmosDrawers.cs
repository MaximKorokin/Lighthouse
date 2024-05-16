using UnityEditor;
using UnityEngine;

public static class ActRequirementGizmosDrawers
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void ActEndRequirement(ActEndRequirement requirement, GizmoType gizmoType)
    {
        if (requirement.ScenarioAct == null)
        {
            return;
        }

        EditorUtils.DrawingColor = Color.white;
        EditorUtils.DrawArc(
            requirement.ScenarioAct.transform.position,
            requirement.transform.position,
            (requirement.ScenarioAct.transform.position - requirement.transform.position).magnitude * 0.1f,
            true);
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void ActorActedRequirement(ActorActedRequirement requirement, GizmoType gizmoType)
    {
        if (requirement.Actor == null)
        {
            return;
        }
        EditorUtils.DrawArrowWithIcon(requirement.transform.position, requirement.Actor.transform.position, requirement.IconName);
    }
}

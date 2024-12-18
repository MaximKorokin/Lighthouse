using UnityEditor;

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
        foreach (var worldObject in requirement.WorldObjects)
        {
            EditorUtils.DrawArrowWithIcon(requirement.transform.position, worldObject.transform.position, ArrowType.Line, requirement.IconName);
        }
    }
}

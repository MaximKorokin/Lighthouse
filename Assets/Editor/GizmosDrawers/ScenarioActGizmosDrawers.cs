using System.Linq;
using UnityEditor;

public static class ScenarioActGizmosDrawers
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void ScenarioAct(ScenarioAct act, GizmoType gizmoType)
    {
        act.Requirements?.Where(x => !string.IsNullOrWhiteSpace(x.IconName)).ToArray().HorizontallyAlignIcons(act.transform.position, 1);
        act.Phases?.Where(x => !string.IsNullOrWhiteSpace(x.IconName)).ToArray().HorizontallyAlignIcons(act.transform.position, -1);
    }
}

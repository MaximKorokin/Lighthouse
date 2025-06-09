using System.Linq;
using UnityEditor;
using UnityEngine;

public static class ScenarioActGizmosDrawers
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void DrawScenarioAct(ScenarioAct act, GizmoType gizmoType)
    {
        if (Selection.activeGameObject == null || Selection.activeGameObject.GetComponent<ScenarioAct>() == null) return;

        act.Requirements?.Where(x => !string.IsNullOrWhiteSpace(x.IconName)).ToArray().HorizontallyAlignIcons(act.transform.position, 1);
        act.Phases?.Where(x => !string.IsNullOrWhiteSpace(x.IconName)).ToArray().HorizontallyAlignIcons(act.transform.position, -1);

        EditorUtils.DrawingColor = Color.white;
        foreach (var childAct in act.ChildrenActs)
        {
            EditorUtils.DrawArc(
                act.transform.position,
                childAct.transform.position,
                (childAct.transform.position - act.transform.position).magnitude * 0.1f,
                true);
        }
    }
}

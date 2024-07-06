using System.Linq;
using UnityEditor;

public static class ActPhaseGizmosDrawers
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void CameraMovePhase(CameraMovePhase phase, GizmoType gizmoType)
    {
        if (phase.TransformPosition != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.TransformPosition.position, ArrowType.Circle, phase.IconName);
        }
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
        phase.WorldObjects?.Where(x => x != null)
            .ForEach(x => EditorUtils.DrawArrowWithIcon(phase.transform.position, x.transform.position, ArrowType.Line, phase.IconName));
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void SpawningPhase(SpawningPhase phase, GizmoType gizmoType)
    {
        if (phase.TransformPosition != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.TransformPosition.position, ArrowType.Circle, phase.IconName);
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void MovableMovePhase(MovableMovePhase phase, GizmoType gizmoType)
    {
        if (phase.Movable != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.Movable.transform.position, ArrowType.Line, phase.IconName);
        }
        if (phase.TransformPosition != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.TransformPosition.position, ArrowType.Circle, phase.IconName);
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void EffectAct(EffectPhase phase, GizmoType gizmoType)
    {
        if (phase.WorldObject != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.WorldObject.transform.position, ArrowType.Line, phase.IconName);
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy | GizmoType.Active)]
    public static void GameObjectSetActivePhase(GameObjectSetActivePhase phase, GizmoType gizmoType)
    {
        if (phase.GameObject != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.GameObject.transform.position, ArrowType.Line, phase.IconName);
        }
    }
}

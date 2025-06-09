using System.Linq;
using UnityEditor;
using UnityEngine;

public static class ActPhaseGizmosDrawers
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void CameraMovePhase(CameraMovePhase phase, GizmoType gizmoType)
    {
        if (phase.TransformPosition != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.TransformPosition.position, ArrowType.Circle, phase.IconName);
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void ActorActionPhase(ActorsActingPhase phase, GizmoType gizmoType)
    {
        if (phase.Actors == null || !phase.Actors.Any())
        {
            return;
        }
        phase.Actors.ForEach(x => EditorUtils.DrawArrowWithIcon(phase.transform.position, x.transform.position, ArrowType.Circle, phase.IconName));
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void FactionChangePhase(FactionChangingPhase phase, GizmoType gizmoType)
    {
        phase.WorldObjects?.Where(x => x != null)
            .ForEach(x => EditorUtils.DrawArrowWithIcon(phase.transform.position, x.transform.position, ArrowType.Line, phase.IconName));
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void SpawningPhase(SpawningPhase phase, GizmoType gizmoType)
    {
        if (phase.TransformPosition != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.TransformPosition.position, ArrowType.Circle, phase.IconName);
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void MovableMovePhase(MovableMovePhase phase, GizmoType gizmoType)
    {
        if (phase.Movable == null) return;

        EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.Movable.transform.position, ArrowType.Line, phase.IconName);
        if (phase.TransformPositions != null)
        {
            var first = phase.Movable.transform.position;
            Vector2 second;
            foreach (var transformPosition in phase.TransformPositions)
            {
                second = transformPosition.position;
                EditorUtils.DrawArrowWithIcon(first, second, ArrowType.Arrow, phase.IconName);
                first = second;
            }
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void EffectAct(EffectPhase phase, GizmoType gizmoType)
    {
        if (phase.WorldObject != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.WorldObject.transform.position, ArrowType.Line, phase.IconName);
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void GameObjectSetActivePhase(GameObjectSetActivePhase phase, GizmoType gizmoType)
    {
        if (phase.GameObject != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.GameObject.transform.position, ArrowType.Line, phase.IconName);
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void SpeechBubblePhase(SpeechBubblePhase phase, GizmoType gizmoType)
    {
        if (phase.CanvasProvider != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.CanvasProvider.transform.position, ArrowType.Line, phase.IconName);
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void PermissionPhase(PermissionPhase phase, GizmoType gizmoType)
    {
        if (phase.PermissionRequirement != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.PermissionRequirement.transform.position, ArrowType.Line, phase.IconName);
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.Active)]
    public static void SetPositionPhase(SetPositionPhase phase, GizmoType gizmoType)
    {
        if (phase.TransformPosition != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.TransformPosition.transform.position, ArrowType.Line, phase.IconName);
        }
        if (phase.TargetTransform != null)
        {
            EditorUtils.DrawArrowWithIcon(phase.transform.position, phase.TargetTransform.transform.position, ArrowType.Arrow, phase.IconName);
        }
    }
}

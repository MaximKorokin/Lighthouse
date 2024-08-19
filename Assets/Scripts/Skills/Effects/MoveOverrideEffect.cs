using System.Collections;
using UnityEngine;

public abstract class MoveOverrideEffect : ControllerOverrideEffect
{
    [SerializeField]
    protected float Speed;

    protected override void StartOverride(CastState castState)
    {
        if (castState.GetTarget() is not MovableWorldObject movable)
        {
            return;
        }
        base.StartOverride(castState);
        movable.Stop();
    }

    protected override IEnumerator ControllerOverrideCoroutine(CastState castState)
    {
        var moveTarget = castState.GetTarget();
        if (moveTarget is not MovableWorldObject movable)
        {
            yield break;
        }

        var direction = GetDirection(castState);
        var endTime = Time.time + OverrideTime;
        while (Time.time < endTime)
        {
            movable.Direction = direction;
            movable.Move(Speed);
            yield return new WaitForFixedUpdate();
        }
    }

    protected abstract Vector2 GetDirection(CastState castState);
}

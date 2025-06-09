using System.Collections;
using UnityEngine;

public abstract class MoveOverrideEffect : ControllerOverrideEffect
{
    [SerializeField]
    protected float Speed;

    protected override void StartOverride(CastState castState)
    {
        var movable = castState.GetMovableTarget();
        if (movable == null)
        {
            return;
        }
        base.StartOverride(castState);
        movable.Stop();
    }

    protected override IEnumerator ControllerOverrideCoroutine(CastState castState)
    {
        var movable = castState.GetMovableTarget();
        if (movable == null)
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

    protected override void StopOverride(CastState castState)
    {
        base.StopOverride(castState);
        var movable = castState.GetMovableTarget();
        if (movable != null)
        {
            movable.Stop();
        }
    }

    protected abstract Vector2 GetDirection(CastState castState);
}

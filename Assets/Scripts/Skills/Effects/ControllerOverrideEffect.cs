using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerOverrideEffect : Effect
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _time;

    protected readonly Dictionary<WorldObject, (Coroutine Coroutine, bool CanControl)> ActiveOverrides = new();

    public override void Invoke(CastState castState)
    {
        if (castState.Target == null || castState.Source == null)
        {
            return;
        }
        StartOverride(castState);
    }

    protected abstract Vector2 GetDirection(CastState castState);

    protected virtual void StartOverride(CastState castState)
    {
        var moveTarget = castState.GetTarget();
        if (moveTarget is not MovableWorldObject movable)
        {
            return;
        }
        if (ActiveOverrides.ContainsKey(moveTarget))
        {
            StopOverride(moveTarget);
        }

        var controller = movable.GetComponent<ControllerBase>();
        var initalCanControl = controller.CanControl;
        controller.CanControl = false;
        movable.Stop();

        var coroutine = moveTarget.StartCoroutine(MoveOverrideCoroutine(castState));
        ActiveOverrides[moveTarget] = (coroutine, initalCanControl);
    }

    protected virtual void StopOverride(WorldObject worldObject)
    {
        var activeCoroutine = ActiveOverrides[worldObject];
        if (activeCoroutine.Coroutine != null)
        {
            worldObject.StopCoroutine(activeCoroutine.Coroutine);
        }
        worldObject.GetComponent<ControllerBase>().CanControl = activeCoroutine.CanControl;
        ActiveOverrides.Remove(worldObject);
    }

    private IEnumerator MoveOverrideCoroutine(CastState castState)
    {
        var moveTarget = castState.GetTarget();
        if (moveTarget is not MovableWorldObject movable)
        {
            yield break;
        }

        var direction = GetDirection(castState);
        var endTime = Time.time + _time;
        while (Time.time < endTime)
        {
            movable.Direction = direction;
            movable.Move(_speed);
            yield return new WaitForFixedUpdate();
        }
        StopOverride(moveTarget);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveOverrideEffect : Effect
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _time;
    [SerializeField]
    private bool _disableActor;

    private readonly Dictionary<WorldObject, (Coroutine Coroutine, bool CanAct, bool CanMove)> ActiveOverrides = new();

    public override void Invoke(CastState castState)
    {
        if (castState.Target == null || castState.Source == null)
        {
            return;
        }
        StartOverride(castState);
    }

    protected abstract Vector2 GetDirection(CastState castState);
    protected abstract WorldObject GetMoveTarget(CastState castState);

    protected virtual void StartOverride(CastState castState)
    {
        var moveTarget = GetMoveTarget(castState);
        if (ActiveOverrides.ContainsKey(moveTarget))
        {
            StopOverride(moveTarget);
        }
        if (moveTarget is not MovableWorldObject movable)
        {
            return;
        }

        var initalCanMove = movable.CanMove;
        movable.CanMove = false;
        movable.Stop();

        var actor = movable.GetComponent<ActorBase>();
        var initalCanAct = actor.CanAct;
        if (_disableActor)
        {
            actor.CanAct = false;
        }

        var coroutine = moveTarget.StartCoroutine(MoveOverrideCoroutine(castState));
        ActiveOverrides[moveTarget] = (coroutine, initalCanMove, initalCanAct);
    }

    protected virtual void StopOverride(WorldObject worldObject)
    {
        var activeCoroutine = ActiveOverrides[worldObject];
        if (activeCoroutine.Coroutine != null)
        {
            worldObject.StopCoroutine(activeCoroutine.Coroutine);
        }
        (worldObject as MovableWorldObject).CanMove = activeCoroutine.CanMove;
        worldObject.GetComponent<ActorBase>().CanAct = activeCoroutine.CanAct;
        ActiveOverrides.Remove(worldObject);
    }

    private IEnumerator MoveOverrideCoroutine(CastState castState)
    {
        var moveTarget = GetMoveTarget(castState);
        if (moveTarget is not MovableWorldObject movable)
        {
            yield break;
        }

        var direction = GetDirection(castState);
        var endTime = Time.time + _time;
        while (Time.time < endTime)
        {
            movable.Direction = direction;
            movable.MoveRigidbody(_speed);
            yield return new WaitForFixedUpdate();
        }
        StopOverride(moveTarget);
    }
}

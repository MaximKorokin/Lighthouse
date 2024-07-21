using System.Collections;
using UnityEngine;

public abstract class ControllerOverrideEffect : Effect
{
    [SerializeField]
    protected float OverrideTime;

    public override void Invoke(CastState castState)
    {
        StartOverride(castState);
    }

    protected virtual void StartOverride(CastState castState)
    {
        var target = castState.GetTarget();
        target.GetComponent<ControllerBase>().CanControl = false;
        target.StartCoroutineSafe(ControllerOverrideCoroutine(castState), () => StopOverride(target));
    }

    protected virtual void StopOverride(WorldObject worldObject)
    {
        worldObject.GetComponent<ControllerBase>().CanControl = true;
    }

    protected abstract IEnumerator ControllerOverrideCoroutine(CastState castState);
}

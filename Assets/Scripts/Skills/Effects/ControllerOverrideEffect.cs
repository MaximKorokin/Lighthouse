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
        var controller = castState.GetTargetController();
        if (controller != null)
        {
            controller.CanControl = false;
        }
        var target = castState.GetTarget();
        target.StartCoroutineSafe(ControllerOverrideCoroutine(castState), () => StopOverride(castState));
    }

    protected virtual void StopOverride(CastState castState)
    {
        var controller = castState.GetTargetController();
        if (controller != null)
        {
            controller.CanControl = true;
        }
    }

    protected abstract IEnumerator ControllerOverrideCoroutine(CastState castState);
}

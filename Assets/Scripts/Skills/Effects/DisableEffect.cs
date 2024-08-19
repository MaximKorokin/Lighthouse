using System.Collections;
using UnityEngine;

public class DisableEffect : ControllerOverrideEffect
{
    protected override IEnumerator ControllerOverrideCoroutine(CastState castState)
    {
        if (castState.GetTarget() is MovableWorldObject movable)
        {
            movable.Stop();
        }
        yield return new WaitForSeconds(OverrideTime);
    }
}

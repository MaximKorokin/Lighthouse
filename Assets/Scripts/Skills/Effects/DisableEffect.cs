using System.Collections;
using UnityEngine;

public class DisableEffect : ControllerOverrideEffect
{
    protected override IEnumerator ControllerOverrideCoroutine(CastState castState)
    {
        yield return new WaitForSeconds(OverrideTime);
    }
}

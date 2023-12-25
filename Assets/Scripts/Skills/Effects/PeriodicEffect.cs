using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class PeriodicEffect : EndingEffect
{
    private const float MinIntervalValue = 0.1f;

    [field: SerializeField]
    public float Duration { get; private set; }
    [field: SerializeField]
    [field: Tooltip("Must be above 0.1 to call intervals")]
    public float Interval { get; private set; }

    protected virtual IEnumerator DurationCoroutine(CastState castState)
    {
        Coroutine periodicInvokationCoroutine = null;
        if (Interval >= MinIntervalValue)
        {
            periodicInvokationCoroutine = castState.Target.StartCoroutine(PeriodicInvokationCoroutine(castState));
        }
        else
        {
            base.Invoke(castState);
        }
        if (Duration > 0)
        {
            yield return new WaitForSeconds(Duration);
        }
        if (periodicInvokationCoroutine != null)
        {
            castState.Target.StopCoroutine(periodicInvokationCoroutine);
        }
        InvokeEnd(castState);
    }

    [SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite", Justification = "Coroutine")]
    protected virtual IEnumerator PeriodicInvokationCoroutine(CastState castState)
    {
        while (true)
        {
            base.Invoke(castState);
            yield return new WaitForSeconds(Interval);
        }
    }

    public override void Invoke(CastState castState)
    {
        castState.Target.StartCoroutine(DurationCoroutine(castState));
    }
}

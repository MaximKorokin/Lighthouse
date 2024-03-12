using System.Collections;
using UnityEngine;

public class PeriodicEffect : EndingEffect
{
    [field: SerializeField]
    public float Duration { get; private set; }
    [field: SerializeField]
    [field: Tooltip("Must be above 0.1 to call intervals")]
    public float Interval { get; private set; }

    public override void Invoke(CastState castState)
    {
        base.Invoke(castState);
        castState.Target.StartCoroutine(PeriodicInvokationCoroutine(castState));
    }

    private IEnumerator PeriodicInvokationCoroutine(CastState castState)
    {
        var startTime = Time.time;
        for (;;)
        {
            yield return new WaitForSeconds(Interval);
            if (startTime + Duration < Time.time + Interval)
            {
                break;
            }
            base.Invoke(castState);
        }
        InvokeEnd(castState);
    }
}

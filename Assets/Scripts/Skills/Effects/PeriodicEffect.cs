using System.Collections;
using UnityEngine;

public class PeriodicEffect : EndingEffect
{
    [field: SerializeField]
    private float PeriodTime { get; set; }
    [field: SerializeField]
    private int Periods { get; set; }

    public override void Invoke(CastState castState)
    {
        castState.GetTarget().StartCoroutineSafe(PeriodicInvokationCoroutine(castState));
    }

    private IEnumerator PeriodicInvokationCoroutine(CastState castState)
    {
        for (int i = 0; i < Periods; i++)
        {
            yield return new WaitForSeconds(PeriodTime);
            base.Invoke(castState);
        }
        InvokeEnd(castState);
    }
}

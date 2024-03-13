using System.Collections;
using UnityEngine;

public class PeriodicEffect : EndingEffect
{
    [field: SerializeField]
    private float Interval { get; set; }
    [field: SerializeField]
    private int Intervals { get; set; }

    public override void Invoke(CastState castState)
    {
        castState.GetTarget().StartCoroutine(PeriodicInvokationCoroutine(castState));
    }

    private IEnumerator PeriodicInvokationCoroutine(CastState castState)
    {
        for (int i = 0; i < Intervals; i++)
        {
            yield return new WaitForSeconds(Interval);
            base.Invoke(castState);
        }
        InvokeEnd(castState);
    }
}

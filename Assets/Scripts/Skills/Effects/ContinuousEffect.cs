using System.Collections;
using UnityEngine;

public class ContinuousEffect : EndingEffect
{
    [field: SerializeField]
    public float Duration { get; private set; }

    public override void Invoke(CastState castState)
    {
        base.Invoke(castState);
        if (Duration > 0)
        {
            castState.GetTarget().StartCoroutine(DurationCoroutine(castState));
        }
        else
        {
            InvokeEnd(castState);
        }
    }

    private IEnumerator DurationCoroutine(CastState castState)
    {
        yield return new WaitForSeconds(Duration);
        InvokeEnd(castState);
    }
}

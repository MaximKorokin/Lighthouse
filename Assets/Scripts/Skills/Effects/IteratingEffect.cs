using System.Collections;
using UnityEngine;

public abstract class IteratingEffect<T> : EndingEffect where T : class
{
    [field: SerializeField]
    protected float Duration { get; set; }

    protected abstract float Interval { get; set; }

    public override void Invoke(CastState castState)
    {
        base.Invoke(castState);
        StartIterating(castState, null);
    }

    protected virtual void StartIterating(CastState castState, T parameter)
    {
        castState.GetTarget().StartCoroutine(IteratingCoroutine(castState, parameter));
    }

    protected virtual void StopIterating(CastState castState, T parameter) { }

    private IEnumerator IteratingCoroutine(CastState castState, T parameter)
    {
        var startTime = Time.time;
        while (true)
        {
            yield return Interval > 0 ? new WaitForSeconds(Interval) : new WaitForEndOfFrame();
            if (startTime + Duration < Time.time)
            {
                break;
            }
            Iterate(castState, parameter);
        }
        InvokeEnd(castState);
        StopIterating(castState, parameter);
    }

    protected abstract void Iterate(CastState castState, T parameter);
}

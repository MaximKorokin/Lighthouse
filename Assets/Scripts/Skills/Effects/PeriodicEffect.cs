using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[CreateAssetMenu(fileName = "PeriodicEffect", menuName = "ScriptableObjects/Effects/PeriodicEffect", order = 1)]
public class PeriodicEffect : ComplexEffect
{
    [field: SerializeField]
    public float Duration { get; private set; }
    [field: SerializeField]
    public float Interval { get; private set; }

    protected virtual IEnumerator DurationCoroutine(CastState castState)
    {
        var periodicInvokationCoroutine = castState.Target.StartCoroutine(PeriodicInvokationCoroutine(castState));
        yield return new WaitForSeconds(Duration);
        castState.Target.StopCoroutine(periodicInvokationCoroutine);
        InvokeEnd(castState);
    }

    [SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite", Justification = "<Pending>")]
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

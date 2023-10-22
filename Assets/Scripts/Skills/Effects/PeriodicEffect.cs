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

    protected virtual IEnumerator DurationCoroutine(WorldObject source, WorldObject target)
    {
        var periodicInvokationCoroutine = target.StartCoroutine(PeriodicInvokationCoroutine(source, target));
        yield return new WaitForSeconds(Duration);
        target.StopCoroutine(periodicInvokationCoroutine);
        InvokeEnd(source, target);
    }

    [SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite", Justification = "<Pending>")]
    protected virtual IEnumerator PeriodicInvokationCoroutine(WorldObject source, WorldObject target)
    {
        while (true)
        {
            base.Invoke(source, target);
            yield return new WaitForSeconds(Interval);
        }
    }

    public override void Invoke(WorldObject source, WorldObject target)
    {
        target.StartCoroutine(DurationCoroutine(source, target));
    }
}

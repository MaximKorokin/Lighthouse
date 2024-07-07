using System.Collections;
using UnityEngine;

public class WaitingPhase : SkippableActPhase
{
    [SerializeField]
    private float _waitTime;

    public override void Invoke()
    {
        base.Invoke();
        StartCoroutine(WaitCoroutine());
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(_waitTime);
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        InvokeFinished();
    }

    public override string IconName => "Wait.png";
}

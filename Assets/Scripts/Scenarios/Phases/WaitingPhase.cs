using System.Collections;
using UnityEngine;

public class WaitingPhase : ActPhase
{
    [SerializeField]
    private float _waitTime;

    public override void Invoke()
    {
        StartCoroutine(WaitCoroutine());
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(_waitTime);
        InvokeFinished();
    }

    public override string IconName => "Wait.png";
}

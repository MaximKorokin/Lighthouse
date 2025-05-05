using UnityEngine;

public class WaitingPhase : SkippableActPhase
{
    [SerializeField]
    private float _waitTime;

    public override void Invoke()
    {
        base.Invoke();
        CoroutinesHandler.StartUniqueCoroutine(this, CoroutinesUtils.WaitForSeconds(_waitTime), InvokeFinished);
    }

    protected override void OnSkipped()
    {
        InvokeFinished();
    }

    public override string IconName => "Wait.png";
}

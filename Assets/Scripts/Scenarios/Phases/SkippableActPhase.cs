using UnityEngine;

public abstract class SkippableActPhase : ActPhase
{
    private readonly CooldownCounter _skipCooldownCounter = new(.1f);

    private bool _isInvoking;

    public override void Invoke()
    {
        _isInvoking = true;
        _skipCooldownCounter.Reset();
    }

    protected override void InvokeFinished()
    {
        _isInvoking = false;
        base.InvokeFinished();
    }

    private void Update()
    {
        if (_isInvoking && _skipCooldownCounter.IsOver() && InputReader.SkipInputRecieved.HasOccured)
        {
            OnSkipped();
        }
    }

    protected abstract void OnSkipped();
}

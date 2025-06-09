public abstract class SkippableActPhase : ActPhase
{
    private readonly CooldownCounter _skipCooldownCounter = new(0.1f);

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
        if (_isInvoking && InputReader.SkipInputRecieved.HasOccured && _skipCooldownCounter.TryReset())
        {
            OnSkipped();
        }
    }

    protected abstract void OnSkipped();
}

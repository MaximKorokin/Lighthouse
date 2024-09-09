using UnityEngine;

public class OverlayPhase : SkippableActPhase
{
    [SerializeField]
    private OverlaySettings _settings;

    private OverlayController _overlay;

    public override void Invoke()
    {
        base.Invoke();
        _overlay = OverlayPool.Take(_settings);
        _overlay.SetSettings(_settings);
        _overlay.AnimatorController.PlayAnimation(true);
        _overlay.AnimatorController.FinishedPlaying -= OnFinishedPlaying;
        _overlay.AnimatorController.FinishedPlaying += OnFinishedPlaying;
    }

    private void OnFinishedPlaying()
    {
        _overlay.AnimatorController.FinishedPlaying -= OnFinishedPlaying;
        OverlayPool.Return(_overlay);
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        _overlay.AnimatorController.StopAnimation();
    }

    public override string IconName => "Overlay.png";
}

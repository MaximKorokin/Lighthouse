using UnityEngine;

public class OverlayTextPhase : SkippableActPhase
{
    [SerializeField]
    private OverlayTextSettings _settings;

    private InformationTextViewer _viewer;

    public override void Invoke()
    {
        base.Invoke();
        _viewer = OverlayTextPool.Take(_settings);
        _viewer.ViewFinished -= OnFinishedPlaying;
        _viewer.ViewFinished += OnFinishedPlaying;
    }

    private void OnFinishedPlaying()
    {
        _viewer.ViewFinished -= OnFinishedPlaying;
        OverlayTextPool.Return(_viewer);
        InvokeFinished();
    }

    protected override void OnSkipped()
    {
        _viewer.FinishViewText();
    }

    public override string IconName => "Info.png";
}

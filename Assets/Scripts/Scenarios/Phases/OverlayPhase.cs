using UnityEngine;

public class OverlayPhase : ActPhase
{
    [SerializeField]
    private OverlaySettings _settings;

    private OverlayController _overlay;

    public override void Invoke()
    {
        _overlay = OverlayPool.Take(_settings);
        _overlay.SetSettings(_settings);
        _overlay.AnimatorController.PlayAnimation(true);
        _overlay.AnimatorController.EndedPlaying -= OnEndedPlaying;
        _overlay.AnimatorController.EndedPlaying += OnEndedPlaying;
    }

    private void OnEndedPlaying()
    {
        _overlay.AnimatorController.EndedPlaying -= OnEndedPlaying;
        OverlayPool.Return(_overlay);
        InvokeEnded();
    }

    public override string IconName => "Overlay.png";
}

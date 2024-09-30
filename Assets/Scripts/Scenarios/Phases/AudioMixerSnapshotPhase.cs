using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerSnapshotPhase : ActPhase
{
    [SerializeField]
    [Tooltip("If not specified, MainAudioSourceController will be used")]
    private AudioSourceProvider[] _audioSourceProviders;
    [SerializeField]
    private AudioMixerGroup _audioMixerGroup;
    [SerializeField]
    private AudioMixerSnapshotPhaseType _phaseType;
    [SerializeField]
    [ConditionalDisplay(nameof(_phaseType), AudioMixerSnapshotPhaseType.Set)]
    private AudioMixerSnapshot _audioMixerSnapshot;
    [SerializeField]
    [ConditionalDisplay(nameof(_phaseType), AudioMixerSnapshotPhaseType.Return)]
    private AudioMixerSnapshotsTransitionType _transitionType;

    public override void Invoke()
    {
        if (_phaseType == AudioMixerSnapshotPhaseType.Set)
        {
            if (_audioSourceProviders.Length == 0)
            {
                MainAudioSourceController.Instance.ActiveAudioSourceProvider.SetAudioMixerSnapshot(_audioMixerGroup, _audioMixerSnapshot);
                MainAudioSourceController.Instance.InactiveAudioSourceProvider.SetAudioMixerSnapshot(_audioMixerGroup, _audioMixerSnapshot);
            }
            else
            {
                _audioSourceProviders.ForEach(x => x.SetAudioMixerSnapshot(_audioMixerGroup, _audioMixerSnapshot));
            }
        }
        else if (_phaseType == AudioMixerSnapshotPhaseType.Return)
        {
            if (_audioSourceProviders.Length == 0)
            {
                MainAudioSourceController.Instance.ActiveAudioSourceProvider.ReturnToAudioMixerSnapshot(_audioMixerGroup, _transitionType);
                MainAudioSourceController.Instance.InactiveAudioSourceProvider.ReturnToAudioMixerSnapshot(_audioMixerGroup, _transitionType);
            }
            else
            {
                _audioSourceProviders.ForEach(x => x.ReturnToAudioMixerSnapshot(_audioMixerGroup, _transitionType));
            }
        }
        base.InvokeFinished();
    }
}

public enum AudioMixerSnapshotPhaseType
{
    Set = 1,
    Return = 2,
}

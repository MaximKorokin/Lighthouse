using System.Collections.Generic;
using UnityEngine;

public class AudioFilterPhase : ActPhase
{
    [SerializeField]
    [Tooltip("If not specified, will use MainAudioSourceController")]
    private AudioSourceProvider[] _audioSourceProviders;
    [SerializeField]
    private AudioFilter _filter;
    [SerializeField]
    private AudioFilterPhaseType _phaseType;
    [SerializeField]
    [ConditionalDisplay(nameof(_phaseType), AudioFilterPhaseType.Return)]
    private AudioFilterReturnType _returnType;
    [SerializeField]
    [ConditionalDisplay(new[] { nameof(_filter), nameof(_phaseType) }, new object[] { AudioFilter.LowPass, AudioFilterPhaseType.Set })]
    private AudioLowPassFilterCutoffValue _cutoffValue;

    public override void Invoke()
    {
        var providers = _audioSourceProviders.Length == 0
            ? MainAudioSourceController.Instance.ActiveAudioSourceProvider.YieldWith(MainAudioSourceController.Instance.InactiveAudioSourceProvider)
            : _audioSourceProviders;

        if (_phaseType == AudioFilterPhaseType.Set) SetFiltersValue(providers);
        else if (_phaseType == AudioFilterPhaseType.Return) ReturnFiltersValue(providers);

        InvokeFinished();
    }

    private void SetFiltersValue(IEnumerable<AudioSourceProvider> providers)
    {
        switch (_filter)
        {
            case AudioFilter.LowPass:
                providers.ForEach(x => x.SetAudioFilterValue(_filter, (int)_cutoffValue));
                break;
            default:
                Logger.Error($"Unsupported value of {nameof(AudioFilter)}: {_filter}");
                break;
        }
    }

    private void ReturnFiltersValue(IEnumerable<AudioSourceProvider> providers)
    {
        providers.ForEach(x => x.ReturnAudioFilterValue(_filter, _returnType));
    }

    public override string IconName => "SoundSettings.png";
}

public enum AudioFilterPhaseType
{
    Set = 0,
    Return = 10,
}

public enum AudioLowPassFilterCutoffValue
{
    _22k = 22000,
    _5k = 5000,
    _500 = 500,
}

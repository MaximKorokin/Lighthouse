using System.Collections.Generic;
using UnityEngine;

public class AudioSourceProvider : MonoBehaviour
{
    [SerializeField]
    private AudioClipType _audioClipType;
    private AudioClipType _currentAudioClipType;

    private AudioSource _audioSource;
    public AudioSource AudioSource => gameObject.LazyGetComponent(ref _audioSource);

    private readonly Dictionary<AudioFilter, Component> _filters = new();
    private readonly Dictionary<AudioFilter, List<object>> _filtersHistory = new();

    private void Awake()
    {
        SetAudioClipType(_audioClipType);
    }

    public void SetAudioClipType(AudioClipType type)
    {
        _currentAudioClipType = type;
        switch (type)
        {
            case AudioClipType.None:
                RemoveVolumeConfigChangeListeners();
                break;
            case AudioClipType.Sound:
                RemoveVolumeConfigChangeListeners();
                SetVolume(ConfigsManager.Observable.Get(ConfigKey.SoundVolume));
                ConfigsManager.Observable.SetChangeListener(ConfigKey.SoundVolume, SetVolume);
                break;
            case AudioClipType.Music:
                RemoveVolumeConfigChangeListeners();
                SetVolume(ConfigsManager.Observable.Get(ConfigKey.MusicVolume));
                ConfigsManager.Observable.SetChangeListener(ConfigKey.MusicVolume, SetVolume);
                break;
            default:
                Logger.Error($"Invalid {nameof(AudioClipType)}");
                break;
        }
    }

    /// <summary>
    /// Returns volume from configs based on current AudioClipType
    /// </summary>
    /// <returns></returns>
    public float GetTargetVolume()
    {
        return _currentAudioClipType switch
        {
            AudioClipType.None => AudioSource.volume,
            AudioClipType.Sound => ConvertToVolume(ConfigsManager.Observable.Get(ConfigKey.SoundVolume)),
            AudioClipType.Music => ConvertToVolume(ConfigsManager.Observable.Get(ConfigKey.MusicVolume)),
            _ => 0,
        };
    }

    public void SetAudioFilterValue(AudioFilter filter, object value)
    {
        switch (filter)
        {
            case AudioFilter.LowPass:
                var component = _filters.GetOrAdd(filter, () => gameObject.AddComponent<AudioLowPassFilter>()) as AudioLowPassFilter;
                var targetValue = ConvertingUtils.ToFloat(value);
                if (component != null)
                {
                    // Unity bug: without this switch filter just stops changing its value
                    component.enabled = !component.enabled;
                    component.enabled = !component.enabled;
                    CoroutinesHandler.StartUniqueCoroutine(component, CoroutinesUtils.InterpolationCoroutine(
                        () => component.cutoffFrequency,
                        x => component.cutoffFrequency = x,
                        targetValue,
                        0.3f));
                }

                break;
            default:
                Logger.Error($"Unsupported value of {nameof(AudioFilter)}: {filter}");
                return;
        }

        _filtersHistory.AddOrModify(filter, () => new() { value }, x => { x.Add(value); return x; });
    }

    public void ReturnAudioFilterValue(AudioFilter filter, AudioFilterReturnType returnType)
    {
        if (!_filtersHistory.TryGetValue(filter, out var history)) return;

        object value;
        switch (returnType)
        {
            case AudioFilterReturnType.Previous:
                if (history.Count > 1)
                {
                    value = history.Count > 1 ? history[^2] : null;
                    history.RemoveAt(history.Count - 1);
                }
                else value = null;
                break;
            case AudioFilterReturnType.First:
                if (history.Count > 0)
                {
                    value = history[0];
                    history.Clear();
                    history.Add(value);
                }
                else value = null;
                break;
            default:
                value = null;
                break;
        };

        if (value == null) return;

        SetAudioFilterValue(filter, value);
    }

    private void RemoveVolumeConfigChangeListeners()
    {
        ConfigsManager.Observable.RemoveChangeListener(ConfigKey.SoundVolume, SetVolume);
        ConfigsManager.Observable.RemoveChangeListener(ConfigKey.MusicVolume, SetVolume);
    }

    private void SetVolume(string str) => AudioSource.volume = ConvertToVolume(str);

    private float ConvertToVolume(string str) => ConvertingUtils.ToFloat(str) / 10;

    private void OnDestroy()
    {
        RemoveVolumeConfigChangeListeners();
    }
}

public enum AudioClipType
{
    None = 0,
    Sound = 1,
    Music = 2,
}

public enum AudioFilter
{
    LowPass = 10,
}

public enum AudioFilterReturnType
{
    First = 0,
    Previous = 1,
}

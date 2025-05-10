using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceProvider : MonoBehaviour
{
    [SerializeField]
    private AudioClipType _audioClipType;
    private AudioClipType _currentAudioClipType;

    private readonly List<AudioMixerSnapshot> _snapshotsHistory = new();

    public AudioSource _audioSource;
    public AudioSource AudioSource => gameObject.LazyGetComponent(ref _audioSource);

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

    public void SetAudioMixerSnapshot(AudioMixerGroup group, AudioMixerSnapshot snapshot, float time = 0.3f)
    {
        if (group == null || snapshot == null)
        {
            Logger.Error($"{nameof(AudioMixerGroup)} or {nameof(AudioMixerSnapshot)} is null.");
            return;
        }
        _snapshotsHistory.Add(snapshot);
        AudioSource.outputAudioMixerGroup = group;
        snapshot.TransitionTo(time);
    }

    public void ReturnToAudioMixerSnapshot(AudioMixerGroup group, AudioMixerSnapshotsTransitionType transitionType)
    {
        var snapshot = transitionType switch
        {
            AudioMixerSnapshotsTransitionType.First => _snapshotsHistory.Count > 0 ? _snapshotsHistory[0] : null,
            // Get first if history contains only 1 snapshot
            AudioMixerSnapshotsTransitionType.Previous => _snapshotsHistory.Count == 0 ? null : (_snapshotsHistory.Count == 1 ? _snapshotsHistory[0] : _snapshotsHistory[^2]),
            _ => _snapshotsHistory.Count > 0 ? _snapshotsHistory[0] : null,
        };
        SetAudioMixerSnapshot(group, snapshot);
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

public enum AudioMixerSnapshotsTransitionType
{
    First = 0,
    Previous = 1,
}

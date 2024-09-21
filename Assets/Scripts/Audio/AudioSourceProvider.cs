using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceProvider : MonoBehaviour
{
    [SerializeField]
    private AudioClipType _audioClipType;
    private AudioClipType _currentAudioClipType;

    public AudioSource AudioSource { get; private set; }

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
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
                ConfigsManager.SetChangeListener(ConfigKey.SoundVolume, SetVolume);
                break;
            case AudioClipType.Music:
                RemoveVolumeConfigChangeListeners();
                ConfigsManager.SetChangeListener(ConfigKey.MusicVolume, SetVolume);
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
            AudioClipType.Sound => ConvertToVolume(ConfigsManager.GetValue(ConfigKey.SoundVolume)),
            AudioClipType.Music => ConvertToVolume(ConfigsManager.GetValue(ConfigKey.MusicVolume)),
            _ => 0,
        };
    }

    private void RemoveVolumeConfigChangeListeners()
    {
        ConfigsManager.RemoveChangeListener(ConfigKey.SoundVolume, SetVolume);
        ConfigsManager.RemoveChangeListener(ConfigKey.MusicVolume, SetVolume);
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

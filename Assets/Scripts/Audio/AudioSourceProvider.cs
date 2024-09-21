using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceProvider : MonoBehaviour
{
    [SerializeField]
    private AudioClipType _audioClipType;

    public AudioSource AudioSource { get; private set; }

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        SetAudioClipType(_audioClipType);
    }

    public void SetAudioClipType(AudioClipType type)
    {
        switch (type)
        {
            case AudioClipType.None:
                RemoveConfigChangeListeners();
                break;
            case AudioClipType.Sound:
                RemoveConfigChangeListeners();
                ConfigsManager.SetChangeListener(ConfigKey.SoundVolume, SetVolume);
                break;
            case AudioClipType.Music:
                RemoveConfigChangeListeners();
                ConfigsManager.SetChangeListener(ConfigKey.MusicVolume, SetVolume);
                break;
            default:
                Logger.Error($"Invalid {nameof(AudioClipType)}");
                break;
        }
    }

    private void RemoveConfigChangeListeners()
    {
        ConfigsManager.RemoveChangeListener(ConfigKey.SoundVolume, SetVolume);
        ConfigsManager.RemoveChangeListener(ConfigKey.MusicVolume, SetVolume);
    }

    private void SetVolume(string str)
    {
        AudioSource.volume = ConvertingUtils.ToFloat(str) / 10;
    }

    private void OnDestroy()
    {
        RemoveConfigChangeListeners();
    }
}

public enum AudioClipType
{
    None = 0,
    Sound = 1,
    Music = 2,
}

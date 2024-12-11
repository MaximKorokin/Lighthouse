using UnityEngine;

public class AudioSourceProviderPool : ObjectsPool<AudioSourceProvider, AudioSourceProviderSettings>
{
    protected override void Initialize(AudioSourceProvider provider, AudioSourceProviderSettings settings)
    {
        if (settings.IsSpatial)
        {
            provider.AudioSource.spatialBlend = 1;
            provider.transform.SetParent(null);
        }
        else
        {
            provider.AudioSource.spatialBlend = 0;
            provider.transform.SetParent(AudioSourcesParent.Instance.transform);
        }
        provider.transform.localPosition = settings.WorldPosition;
        provider.gameObject.SetActive(true);
    }

    protected override void Deinitialize(AudioSourceProvider provider)
    {
        provider.AudioSource.clip = null;
        provider.AudioSource.spatialBlend = 0;
        provider.AudioSource.loop = false;
        provider.SetAudioClipType(AudioClipType.None);
    }
}

public struct AudioSourceProviderSettings
{
    public AudioSourceProviderSettings(bool isSpatial) : this(isSpatial, Vector2.zero) { }
    public AudioSourceProviderSettings(Vector2 worldPosition) : this(true, worldPosition) { }
    public AudioSourceProviderSettings(bool isSpatial, Vector2 worldPosition)
    {
        IsSpatial = isSpatial;
        WorldPosition = worldPosition;
    }

    public bool IsSpatial;
    public Vector2 WorldPosition;
}

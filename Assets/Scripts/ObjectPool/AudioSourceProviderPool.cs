public class AudioSourceProviderPool : ObjectsPool<AudioSourceProvider, object>
{
    protected override void Initialize(AudioSourceProvider provider, object _)
    {
        provider.transform.SetParent(AudioSourcesParent.Instance.transform);
        provider.gameObject.SetActive(true);
    }

    protected override void Deinitialize(AudioSourceProvider provider)
    {
        provider.AudioSource.clip = null;
        provider.AudioSource.spatialBlend = 0;
        provider.SetAudioClipType(AudioClipType.None);
    }
}

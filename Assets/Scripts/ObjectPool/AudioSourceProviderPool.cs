public class AudioSourceProviderPool : ObjectsPool<AudioSourceProvider, object>
{
    protected override void Initialize(AudioSourceProvider source, object _)
    {
        source.transform.SetParent(AudioSourcesParent.Instance.transform);
        source.gameObject.SetActive(true);
    }

    protected override void Deinitialize(AudioSourceProvider source)
    {
        source.AudioSource.clip = null;
        source.SetAudioClipType(AudioClipType.None);
    }
}

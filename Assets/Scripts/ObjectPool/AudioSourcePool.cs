using UnityEngine;

public class AudioSourcePool : ObjectsPool<AudioSource, object>
{
    protected override void Initialize(AudioSource source, object _)
    {
        source.transform.SetParent(AudioSourcesParent.Instance.transform);
        source.gameObject.SetActive(true);
    }

    protected override void Deinitialize(AudioSource source)
    {
        source.clip = null;
    }
}

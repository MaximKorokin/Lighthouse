using UnityEngine;

public class AudioClipEffect : Effect
{
    [SerializeField]
    private AudioClip _audioClip;

    public override void Invoke(CastState castState)
    {
        var provider = AudioSourceProviderPool.Take(null);
        provider.transform.parent = null;
        provider.transform.position = castState.Source.transform.position;
        provider.AudioSource.spatialBlend = 1;
        provider.PlayAudioClip(_audioClip, false, AudioClipType.Sound, () => AudioSourceProviderPool.Return(provider));
    }
}

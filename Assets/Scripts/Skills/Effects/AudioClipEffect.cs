using UnityEngine;

public class AudioClipEffect : Effect
{
    [SerializeField]
    private AudioClip _audioClip;

    public override void Invoke(CastState castState)
    {
        var provider = AudioSourceProviderPool.Take(new(castState.Source.transform.position));
        provider.PlayAudioClip(_audioClip, false, AudioClipType.Sound, () => AudioSourceProviderPool.Return(provider));
    }
}

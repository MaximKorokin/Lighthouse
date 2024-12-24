using UnityEngine;

public class AudioClipEffect : Effect
{
    [SerializeField]
    private AudioClip _audioClip;

    [SerializeField]
    private bool _parentToTarget;

    public override void Invoke(CastState castState)
    {
        var provider = AudioSourceProviderPool.Take(new(castState.Source.transform.position));
        if (_parentToTarget )
        {
            provider.transform.SetParent(castState.GetTarget().transform);
        }
        provider.PlayAudioClip(_audioClip, false, AudioClipType.Sound, () => AudioSourceProviderPool.Return(provider));
    }
}

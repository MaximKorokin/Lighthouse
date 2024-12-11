using UnityEngine;

public class AudioClipPhase : ActPhase
{
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private AudioClipType _type;

    public override void Invoke()
    {
        var provider = AudioSourceProviderPool.Take(new(false));
        provider.PlayAudioClip(_audioClip, false, _type, () => AudioSourceProviderPool.Return(provider));

        InvokeFinished();
    }

    public override string IconName => "SoundNote.png";
    public override Color IconColor => MyColors.LightGray;
}

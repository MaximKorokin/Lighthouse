using System.Collections;
using UnityEngine;

public class AudioClipPhase : ActPhase
{
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private bool _loop;
    [SerializeField]
    private AudioClipType _type;

    public override void Invoke()
    {
        var provider = AudioSourceProviderPool.Take(null);

        provider.SetAudioClipType(_type);
        provider.AudioSource.loop = _loop;
        provider.AudioSource.clip = _audioClip;
        provider.AudioSource.Play();

        this.StartCoroutineSafe(AudioSourceLifetimeCoroutine(provider));

        InvokeFinished();
    }

    private IEnumerator AudioSourceLifetimeCoroutine(AudioSourceProvider provider)
    {
        yield return new WaitForSeconds(_audioClip.length);
        AudioSourceProviderPool.Return(provider);
    }

    public override string IconName => "SoundNote.png";
    public override Color IconColor => MyColors.LightGray;
}

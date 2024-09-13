using System.Collections;
using UnityEngine;

public class AudioClipPhase : ActPhase
{
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private bool _loop;

    public override void Invoke()
    {
        var audioSource = AudioSourcePool.Take(null);
        this.StartCoroutineSafe(AudioSourceLifetimeCoroutine(audioSource));
        audioSource.loop = _loop;
        audioSource.clip = _audioClip;
        audioSource.Play();
        InvokeFinished();
    }

    private IEnumerator AudioSourceLifetimeCoroutine(AudioSource audioSource)
    {
        yield return new WaitForSeconds(_audioClip.length);
        AudioSourcePool.Return(audioSource);
    }

    public override string IconName => "SoundNote.png";
    public override Color IconColor => MyColors.LightGray;
}

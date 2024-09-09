using System.Collections;
using UnityEngine;

public class AudioClipPhase : ActPhase
{
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private bool _loop;
    [SerializeField]
    private bool _main;

    public override void Invoke()
    {
        AudioSource audioSource;
        if (_main)
        {
            audioSource = AudioSourceProvider.Main.AudioSource;
        }
        else
        {
            audioSource = AudioSourcePool.Take(null);
            this.StartCoroutineSafe(AudioSourceLifetimeCoroutine(audioSource));
        }
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
}

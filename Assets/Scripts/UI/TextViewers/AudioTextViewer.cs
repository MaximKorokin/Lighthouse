using UnityEngine;

public abstract class AudioTextViewer : TextViewer
{
    private AudioSource _audioSource;
    private AudioSource AudioSource => _audioSource = _audioSource != null ? _audioSource : AudioSourcePool.Take(null);

    protected virtual void Start()
    {
        Typewriter.CharTyping += OnCharTyping;
        AudioSource.loop = false;
    }

    private void OnCharTyping()
    {
        if (AudioSource.clip != null)
        {
            AudioSource.Play();
        }
    }

    public void SetTypingSound(AudioClip audioClip)
    {
        AudioSource.clip = audioClip;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_audioSource != null && _audioSource.gameObject.activeInHierarchy)
        {
            AudioSourcePool.Return(_audioSource);
        }
    }
}

using UnityEngine;

public abstract class AudioTextViewer : TextViewer
{
    private AudioSourceProvider _audioSourceProvider;
    private AudioSourceProvider AudioSourceProvider => _audioSourceProvider = _audioSourceProvider != null ? _audioSourceProvider : AudioSourceProviderPool.Take(null);

    protected virtual void Start()
    {
        Typewriter.CharTyping += OnCharTyping;
        AudioSourceProvider.SetAudioClipType(AudioClipType.Sound);
        AudioSourceProvider.AudioSource.loop = false;
    }

    private void OnCharTyping()
    {
        if (AudioSourceProvider.AudioSource.clip != null)
        {
            AudioSourceProvider.AudioSource.Play();
        }
    }

    public void SetTypingSound(AudioClip audioClip)
    {
        AudioSourceProvider.AudioSource.clip = audioClip;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (_audioSourceProvider != null && _audioSourceProvider.gameObject.activeInHierarchy)
        {
            AudioSourceProviderPool.Return(_audioSourceProvider);
        }
    }
}

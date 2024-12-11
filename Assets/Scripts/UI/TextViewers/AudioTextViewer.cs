using UnityEngine;

public abstract class AudioTextViewer : TextViewer
{
    private AudioSourceProvider _audioSourceProvider;
    private AudioSourceProvider AudioSourceProvider => _audioSourceProvider = _audioSourceProvider != null ? _audioSourceProvider : AudioSourceProviderPool.Take(new(_spatial, transform.position));

    [SerializeField]
    private bool _spatial;
    [SerializeField]
    private AudioClip _audioClip;

    protected virtual void Start()
    {
        Typewriter.CharTyping += OnCharTyping;
        AudioSourceProvider.SetAudioClipType(AudioClipType.Sound);
        AudioSourceProvider.transform.SetParent(transform);
    }

    private void OnCharTyping()
    {
        if (_audioClip != null)
        {
            AudioSourceProvider.PlayAudioClip(_audioClip, false, AudioClipType.Sound);
        }
    }

    public void SetTypingSound(AudioClip audioClip)
    {
        _audioClip = audioClip;
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

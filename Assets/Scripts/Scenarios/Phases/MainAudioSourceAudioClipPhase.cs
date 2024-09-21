using UnityEngine;

public class MainAudioSourceAudioClipPhase : ActPhase
{
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private AudioClipType _type;
    [SerializeField]
    private bool _loop;
    [SerializeField]
    private AudioClipOperation _operation;

    public override void Invoke()
    {
        MainAudioSourceController.Instance.SetAudioClip(_audioClip, _type, _loop, _operation);
        InvokeFinished();
    }

    public override string IconName => "SoundNote.png";
}

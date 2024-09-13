using System.Collections.Generic;
using UnityEngine;

public class MainAudioSourceAudioClipPhase : ActPhase
{
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private bool _loop;
    [SerializeField]
    private AudioClipOperation _operation;

    public override void Invoke()
    {
        MainAudioSourceController.Instance.SetAudioClip(_audioClip, _loop, _operation);
        InvokeFinished();
    }

    public override string IconName => "SoundNote.png";
}

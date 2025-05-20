using UnityEngine;

public class AudioClipPhase : ActPhase
{
    [SerializeField]
    private AudioClip _audioClip;
    [SerializeField]
    private AudioClipType _type;
    [Space]
    [SerializeField]
    private bool _spatial;
    [SerializeField]
    [ConditionalDisplay(nameof(_spatial), true)]
    private bool _childToTarget;
    [SerializeField]
    [ConditionalDisplay(nameof(_spatial), true)]
    private Transform _transformPosition;

    public override void Invoke()
    {
        var provider = _spatial
            ? AudioSourceProviderPool.Take(new(true, _transformPosition.position))
            : AudioSourceProviderPool.Take(new(false));
        provider.PlayAudioClip(_audioClip, false, _type, () => AudioSourceProviderPool.Return(provider));

        if (_spatial && _childToTarget)
        {
            provider.transform.SetParent(_transformPosition, false);
        }

        InvokeFinished();
    }

    public override string IconName => "SoundNote.png";
    public override Color IconColor => MyColors.LightGray;
}

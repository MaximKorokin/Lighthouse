using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GenericAnimatorController : MonoBehaviour
{
    private static readonly string PlayAnimationKey = AnimatorKey.PlayAnimation.ToString();
    private static readonly string StopAnimationKey = AnimatorKey.StopAnimation.ToString();
    private const string ActionAnimationName = "Action";

    [SerializeField]
    private RuntimeAnimatorController _animatorController;

    private Animator _animator;
    private AnimationClip _animation;
    private CooldownCounter _playingCounter;
    private bool _isPlaying;

    public event Action FinishedPlaying;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playingCounter = new(0);
    }

    private void Update()
    {
        if (_isPlaying && _playingCounter.IsOver())
        {
            StopAnimation();
        }
    }

    public void SetAnimation(AnimationClip animation)
    {
        var overrideController = new AnimatorOverrideController(_animatorController);
        overrideController[ActionAnimationName] = animation;
        _animator.runtimeAnimatorController = overrideController;
        _animation = animation;
    }

    public void PlayAnimation(bool hasDuration)
    {
        _animator.SetBool(PlayAnimationKey, true);

        _playingCounter.Cooldown = hasDuration ? _animation.length : float.PositiveInfinity;
        _playingCounter.Reset();
        _isPlaying = true;
    }

    public void StopAnimation()
    {
        if (_isPlaying)
        {
            _isPlaying = false;
            _animator.SetBool(StopAnimationKey, true);
            FinishedPlaying?.Invoke();
        }
    }
}

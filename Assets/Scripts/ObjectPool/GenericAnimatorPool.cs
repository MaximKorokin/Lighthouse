using UnityEngine;

public class GenericAnimatorPool : ObjectPool<Animator, AnimationClip>
{
    public static GenericAnimatorPool Instance { get; private set; }

    [SerializeField]
    private RuntimeAnimatorController _controller;

    private void Awake()
    {
        Instance = this;
    }

    protected override void Initialize(Animator animator, AnimationClip animation)
    {
        var overrideController = new AnimatorOverrideController(_controller);
        overrideController["Action"] = animation;
        animator.runtimeAnimatorController = overrideController;
        animator.gameObject.SetActive(true);
        animator.SetTrigger("Act");
    }
}

using UnityEngine;

public class GenericAnimatorPool : ObjectPool<Animator, AnimationClip>
{
    private const string IsActingKey = "IsActing";
    private const string ActionAnimationName = "Action";

    [SerializeField]
    private RuntimeAnimatorController _animatorController;

    protected override void Initialize(Animator animator, AnimationClip animation)
    {
        var overrideController = new AnimatorOverrideController(_animatorController);
        overrideController[ActionAnimationName] = animation;
        animator.runtimeAnimatorController = overrideController;
        animator.gameObject.SetActive(true);
        animator.enabled = true;
        animator.SetBool(IsActingKey, true);
    }

    protected override void Deinitialize(Animator animator)
    {
        animator.SetBool(IsActingKey, false);
        var spriteRenderer = animator.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
            spriteRenderer.color = Color.white;
        }
    }
}

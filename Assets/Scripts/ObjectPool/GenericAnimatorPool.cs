using UnityEngine;

public class GenericAnimatorPool : ObjectPool<Animator, AnimationClip>
{
    [SerializeField]
    private RuntimeAnimatorController _animatorController;

    protected override void Initialize(Animator animator, AnimationClip animation)
    {
        var overrideController = new AnimatorOverrideController(_animatorController);
        overrideController["Action"] = animation;
        animator.runtimeAnimatorController = overrideController;
        animator.gameObject.SetActive(true);
        animator.SetBool("IsActing", true);
    }

    protected override void Deinitialize(Animator animator)
    {
        animator.SetBool("IsActing", false);
        var spriteRenderer = animator.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
            spriteRenderer.color = Color.white;
        }
    }
}

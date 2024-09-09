using UnityEngine;

public class GenericAnimatorPool : ObjectsPool<GenericAnimatorController, AnimationClip>
{
    protected override void Initialize(GenericAnimatorController controller, AnimationClip animation)
    {
        controller.gameObject.SetActive(true);
        controller.SetAnimation(animation);
    }

    protected override void Deinitialize(GenericAnimatorController controller)
    {
        controller.StopAnimation();
        if (controller.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
        {
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
            spriteRenderer.color = Color.white;
            spriteRenderer.sprite = null;
        }
    }
}

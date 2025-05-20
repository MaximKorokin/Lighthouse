/// <summary>
/// Must be used in conjunction with <see cref="ComplexAnimator"/> which finds instances of this class
/// </summary>
public class SingleAnimator : AnimatorBase
{
    private void Awake()
    {
        if (TryGetComponent(out ComplexAnimator animator) || transform.parent.TryGetComponent(out animator))
        {
            animator.AddAnimator(this);
        }
        else
        {
            Logger.Warn($"Could not find {typeof(ComplexAnimator)} on self ({name}) or parent ({transform.parent.name}) object.");
        }
    }
}

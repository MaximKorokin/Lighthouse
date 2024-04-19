using UnityEngine;

public interface IAnimator
{
    void SetAnimatorValue<T>(AnimatorKey key, T value = default) where T : struct;
    void SetFlip(bool shouldFlip);
    Vector2 GetExtents();
    void SetShift(Vector2 shift);
}
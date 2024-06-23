using UnityEngine;

public class TriggerDetector : TriggerDetectorBase<Collider2D>
{
    protected override bool ValidateTarget(Collider2D collision, out Collider2D collider)
    {
        collider = collision;
        return base.ValidateTarget(collision, out _);
    }
}

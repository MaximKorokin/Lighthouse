using System;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    public GenericAnimatorController AnimatorController { get; private set; }

    private void Awake()
    {
        AnimatorController = this.GetRequiredComponent<GenericAnimatorController>();
    }

    public void SetSettings(OverlaySettings settings)
    {
        AnimatorController.SetAnimation(settings.Animation);
    }
}

[Serializable]
public struct OverlaySettings
{
    public AnimationClip Animation;
    public bool IsHierarchyPriority;
}

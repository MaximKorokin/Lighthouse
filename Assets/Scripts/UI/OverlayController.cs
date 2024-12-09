using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
public class OverlayController : MonoBehaviour
{
    public GenericAnimatorController AnimatorController { get; private set; }

    private void Awake()
    {
        AnimatorController = GetComponent<GenericAnimatorController>();
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

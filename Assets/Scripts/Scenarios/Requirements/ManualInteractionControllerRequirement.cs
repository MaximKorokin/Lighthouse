using UnityEngine;

public class ManualInteractionControllerRequirement : ActRequirement
{
    [SerializeField]
    private ManualInteractionController _controller;

    private bool _hasInteracted = false;

    public ManualInteractionController Controller => _controller;

    private void Awake()
    {
        _controller.Interacted += OnIneracted;
    }

    private void OnIneracted()
    {
        _hasInteracted = true;
        InvokeFulfilled();
    }

    public override bool IsFulfilled()
    {
        return _hasInteracted;
    }

    public override string IconName => base.IconName;
}

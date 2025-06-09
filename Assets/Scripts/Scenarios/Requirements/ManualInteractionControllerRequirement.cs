using UnityEngine;

public class ManualInteractionControllerRequirement : ActRequirement
{
    [SerializeField]
    private ManualInteractionController _controller;
    public ManualInteractionController Controller => _controller;

    private readonly CooldownCounter _interactionCD = new(0.5f);

    private void Awake()
    {
        _controller.Interacted += OnInteracted;
    }

    private void OnInteracted()
    {
        _interactionCD.Reset();
        InvokeFulfilled();
    }

    public override bool IsFulfilled()
    {
        return !_interactionCD.IsOver();
    }

    public override string IconName => "Input.png";
}

﻿public class WorldObjectsTriggeringMediatorListener : WorldObjectsTriggeringMediatorAgent
{
    protected override void Start()
    {
        base.Start();

        var detector = this.GetRequiredComponent<WorldObjectInteractingTriggerDetector>();
        WorldObjectsTriggeringMediator.Instance.AddListener(WorldObject, detector);

        WorldObject.PhysicsStateReloading += OnPhysicsStateReloaded;
    }

    private void OnPhysicsStateReloaded()
    {
        if (WorldObjectsTriggeringMediator.Instance != null)
        {
            WorldObjectsTriggeringMediator.Instance.ResetListenerState(WorldObject);
        }
    }

    protected override void OnDestroyed()
    {
        base.OnDestroyed();
        WorldObjectsTriggeringMediator.Instance.RemoveListener(WorldObject);
    }
}

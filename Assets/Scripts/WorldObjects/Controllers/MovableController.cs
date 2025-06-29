public abstract class MovableController : TriggerController
{
    protected MovableWorldObject MovableWorldObject { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        MovableWorldObject = this.GetRequiredComponent<MovableWorldObject>();
    }

    protected override void Update()
    {
        if (MovableWorldObject.IsAlive)
        {
            base.Update();
        }
    }
}

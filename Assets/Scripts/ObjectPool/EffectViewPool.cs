class EffectViewPool : ObjectPool<EffectView, Effect>
{
    protected override void Initialize(EffectView view, Effect param)
    {
        view.Initialize(param);
        view.gameObject.SetActive(true);
    }

    protected override void Deinitialize(EffectView obj)
    {

    }
}

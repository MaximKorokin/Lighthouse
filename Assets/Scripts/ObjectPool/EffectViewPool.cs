class EffectViewPool : ObjectsPool<EffectView, EffectPreview>
{
    protected override void Initialize(EffectView view, EffectPreview param)
    {
        view.Initialize(param);
        view.gameObject.SetActive(true);
    }

    protected override void Deinitialize(EffectView obj)
    {

    }
}

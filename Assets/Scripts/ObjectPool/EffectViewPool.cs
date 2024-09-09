class EffectViewPool : ObjectsPool<EffectView, EffectPreview>
{
    protected override void Initialize(EffectView view, EffectPreview parameter)
    {
        view.Initialize(parameter);
        view.gameObject.SetActive(true);
    }

    protected override void Deinitialize(EffectView obj)
    {

    }
}

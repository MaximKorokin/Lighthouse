class BarsPool : ObjectPool<BarController, BarController>
{
    protected override void Initialize(BarController bar, BarController source)
    {
        if (bar == null || source == null)
        {
            return;
        }

        source.CopyTo(bar);
        bar.gameObject.SetActive(true);
    }

    protected override void Deinitialize(BarController bar)
    {

    }
}

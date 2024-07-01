using UnityEngine;

public abstract class VignetteAmountVisualizer : AmountVisualizerBase
{
    [SerializeField]
    private OverlaySettings _overlayParameter;

    protected OverlayController Overlay { get; private set; }

    protected virtual void Start()
    {
        Overlay = OverlayPool.Take(_overlayParameter);
    }

    protected virtual void OnDestroy()
    {
        ReturnOverlay();
    }

    protected void ReturnOverlay()
    {
        if (Overlay == null)
        {
            return;
        }
        OverlayPool.Return(Overlay);
        Overlay = null;
    }
}

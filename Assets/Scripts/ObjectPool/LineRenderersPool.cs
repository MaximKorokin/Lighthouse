using UnityEngine;

public class LineRenderersPool : ObjectPool<LineRenderer, LineRenderer>
{
    protected override void Initialize(LineRenderer renderer, LineRenderer _)
    {
        renderer.gameObject.SetActive(true);
    }

    protected override void Deinitialize(LineRenderer renderer)
    {

    }
}

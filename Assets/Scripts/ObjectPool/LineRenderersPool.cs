using UnityEngine;

public class LineRenderersPool : ObjectsPool<LineRenderer, LineRenderer>
{
    protected override void Initialize(LineRenderer renderer, LineRenderer _)
    {
        renderer.gameObject.SetActive(true);
    }

    protected override void Deinitialize(LineRenderer renderer)
    {

    }
}

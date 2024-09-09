using UnityEngine;

public class LineRenderersPool : ObjectsPool<LineRenderer, object>
{
    protected override void Initialize(LineRenderer renderer, object _)
    {
        renderer.gameObject.SetActive(true);
    }

    protected override void Deinitialize(LineRenderer renderer)
    {

    }
}

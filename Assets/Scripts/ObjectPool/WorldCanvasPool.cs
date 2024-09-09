using UnityEngine;

class WorldCanvasPool : ObjectsPool<WorldCanvasController, object>
{
    protected override void Initialize(WorldCanvasController canvasController, object _)
    {
        canvasController.Canvas.transform.localScale = Vector3.one * .1f;
        canvasController.gameObject.SetActive(true);
    }

    protected override void Deinitialize(WorldCanvasController canvasController)
    {

    }
}

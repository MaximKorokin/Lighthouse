using UnityEngine;

class WorldCanvasPool : ObjectsPool<WorldCanvasController, WorldCanvasController>
{
    protected override void Initialize(WorldCanvasController canvasController, WorldCanvasController _)
    {
        if (canvasController == null)
        {
            return;
        }

        canvasController.Canvas.transform.localScale = Vector3.one * .1f;
        canvasController.gameObject.SetActive(true);
    }

    protected override void Deinitialize(WorldCanvasController canvasController)
    {

    }
}

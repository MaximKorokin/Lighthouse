using UnityEngine;

class WorldObjectCanvasPool : ObjectsPool<WorldObjectCanvasController, WorldObjectCanvasController>
{
    protected override void Initialize(WorldObjectCanvasController canvasController, WorldObjectCanvasController _)
    {
        if (canvasController == null)
        {
            return;
        }

        canvasController.Canvas.transform.localScale = Vector3.one * .1f;
        canvasController.gameObject.SetActive(true);
    }

    protected override void Deinitialize(WorldObjectCanvasController canvasController)
    {

    }
}

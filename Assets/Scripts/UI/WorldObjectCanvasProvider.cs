using UnityEngine;

public class WorldObjectCanvasProvider : MonoBehaviour
{
    private WorldObjectCanvasController _canvasController;
    public WorldObjectCanvasController CanvasController => _canvasController = _canvasController != null ? _canvasController : TakeAndInitialize();

    private WorldObjectCanvasController TakeAndInitialize()
    {
        _canvasController = WorldObjectCanvasPool.Take(null);
        _canvasController.transform.SetParent(transform, false);
        return _canvasController;
    }

    private void OnDestroy()
    {
        if (_canvasController != null) WorldObjectCanvasPool.Return(_canvasController);
    }
}

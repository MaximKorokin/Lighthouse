using UnityEngine;

public class WorldCanvasProvider : MonoBehaviour
{
    private WorldCanvasController _canvasController;
    public WorldCanvasController CanvasController => _canvasController = _canvasController != null ? _canvasController : TakeAndInitialize();

    private WorldCanvasController TakeAndInitialize()
    {
        _canvasController = WorldCanvasPool.Take(null);
        _canvasController.transform.SetParent(transform, false);
        return _canvasController;
    }

    private void OnDestroy()
    {
        if (_canvasController != null) WorldCanvasPool.Return(_canvasController);
    }
}

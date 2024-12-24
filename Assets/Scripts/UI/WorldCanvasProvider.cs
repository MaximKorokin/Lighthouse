using UnityEngine;

public class WorldCanvasProvider : MonoBehaviour
{
    private Vector3 _initialCanvasScale;
    private Vector3 _previousParentScale;

    private WorldCanvasController _canvasController;
    public WorldCanvasController CanvasController => _canvasController = _canvasController != null ? _canvasController : TakeAndInitialize();

    private WorldCanvasController TakeAndInitialize()
    {
        _canvasController = WorldCanvasPool.Take(null);
        _canvasController.transform.SetParent(transform, false);
        _initialCanvasScale = _canvasController.Canvas.transform.localScale;
        ScaleCanvasToParent();
        return _canvasController;
    }

    private void ScaleCanvasToParent()
    {
        if (_canvasController == null || transform.localScale == _previousParentScale) return;

        _previousParentScale = transform.localScale;
        _canvasController.transform.localScale = new Vector3(
            _initialCanvasScale.x / _previousParentScale.x,
            _initialCanvasScale.y / _previousParentScale.y,
            _initialCanvasScale.z / _previousParentScale.z);
    }

    private void Update()
    {
        ScaleCanvasToParent();
    }

    private void OnDestroy()
    {
        if (_canvasController != null) WorldCanvasPool.Return(_canvasController);
    }
}

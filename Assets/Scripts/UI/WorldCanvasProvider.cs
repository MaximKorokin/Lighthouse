using UnityEngine;

public class WorldCanvasProvider : MonoBehaviour
{
    private Vector3 _previousParentScale;

    private WorldCanvasController _canvasController;
    public WorldCanvasController CanvasController => this.LazyInitialize(ref _canvasController, TakeAndInitialize);

    private WorldCanvasController TakeAndInitialize()
    {
        _canvasController = WorldCanvasPool.Take(null);
        _canvasController.transform.SetParent(transform, false);
        ScaleCanvasToParent();
        return _canvasController;
    }

    private void ScaleCanvasToParent()
    {
        if (_canvasController == null || transform.localScale == _previousParentScale) return;

        _previousParentScale = transform.localScale;

        var targetScale = new Vector3(1 / _previousParentScale.x, 1 / _previousParentScale.y, 1 / _previousParentScale.z);

        var childrenElements = new Transform[]
        {
            _canvasController.SpeechBubbleParent,
            _canvasController.HPChangeParent,
            _canvasController.HPViewParent,
            _canvasController.SkillsCDParent,
        };

        foreach (var childTransform in childrenElements)
        {
            if (childTransform.localScale != targetScale)
            {
                childTransform.localScale = targetScale;
            }
        }
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

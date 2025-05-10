using UnityEngine;

public class InheritedCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _parentCamera;

    private Camera _camera;

    private void Awake()
    {
        _camera = this.GetRequiredComponent<Camera>();
    }

    private void Update()
    {
        if (_camera.orthographicSize != _parentCamera.orthographicSize)
        {
            _camera.orthographicSize = _parentCamera.orthographicSize;
        }
    }
}

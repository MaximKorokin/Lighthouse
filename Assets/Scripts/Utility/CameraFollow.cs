using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _outrunningValue;

    private Camera _mainCamera;
    private MovableWorldObject _movableWorldObject;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _movableWorldObject = GetComponent<MovableWorldObject>();
    }

    private void Update()
    {
        var _mainCameraZ = _mainCamera.transform.position.z;
        var newPos = Vector2.Lerp(
            _mainCamera.transform.position,
            (Vector2)transform.position + _movableWorldObject.Direction * _outrunningValue,
            _speed * Time.deltaTime);
        _mainCamera.transform.position = new Vector3(newPos.x, newPos.y, _mainCameraZ);
    }
}
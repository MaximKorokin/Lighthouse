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
    private float _zDifference;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _movableWorldObject = GetComponent<MovableWorldObject>();
        _zDifference = _mainCamera.transform.position.z - _movableWorldObject.transform.position.z;
        _mainCamera.transform.position = new Vector3(
            _movableWorldObject.transform.position.x,
            _movableWorldObject.transform.position.y,
            _zDifference);
    }

    private void FixedUpdate()
    {
        var newPos = Vector2.Lerp(
            _mainCamera.transform.position,
            (Vector2)transform.position + _movableWorldObject.Direction * _outrunningValue,
            _speed * Time.fixedDeltaTime);
        _mainCamera.transform.position = new Vector3(newPos.x, newPos.y, _movableWorldObject.transform.position.z + _zDifference);
    }
}

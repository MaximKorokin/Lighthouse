using UnityEngine;

[RequireComponent(typeof(MovableWorldObject))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _outrunningValue;
    [SerializeField]
    private CameraMovementPriority _priority;

    private MainCameraController _mainCamera;
    private MovableWorldObject _movableWorldObject;

    private void Start()
    {
        _movableWorldObject = GetComponent<MovableWorldObject>();
        _movableWorldObject.DirectionSet += OnDirectionSet;

        _mainCamera = MainCameraController.Instance;
    }

    private void OnDirectionSet(Vector2 obj)
    {
        SetMovement();
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            SetMovement();
        }
    }

    private void SetMovement()
    {
        _mainCamera.SetMovement(transform.position + (Vector3)_movableWorldObject.Direction * _outrunningValue, _speed, true, _priority);
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MainCameraController : MonoBehaviorSingleton<MainCameraController>
{
    private Camera _camera;
    private Vector3 _destination;
    private float _speed;
    private bool _smooth;

    private float _zDifference;

    public CameraMovementPriority MinPriority { get; set; } = CameraMovementPriority.Low;

    public static event Action<Vector2, Vector2> PositionChanged;
    public static event Action MoveFinished;

    protected override void Awake()
    {
        _camera = GetComponent<Camera>();
        _zDifference = _camera.transform.position.z;
        if (_camera == Camera.main)
        {
            base.Awake();
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            MoveTowardsDestination(Time.unscaledDeltaTime);
        }
    }

    private void FixedUpdate()
    {
        if ((Vector2)_camera.transform.position != (Vector2)_destination)
        {
            MoveTowardsDestination(Time.fixedDeltaTime);
        }
    }

    private void MoveTowardsDestination(float factor)
    {
        Vector2 newPosition;
        if (_smooth)
        {
            newPosition = Vector2.Lerp(
                _camera.transform.position,
                _destination,
                _speed * factor);
        }
        else
        {
            newPosition = Vector2.MoveTowards(
                _camera.transform.position,
                _destination,
                _speed * factor);
        }
        var oldPosition = _camera.transform.position;
        _camera.transform.position = new Vector3(newPosition.x, newPosition.y, _destination.z + _zDifference);
        PositionChanged?.Invoke(oldPosition, _camera.transform.position);

        if ((Vector2)_camera.transform.position == (Vector2)_destination)
        {
            MoveFinished?.Invoke();
        }
    }

    public static void SetMovement(Vector3 destination, float speed, bool smooth, CameraMovementPriority priority)
    {
        if (priority < Instance.MinPriority)
        {
            return;
        }
        Instance._destination = destination;
        Instance._speed = speed;
        Instance._smooth = smooth;
    }
}

public enum CameraMovementPriority
{
    None = 0,
    Low = 10,
    Medium = 20,
    High = 30,
}

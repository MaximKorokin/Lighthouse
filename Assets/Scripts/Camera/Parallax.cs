using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private float _movementScale;

    private void Start()
    {
        MainCameraController.Instance.PositionChanged += OnCameraPositionChanged;
    }

    private void OnCameraPositionChanged(Vector2 oldPosition, Vector2 newPosition)
    {
        var positionDelta = newPosition.x - oldPosition.x;
        transform.position += _movementScale * positionDelta * Vector3.right;
    }
}

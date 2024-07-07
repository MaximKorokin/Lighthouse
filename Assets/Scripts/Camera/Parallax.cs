using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Vector2 _movementScale;

    private void Start()
    {
        MainCameraController.PositionChanged += OnCameraPositionChanged;
    }

    private void OnCameraPositionChanged(Vector2 oldPosition, Vector2 newPosition)
    {
        var positionDelta = newPosition - oldPosition;
        transform.position += Vector3.Scale(positionDelta, _movementScale);
    }
}

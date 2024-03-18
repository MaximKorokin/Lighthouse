using UnityEngine;

[RequireComponent(typeof(WorldObject))]
public class WorldObjectZIndex : MonoBehaviour
{
    private Transform _transform;
    private WorldObject _worldObject;

    private void Awake()
    {
        _transform = transform;
        _worldObject = GetComponent<WorldObject>();
    }

    private void FixedUpdate()
    {
        var position = _transform.position;
        _transform.position = new Vector3(position.x, position.y, position.y + _worldObject.VisualPositionOffset.y);
    }
}

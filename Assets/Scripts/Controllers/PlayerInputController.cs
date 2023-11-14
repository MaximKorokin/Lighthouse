using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private Vector2 direction;
    protected MovableWorldObject WorldObject { get; private set; }

    private void Awake()
    {
        WorldObject = GetComponent<MovableWorldObject>();
    }

    private void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        WorldObject.Direction = direction;
        if (direction == Vector2.zero)
        {
            WorldObject.Stop();
        }
        else
        {
            WorldObject.Move();
        }

        WorldObject.Act(WorldObject);
    }
}

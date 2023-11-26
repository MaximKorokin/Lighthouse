using UnityEngine;

public class DeviceInputReader : InputReader
{
    protected override Vector2 GetMoveVectorInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}

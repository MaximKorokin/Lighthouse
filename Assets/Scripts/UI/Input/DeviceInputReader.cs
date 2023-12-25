using UnityEngine;

public class DeviceInputReader : InputReader
{
    protected override Vector2 GetMoveVectorInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    protected override bool IsActiveAbilityUsed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}

using UnityEngine;

public class DeviceInputReader : InputReader
{
    protected override Vector2 GetMoveInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); ;
    }

    protected override bool IsActiveAbilityUsed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    protected override bool IsMoveAbilityUsed()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }
}

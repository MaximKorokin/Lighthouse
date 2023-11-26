using UnityEngine;

public class ScreenInputReader : InputReader
{
    [SerializeField]
    private Joystick _joystick;

    protected override Vector2 GetMoveVectorInput()
    {
        return _joystick.InputVector;
    }
}

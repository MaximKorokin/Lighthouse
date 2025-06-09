using UnityEngine;

public class InputDashEffect : DashEffect
{
    protected override Vector2 GetDirection(CastState castState)
    {
        return InputReader.MoveAbilityInputRecieved.Value;
    }
}

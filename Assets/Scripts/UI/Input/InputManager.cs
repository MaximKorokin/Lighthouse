using System;
using UnityEngine;

public static class InputManager
{
    private static Vector2 _moveVector;

    public static bool IsControlInputBlocked { get; set; }

    public static event Action AnyKeyClicked;
    public static event Action<Vector2> MoveVectorChanged;
    public static event Action ActiveAbilityUsed;

    public static void InvokeAnyKeyClicked()
    {
        AnyKeyClicked?.Invoke();
    }

    public static void ChangeMoveVector(Vector2 vector)
    {
        if (IsControlInputBlocked || _moveVector == vector)
        {
            return;
        }
        _moveVector = vector;
        MoveVectorChanged?.Invoke(_moveVector);
    }

    public static void UseActiveAbility()
    {
        if (IsControlInputBlocked)
        {
            return;
        }
        ActiveAbilityUsed?.Invoke();
    }
}

using System;
using UnityEngine;

public static class InputManager
{
    private static Vector2 _moveVector;

    public static bool _isControlInputBlocked;
    public static bool IsControlInputBlocked
    {
        get => _isControlInputBlocked;
        set
        {
            if (_isControlInputBlocked != value)
            {
                InputBlockChanging?.Invoke(value);
                _isControlInputBlocked = value;
            }
        }
    }

    public static event Action AnyKeyClicked;
    public static event Action<Vector2> MoveVectorChanged;
    public static event Action ActiveAbilityUsed;

    public static event Action<bool> InputBlockChanging;

    static InputManager()
    {
        InputBlockChanging += blocked => { if (blocked) ChangeMoveVector(Vector2.zero); };
    }

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

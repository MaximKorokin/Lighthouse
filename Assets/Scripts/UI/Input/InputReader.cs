using System;
using UnityEngine;

public abstract class InputReader : MonoBehaviour
{
    #region static
    private static readonly object _eventsInvoker = new();

    private static BoolCounter _isControlInputBlocked = new(false);
    public static bool IsControlInputBlocked
    {
        get => _isControlInputBlocked;
        set => _isControlInputBlocked.Set(value);
    }

    public static event Action<bool> InputBlockChanging;

    public static FrameBoundEvent<bool> AnyKeyClicked = new(_eventsInvoker);
    public static FrameBoundEvent<Vector2> MoveInputRecieved = new(_eventsInvoker);
    public static FrameBoundEvent<bool> ActiveAbilityInputRecieved = new(_eventsInvoker);
    public static FrameBoundEvent<bool> MoveAbilityInputRecieved = new(_eventsInvoker);

    static InputReader()
    {
        _isControlInputBlocked.ValueChanged += v =>
        {
            if (v) ResetInput();
            InputBlockChanging?.Invoke(v);
        };
    }
    #endregion

    private Vector2 _moveInput = new();

    protected abstract Vector2 GetMoveInput();
    protected abstract bool IsActiveAbilityUsed();
    protected abstract bool IsMoveAbilityUsed();

    protected virtual void Update()
    {
        if (IsControlInputBlocked)
        {
            return;
        }
        
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            AnyKeyClicked?.Invoke(_eventsInvoker, true);
        }

        if (TryGetMoveVectorInput(out var moveInput))
        {
            MoveInputRecieved?.Invoke(_eventsInvoker, moveInput);
        }

        if (IsActiveAbilityUsed())
        {
            ActiveAbilityInputRecieved?.Invoke(_eventsInvoker, true);
        }

        if (IsMoveAbilityUsed())
        {
            MoveAbilityInputRecieved?.Invoke(_eventsInvoker, true);
        }
    }

    protected bool TryGetMoveVectorInput(out Vector2 input)
    {
        input = GetMoveInput();
        if (_moveInput == input) return false;
        _moveInput = input;
        return true;
    }

    private static void ResetInput()
    {
        MoveInputRecieved?.Invoke(_eventsInvoker, Vector2.zero);
    }
}

using System;
using UnityEngine;

public abstract class InputReader : MonoBehaviour
{
    #region static
    private static readonly object _eventsInvoker = new();

    public static FrameBoundEvent<bool> AnyKeyClicked = new(_eventsInvoker);
    public static FrameBoundEvent<bool> SkipInputRecieved = new(_eventsInvoker);
    public static FrameBoundEvent<Vector2> MoveInputRecieved = new(_eventsInvoker);
    public static FrameBoundEvent<bool> ActiveAbilityInputRecieved = new(_eventsInvoker);
    public static FrameBoundEvent<bool> MoveAbilityInputRecieved = new(_eventsInvoker);

    static InputReader()
    {
        GameManager.InputBlockChanged += v =>
        {
            if (v) ResetInput();
        };
    }
    #endregion

    private Vector2 _moveInput = new();

    protected abstract bool IsAnyKeyClicked();
    protected abstract bool IsSkipInputRecieved();
    protected abstract Vector2 GetMoveInput();
    protected abstract bool IsActiveAbilityUsed();
    protected abstract bool IsMoveAbilityUsed();

    protected virtual void Update()
    {
        if (IsAnyKeyClicked())
        {
            AnyKeyClicked?.Invoke(_eventsInvoker, true);
        }

        if (IsSkipInputRecieved())
        {
            SkipInputRecieved?.Invoke(_eventsInvoker, true);
        }

        // controls

        if (GameManager.IsControlInputBlocked)
        {
            return;
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

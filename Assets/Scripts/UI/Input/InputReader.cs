using System;
using UnityEngine;

public abstract class InputReader : MonoBehaviour
{
    #region static
    public static bool _isControlInputBlocked;
    public static bool IsControlInputBlocked
    {
        get => _isControlInputBlocked;
        set
        {
            if (_isControlInputBlocked != value)
            {
                if (value) MoveInputRecieved?.Invoke(Vector2.zero);

                InputBlockChanging?.Invoke(value);
                _isControlInputBlocked = value;
            }
        }
    }

    public static event Action<bool> InputBlockChanging;

    public static event Action AnyKeyClicked;
    public static event Action<Vector2> MoveInputRecieved;
    public static event Action ActiveAbilityInputRecieved;
    public static event Action MoveAbilityInputRecieved;
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
            AnyKeyClicked?.Invoke();
        }

        if (TryGetMoveVectorInput(out var moveInput))
        {
            MoveInputRecieved?.Invoke(moveInput);
        }

        if (IsActiveAbilityUsed())
        {
            ActiveAbilityInputRecieved?.Invoke();
        }

        if (IsMoveAbilityUsed())
        {
            MoveAbilityInputRecieved?.Invoke();
        }
    }

    protected bool TryGetMoveVectorInput(out Vector2 input)
    {
        input = GetMoveInput();
        if (_moveInput == input) return false;
        _moveInput = input;
        return true;
    }
}

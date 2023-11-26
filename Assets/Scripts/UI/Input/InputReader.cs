using UnityEngine;

public abstract class InputReader : MonoBehaviour
{
    private Vector2 _moveVectorInput;

    protected abstract Vector2 GetMoveVectorInput();

    protected virtual void Update()
    {
        var newMoveVectorinput = GetMoveVectorInput();
        if (newMoveVectorinput != _moveVectorInput)
        {
            _moveVectorInput = newMoveVectorinput;
            InputManager.ChangeMoveVector(_moveVectorInput);
        }
    }
}

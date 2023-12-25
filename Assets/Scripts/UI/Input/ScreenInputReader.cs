using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenInputReader : InputReader, IPointerDownHandler
{
    [SerializeField]
    private Joystick _joystick;
    private bool _isActiveAbilityUsed;

    protected override Vector2 GetMoveVectorInput()
    {
        return _joystick.InputVector;
    }

    protected override bool IsActiveAbilityUsed()
    {
        var used = _isActiveAbilityUsed;
        _isActiveAbilityUsed = false;
        return used;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isActiveAbilityUsed = Input.touchCount > 1;
    }
}

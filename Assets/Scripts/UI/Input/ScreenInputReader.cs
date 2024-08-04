using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenInputReader : InputReader, IPointerDownHandler
{
    [SerializeField]
    private Joystick _joystick;
    [SerializeField]
    private  float _swipeThreshold = Screen.width / 3f;
    [SerializeField]
    private  float _timeForSwipe = 0.3f;

    private bool _isActiveAbilityUsed;
    private Vector2 swipeStartPosition;
    private float swipeStartTime;

    private bool _isSkipInputRecieved;

    protected override Vector2 GetMoveInput()
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

    protected override bool IsMoveAbilityUsed()
    {
        return CheckForSwipe();
    }

    private bool CheckForSwipe()
    {
        if (!Input.GetMouseButton(0))
        {
            return false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            swipeStartTime = Time.time;
            swipeStartPosition = Input.mousePosition;
            return false;
        }

        return Time.time <= swipeStartTime + _timeForSwipe && Vector2.Distance(swipeStartPosition, Input.mousePosition) >= _swipeThreshold;
    }

    protected override bool IsAnyKeyClicked()
    {
        return Input.touchCount > 0;
    }

    protected override bool IsSkipInputRecieved()
    {
        var returnValue = _isSkipInputRecieved;
        _isSkipInputRecieved = false;
        return returnValue;
    }

    public void RecieveSkipInput()
    {
        _isSkipInputRecieved = true;
    }
}

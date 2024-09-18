using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenInputReader : InputReader, IPointerDownHandler
{
    [SerializeField]
    private Joystick _joystick;
    [SerializeField]
    private float _swipeThreshold = Screen.width / 3f;
    [SerializeField]
    private float _timeForSwipe = 0.3f;
    [SerializeField]
    private float _timeForSecondaryHold = 0.3f;
    [SerializeField]
    private float _timeForDoubleTap = 0.2f;

    private readonly ReadOnceValue<bool> _isSkipInputRecieved = new(false);
    private readonly ReadOnceValue<bool> _gotDoubleTap = new(false);
    private readonly ReadOnceValue<bool> _gotSecondaryHold = new(false);

    private Vector2 _swipeStartPosition;
    private float _swipeStartTime;
    private CooldownCounter _secondaryHoldCooldownCounter;
    private Vector2 _previousTapPosition = Vector2.one * float.NegativeInfinity;
    private CooldownCounter _doubleTapCooldownCounter;

    private void Awake()
    {
        _secondaryHoldCooldownCounter = new(_timeForSecondaryHold);
        _doubleTapCooldownCounter = new(_timeForDoubleTap);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.touchCount > 1)
        {
            if (_secondaryHoldCooldownCounter.TryReset())
            {
                _gotSecondaryHold.Set(true);
            }
        }
        else
        {
            _secondaryHoldCooldownCounter.Reset();
        }
    }

    protected override Vector2 GetMoveInput()
    {
        return _joystick.InputVector;
    }

    protected override bool IsActiveAbilityUsed()
    {
        return _gotDoubleTap;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if ((eventData.position - _previousTapPosition).sqrMagnitude <= 10_000 && !_doubleTapCooldownCounter.TryReset())
        {
            _gotDoubleTap.Set(true);
        }
        _previousTapPosition = eventData.position;
    }

    protected override bool IsMoveAbilityUsed()
    {
        return CheckForSwipe() || _gotSecondaryHold;
    }

    private bool CheckForSwipe()
    {
        if (!Input.GetMouseButton(0))
        {
            return false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _swipeStartTime = Time.time;
            _swipeStartPosition = Input.mousePosition;
            return false;
        }

        return Time.time <= _swipeStartTime + _timeForSwipe && Vector2.Distance(_swipeStartPosition, Input.mousePosition) >= _swipeThreshold;
    }

    protected override bool IsAnyKeyClicked()
    {
        return Input.touchCount > 0;
    }

    protected override bool IsSkipInputRecieved()
    {
        return _isSkipInputRecieved;
    }

    public void RecieveSkipInput()
    {
        _isSkipInputRecieved.Set(true);
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenInputReader : InputReader, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    private Joystick _joystick;
    [SerializeField]
    private float _swipeThreshold = Screen.width / 3f;
    [SerializeField]
    private float _timeForSwipe = 0.3f;
    [SerializeField]
    private float _timeForDoubleTap = 0.2f;

    private readonly ReadOnceValue<bool> _isSkipInputRecieved = new(false);
    private readonly ReadOnceValue<bool> _gotDoubleTap = new(false);

    private readonly Dictionary<int, ScreenTouch> _touches = new();

    private void Awake()
    {
        SessionDataStorage.Observable.SetChangeListener(SessionDataKey.PhaseSkipInputRecieved, RecieveSkipInput);
    }

    protected override void Update()
    {
        base.Update();

        foreach (var inputTouch in Input.touches)
        {
            UpdateTouch(inputTouch.fingerId, inputTouch.position);
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
        if (_touches.Values.Any(x =>
            !x.IsDown &&
            (eventData.position - x.LastPosition).sqrMagnitude <= 10_000 &&
            Time.time - x.StartTime <= _timeForDoubleTap))
        {
            _gotDoubleTap.Set(true);
        }

        _touches[eventData.pointerId] = new(true, eventData.position, eventData.position, Time.time, Time.time);
    }

    // Needs this because Input.touches that is used in Update only applies to touch screen
    public void OnDrag(PointerEventData eventData)
    {
        UpdateTouch(eventData.pointerId, eventData.position);
    }

    private void UpdateTouch(int id, Vector2 position)
    {
        if (_touches.TryGetValue(id, out var touch))
        {
            touch.LastPosition = position;
            touch.LastTime = Time.time;
            _touches[id] = touch;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var touch = _touches[eventData.pointerId];
        touch.IsDown = false;
        _touches[eventData.pointerId] = touch;
    }

    protected override Vector2 GetMoveAbilityInput()
    {
        // 0+ are indices for touches
        //return _touches.Any(x => x.Key > 0 && x.Value.IsDown && x.Value.LastTime - x.Value.StartTime >= _timeForSecondaryHold)
        //    ? GetMoveInput()
        //    : CheckForSwipe();

        return CheckForSwipe();
    }

    private Vector2 CheckForSwipe()
    {
        var touch = _touches.Values.FirstOrDefault(x =>
            x.IsDown &&
            x.LastTime - x.StartTime <= _timeForSwipe &&
            Vector2.Distance(x.LastPosition, x.StartPosition) >= _swipeThreshold);

        return (touch.LastPosition - touch.StartPosition).normalized;
    }

    protected override bool IsAnyKeyClicked()
    {
        return Input.touchCount > 0;
    }

    protected override bool IsBackInputRecieved()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    protected override bool IsSkipInputRecieved()
    {
        return _isSkipInputRecieved;
    }

    public void RecieveSkipInput(object value)
    {
        if (ConvertingUtils.ToBool(value))
        {
            _isSkipInputRecieved.Set(true);
            SessionDataStorage.Observable.Set(SessionDataKey.PhaseSkipInputRecieved, false.ToString());
        }
    }

    private void OnDestroy()
    {
        SessionDataStorage.Observable.RemoveChangeListener(SessionDataKey.PhaseSkipInputRecieved, RecieveSkipInput);
    }

    struct ScreenTouch
    {
        public bool IsDown;
        public Vector2 StartPosition;
        public Vector2 LastPosition;
        public float StartTime;
        public float LastTime;

        public ScreenTouch(bool isDown, Vector2 startPosition, Vector2 currentPosition, float startTime, float lastTime)
        {
            IsDown = isDown;
            StartPosition = startPosition;
            LastPosition = currentPosition;
            StartTime = startTime;
            LastTime = lastTime;
        }
    }
}

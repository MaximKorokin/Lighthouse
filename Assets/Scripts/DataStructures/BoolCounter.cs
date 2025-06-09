using System;

public class BoolCounter
{
    private int _counter;

    public bool Value => _counter > 0;

    public event Action<bool> ValueChanged;

    public BoolCounter(bool initialValue)
    {
        Reset(initialValue);
    }

    public void Set(bool value)
    {
        var oldValue = Value;

        if (value) _counter++;
        else _counter--;

        if (oldValue != Value)
        {
            ValueChanged?.Invoke(Value);
        }
    }

    public void Reset(bool value)
    {
        _counter = value ? 1 : 0;
    }

    public override string ToString() => (Value, _counter).ToString();

    public static implicit operator bool(BoolCounter counter) => counter.Value;
}

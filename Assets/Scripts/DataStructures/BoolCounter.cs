using System;

public class BoolCounter
{
    private int _counter;

    public bool Value => _counter > 0;

    public event Action<bool> ValueChanged;

    public BoolCounter(bool initialValue)
    {
        _counter = initialValue ? 1 : 0;
        ValueChanged = null;
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

    public override string ToString() => Value.ToString();

    public static implicit operator bool(BoolCounter counter) => counter.Value;
}

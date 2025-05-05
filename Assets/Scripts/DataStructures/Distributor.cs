using System.Collections.Generic;
using System.Linq;

public class Distributor<T>
{
    private readonly List<T> _items;
    private readonly DistributionType _distributionType;

    private int _previousItemIndex = -1;

    public Distributor(T[] items, DistributionType distributionType = DistributionType.Queue)
    {
        _items = items.ToList();
        if (_items == null || _items.Count == 0)
        {
            Logger.Error($"{nameof(items)} is empty");
        }
        _distributionType = distributionType;
    }

    public Distributor<T> Add(params T[] items)
    {
        _items.AddRange(items);
        return this;
    }

    public T GetNext()
    {
        if (_distributionType == DistributionType.Queue)
        {
            if (_items.Count > _previousItemIndex + 1)
            {
                _previousItemIndex++;
            }

            return _items[_previousItemIndex];
        }

        Logger.Error($"Unsupported {nameof(DistributionType)} value: {_distributionType}");
        return default;
    }
}

public enum DistributionType
{
    Queue = 0,
}

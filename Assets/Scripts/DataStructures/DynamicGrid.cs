using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DynamicGrid<TItem, TCell> : IEnumerable<Vector2Int>
{
    public readonly float CellSize;

    protected readonly Dictionary<int, Dictionary<int, TCell>> Grid = new();

    public DynamicGrid(float cellSize)
    {
        CellSize = cellSize;
    }

    protected abstract void AddToGrid(Vector2Int point, TItem item);
    protected abstract void RemoveFromGrid(Vector2Int point, TItem item);
    protected abstract TCell GetFromGrid(Vector2Int point);
    protected abstract IEnumerable<TItem> GetCellItems(TCell cell);

    public void AddItem(TItem item, Bounds box)
    {
        var minPoint = GetGridPoints(box.min).ToArray().MinBy(p => p.sqrMagnitude);
        var maxPoint = GetGridPoints(box.max).ToArray().MaxBy(p => p.sqrMagnitude);
        for (int x = minPoint.x; x <= maxPoint.x; x++)
        {
            for (int y = minPoint.y; y <= maxPoint.y; y++)
            {
                AddToGrid(new Vector2Int(x, y), item);
            }
        }
    }

    public void AddItem(TItem item, Vector2 point)
    {
        GetGridPoints(point).ForEach(p => AddToGrid(p, item));
    }

    public void RemoveItem(TItem item, Bounds box)
    {
        GetBoundPoints(box).ForEach(p => RemoveFromGrid(p, item));
    }

    public void RemoveItem(TItem item, Vector2 point)
    {
        GetGridPoints(point).ForEach(p => RemoveFromGrid(p, item));
    }

    public IEnumerable<TItem> GetItems(Vector2 worldPoint)
    {
        return GetGridPoints(worldPoint).SelectMany(p => GetCellItems(GetFromGrid(p))).Distinct();
    }

    public IEnumerable<TItem> GetItems(Bounds box)
    {
        return GetBoundPoints(box).SelectMany(p => GetCellItems(GetFromGrid(p))).Distinct();
    }

    public IEnumerable<Vector2> GetBoundWorldPoints(Bounds box)
    {
        return GetBoundPoints(box).Select(p => (Vector2)p * CellSize);
    }

    protected IEnumerable<Vector2Int> GetBoundPoints(Bounds box)
    {
        var minPoint = GetGridPoints(box.min).MinBy(p => p.sqrMagnitude);
        var maxPoint = GetGridPoints(box.max).MaxBy(p => p.sqrMagnitude);
        for (int x = minPoint.x; x <= maxPoint.x; x++)
        {
            for (int y = minPoint.y; y <= maxPoint.y; y++)
            {
                yield return new Vector2Int(x, y);
            }
        }
    }

    protected IEnumerable<Vector2Int> GetGridPoints(Vector2 worldPoint)
    {
        var modX = worldPoint.x % CellSize;
        var modY = worldPoint.y % CellSize;
        if (modX.EqualsTo(0.5f) && modY.EqualsTo(0.5f))
        {
            var x = Mathf.RoundToInt(worldPoint.x / CellSize);
            var y = Mathf.RoundToInt(worldPoint.y / CellSize);
            yield return new Vector2Int(x, y);
            yield return new Vector2Int(x + 1, y);
            yield return new Vector2Int(x + 1, y + 1);
            yield return new Vector2Int(x, y + 1);
        }
        else if (modX.EqualsTo(0.5f))
        {
            var x = Mathf.RoundToInt(worldPoint.x / CellSize);
            var y = (int)(worldPoint.y / CellSize);
            yield return new Vector2Int(x, y);
            yield return new Vector2Int(x + 1, y);
        }
        else if (modY.EqualsTo(0.5f))
        {
            var x = (int)(worldPoint.x / CellSize);
            var y = Mathf.RoundToInt(worldPoint.y / CellSize);
            yield return new Vector2Int(x, y);
            yield return new Vector2Int(x, y + 1);
        }
        else
        {
            yield return new Vector2Int(Mathf.RoundToInt(worldPoint.x / CellSize), Mathf.RoundToInt(worldPoint.y / CellSize));
        }
    }

    public IEnumerator<Vector2Int> GetEnumerator()
    {
        foreach (var yRow in Grid)
        {
            foreach (var point in yRow.Value)
            {
                yield return new(yRow.Key, point.Key);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
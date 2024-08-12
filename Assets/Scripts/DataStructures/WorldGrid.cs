using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGrid<T> : DynamicGrid<T, HashSet<T>>
{
    public WorldGrid(float cellSize) : base(cellSize) { }

    protected override void AddToGrid(Vector2Int point, T item)
    {
        if (Grid.TryGetValue(point.x, out var yRow))
        {
            if (yRow.TryGetValue(point.y, out var items))
            {
                items.Add(item);
            }
            else
            {
                Grid[point.x].Add(point.y, new HashSet<T>() { item });
            }
        }
        else
        {
            yRow = new Dictionary<int, HashSet<T>>
            {
                { point.y, new HashSet<T>() { item } }
            };
            Grid.Add(point.x, yRow);
        }
    }

    protected override HashSet<T> GetFromGrid(Vector2Int point)
    {
        if (Grid.TryGetValue(point.x, out var yRow))
        {
            if (yRow.TryGetValue(point.y, out var items))
            {
                return items;
            }
        }
        return null;
    }

    protected override void RemoveFromGrid(Vector2Int point, T item)
    {
        if (Grid.TryGetValue(point.x, out var yRow) && yRow.TryGetValue(point.y, out var items))
        {
            items.Remove(item);
        }
    }

    protected override IEnumerable<T> GetCellItems(HashSet<T> cell)
    {
        return cell ?? Enumerable.Empty<T>();
    }
}

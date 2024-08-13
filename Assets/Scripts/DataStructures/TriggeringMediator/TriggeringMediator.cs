using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// T is restricted to MonoBehaviour because of == operator usage
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class TriggeringMediator<T> : MonoBehaviorSingleton<TriggeringMediator<T>> where T : MonoBehaviour
{
    [SerializeField]
    private int _operationsPerFrame = 1000;

    protected readonly WorldGrid<T> WorldGrid = new(1);
    protected readonly Dictionary<T, Vector2> ItemsCachedPositions = new();

    protected readonly DirectedGraph<T> Items = new();
    protected readonly Dictionary<T, TriggerDetectorBase<T>> Listeners = new();

    protected override void Awake()
    {
        base.Awake();

        Items.ConnectionAdded += OnConnectionAdded;
        Items.ConnectionRemoved += OnConnectionRemoved;

        StartCoroutine(TriggersFindingCoroutine());
    }

    private void OnConnectionAdded(T item1, T item2)
    {
        if (Listeners.TryGetValue(item1, out var detector))
        {
            detector.OnTriggerEnter2D(GetCollider(item2));
        }
    }

    private void OnConnectionRemoved(T item1, T item2)
    {
        if (Listeners.TryGetValue(item1, out var detector))
        {
            detector.OnTriggerExit2D(GetCollider(item2));
        }
    }

    private IEnumerator TriggersFindingCoroutine()
    {
        var frameOperations = 0;
        while (true)
        {
            UpdateWorldGrid();

            foreach (var listener in Listeners.ToArray())
            {
                if (listener.Key == null)
                {
                    continue;
                }

                foreach (var item in WorldGrid
                    .GetItems(new Bounds(GetPosition(listener.Key), 2 * GetTriggeringRadius(listener.Key) * Vector2.one))
                    .Concat(Items.GetConnectedItems(listener.Key)).ToArray())
                {
                    if (item.Equals(listener.Key))
                    {
                        continue;
                    }
                    var collider = GetCollider(item);
                    if (collider == null)
                    {
                        continue;
                    }
                    if (HasIntersection(listener.Key, collider))
                    {
                        if (!Items.HasConnection(listener.Key, item))
                        {
                            Items.AddConnection(listener.Key, item);
                        }
                    }
                    else
                    {
                        if (Items.HasConnection(listener.Key, item))
                        {
                            Items.RemoveConnection(listener.Key, item);
                        }
                    }

                    frameOperations++;
                }

                if (frameOperations > _operationsPerFrame)
                {
                    frameOperations = 0;
                    yield return null;
                }
            }
            frameOperations = 0;
            yield return null;
        }
    }

    private void UpdateWorldGrid()
    {
        foreach (var item in Items)
        {
            var position = GetPosition(item);
            if (ItemsCachedPositions.TryGetValue(item, out var cachedPosition))
            {
                if (cachedPosition != position)
                {
                    WorldGrid.RemoveItem(item, cachedPosition);
                    WorldGrid.AddItem(item, GetPosition(item));
                    ItemsCachedPositions[item] = position;
                }
            }
            else
            {
                WorldGrid.AddItem(item, GetPosition(item));
                ItemsCachedPositions[item] = position;
            }
        }
    }

    public virtual void AddItem(T item)
    {
        Items.AddItem(item);
    }

    public virtual void RemoveItem(T item)
    {
        if (ItemsCachedPositions.TryGetValue(item, out var cachedPosition))
        {
            WorldGrid.RemoveItem(item, cachedPosition);
            ItemsCachedPositions.Remove(item);
        }
        Items.RemoveItem(item);
    }

    public virtual void AddListener(T listener, TriggerDetectorBase<T> detector)
    {
        Listeners.TryAdd(listener, detector);
    }

    public virtual void RemoveListener(T listener)
    {
        ResetListenerState(listener);
        Listeners.Remove(listener);
    }

    public virtual void ResetListenerState(T listener)
    {
        Items.ClearConnections(listener, true);
    }

    protected abstract Vector2 GetPosition(T item);
    protected abstract Collider2D GetCollider(T item);
    protected abstract float GetTriggeringRadius(T item);
    protected abstract bool HasIntersection(T item, Collider2D collider);
}

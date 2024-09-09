using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DirectedGraph<T> : IContainsEnumerable<T>
{
    private readonly Dictionary<T, Node> _nodes = new();

    public event Action<T, T> ConnectionAdded;
    public event Action<T, T> ConnectionRemoved;

    private readonly struct Node
    {
        public Node(T item)
        {
            Item = item;
            Nodes = new();
        }

        public readonly T Item;
        /// <summary>
        /// Bool value indicates if node is connected this side
        /// </summary>
        public readonly Dictionary<Node, bool> Nodes;

        public override readonly string ToString() => $"{Item} - {Nodes.Count}";
    }

    public void AddItem(T item)
    {
        if (!_nodes.ContainsKey(item))
        {
            _nodes.Add(item, new Node(item));
        }
    }

    public void RemoveItem(T item)
    {
        ClearConnections(item, true);
        _nodes.Remove(item);
    }

    public void AddConnection(T item1, T item2, bool bothSides = false)
    {
        if (!item1.Equals(item2) && _nodes.TryGetValue(item1, out var node1) && _nodes.TryGetValue(item2, out var node2))
        {
            // Call ConnectionAdded if no connection existed before
            if (!node1.Nodes.TryGetValue(node2, out var node1Connected) || !node1Connected) ConnectionAdded?.Invoke(item1, item2);

            // Call ConnectionAdded if no connection existed before and both sides must be connected
            if (bothSides && (!node2.Nodes.TryGetValue(node1, out var node2Connected) || !node2Connected)) ConnectionAdded?.Invoke(item2, item1);

            // The internal connection always must be two-side
            node1.Nodes[node2] = true;

            // If internal connection exists, then use its value if it is True, else use bothSides value.
            node2.Nodes[node1] = (node2.Nodes.TryGetValue(node1, out var connected) && connected) || bothSides;
        }
    }

    public void RemoveConnection(T item1, T item2, bool bothSides = false)
    {
        if (!item1.Equals(item2) && _nodes.TryGetValue(item1, out var node1) && _nodes.TryGetValue(item2, out var node2))
        {
            RemoveConnectionInternal(node1, node2, bothSides);
        }
    }

    private void RemoveConnectionInternal(Node node1, Node node2, bool bothSides)
    {
        // Checking if connection exists.
        // Single check is enough because two-side internal connection is guaranteed.
        if (!node1.Nodes.ContainsKey(node2))
        {
            return;
        }

        node1.Nodes.TryGetValue(node2, out var node1Connected);
        node2.Nodes.TryGetValue(node1, out var node2Connected);
        // If both connections should be removed or second node is not connected, then remove both connections.
        if (bothSides || !node2Connected)
        {
            node1.Nodes.Remove(node2);
            node2.Nodes.Remove(node1);

            if (node1Connected) ConnectionRemoved?.Invoke(node1.Item, node2.Item);
            if (node2Connected) ConnectionRemoved?.Invoke(node2.Item, node1.Item);
            return;
        }
        // If second node is connected, then just set connection to False.
        node1.Nodes[node2] = false;

        if (node1Connected) ConnectionRemoved?.Invoke(node1.Item, node2.Item);
    }

    public void ClearConnections(T item, bool bothSides = false)
    {
        if (_nodes.TryGetValue(item, out var parentNode))
        {
            foreach (var node in parentNode.Nodes.Keys.ToArray())
            {
                RemoveConnectionInternal(parentNode, node, bothSides);
            }
        }
    }

    public bool HasConnection(T item1, T item2)
    {
        return !item1.Equals(item2)
            && _nodes.TryGetValue(item1, out var node1)
            && _nodes.TryGetValue(item2, out var node2)
            && node1.Nodes.TryGetValue(node2, out var connected)
            && connected;
    }

    public IEnumerable<T> GetConnectedItems(T item)
    {
        return _nodes.TryGetValue(item, out var node)
            ? node.Nodes.Where(x => x.Value).Select(x => x.Key.Item)
            : Enumerable.Empty<T>();
    }

    public bool Contains(T item)
    {
        return _nodes.ContainsKey(item);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _nodes.Keys.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _nodes.Keys.GetEnumerator();
    }
}

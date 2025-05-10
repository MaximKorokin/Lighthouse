using UnityEngine;

public class TransformChildrenSorter
{
    public Transform Transform { get; private set; }

    public TransformChildrenSorter(Transform transform)
    {
        Transform = transform;
    }

    private readonly PrioritizedList<Transform> _elements = new();

    public void SetChild(Transform rect, int priority)
    {
        _elements.Add(rect, priority);
        rect.SetParent(Transform, false);
        rect.SetSiblingIndex(_elements.IndexOf(rect));
    }
}

using UnityEngine;

public class LoadableUIParent : OptionalMonoBehaviorSingleton<LoadableUIParent>
{
    private TransformChildrenSorter _elements;
    private TransformChildrenSorter Elements => this.LazyInitialize(ref _elements, () => new(transform));

    public void SetUIElement(RectTransform rect, int priority)
    {
        Elements.SetChild(rect, priority);
    }
}

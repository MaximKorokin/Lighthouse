using System.Collections.Generic;
using UnityEngine;

public class LoadableUIParent : MonoBehaviorSingleton<LoadableUIParent>
{
    private static readonly SortedList<int, RectTransform> _prioritizedElements = new(new PriorityComparer());

    public static void SetUIElement(RectTransform rect, int priority)
    {
        _prioritizedElements[priority] = rect;
        rect.SetParent(Instance.transform, false);
        rect.SetSiblingIndex(_prioritizedElements.IndexOfValue(rect));
    }

    // In case of conflict Priority, the element should be inserted after all same priority elements
    private class PriorityComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            var comparison = x.CompareTo(y);
            if (comparison == 0) return -1;
            else return comparison;
        }
    }
}

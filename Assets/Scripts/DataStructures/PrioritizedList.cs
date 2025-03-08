using System.Collections;
using System.Collections.Generic;

public class PrioritizedList<T> : IEnumerable<T>
{
    private readonly SortedList<int, T> _prioritizedElements = new(new PriorityComparer());

    public void Add(T element, int priority)
    {
        _prioritizedElements[priority] = element;
    }

    public int IndexOf(T element)
    {
        return _prioritizedElements.IndexOfValue(element);
    }

    public void Clear()
    {
        _prioritizedElements.Clear();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _prioritizedElements.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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

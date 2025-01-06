using System;
using System.Collections;
using System.Collections.Generic;

public static class EnumeratorUtils
{
    public static IEnumerator Yield(Action action)
    {
        action();
        yield return null;
    }

    public static IEnumerator Then(this IEnumerator enumerator1, IEnumerator enumerator2)
    {
        while (enumerator1.MoveNext())
        {
            yield return enumerator1.Current;
        }
        while (enumerator2.MoveNext())
        {
            yield return enumerator2.Current;
        }
    }
    
    public static IEnumerator Then(this IEnumerator enumerator1, Action action)
    {
        while (enumerator1.MoveNext())
        {
            yield return enumerator1.Current;
        }
        action();
    }

    public static IEnumerator Enumerate<T>(this IList<T> list, Func<T, IEnumerator> func)
    {
        for (var i = 0; i < list.Count; i++)
        {
            var enumerator = func(list[i]);

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
    }
}

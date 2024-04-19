using System.Collections;
using System.Text;
using UnityEngine;

public static class Logger
{
    private const string Delimiter = ", ";

    public static void Log(object obj)
    {
        if (obj is string)
        {
            Debug.Log(obj);
        }
        else if (obj is IEnumerable enumerable)
        {
            var sb = new StringBuilder();
            foreach (var element in enumerable)
            {
                sb.Append(element.ToString());
                sb.Append(Delimiter);
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - Delimiter.Length, Delimiter.Length);
            }
            Debug.Log(sb.ToString());
        }
        else
        {
            Debug.Log(obj);
        }
    }

    public static void Log() => Log("");

    public static void Log(object obj, params object[] objects) => Log(obj.YieldWith(objects)); 

    public static void Warn(object obj)
    {
        Debug.LogWarning(obj);
    }

    public static void Error(object obj)
    {
        Debug.LogError(obj);
    }
}

using System.Text;
using System.Collections;
using UnityEngine;

public static class Logger
{
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
                sb.Append(", ");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            Debug.Log(sb.ToString());
        }
        else
        {
            Debug.Log(obj);
        }
    }
}

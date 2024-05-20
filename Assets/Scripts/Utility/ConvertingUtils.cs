using System;

public static class ConvertingUtils
{
    public static bool ToBool(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        var stringObj = obj.ToString();
        if (bool.TryParse(stringObj, out bool result1))
        {
            return result1;
        }
        if (float.TryParse(stringObj, out float result2))
        {
            return result2 != 0;
        }
        return !string.IsNullOrWhiteSpace(stringObj);
    }

    public static float ToFloat(object obj)
    {
        if (obj == null)
        {
            return 0f;
        }
        var stringObj = obj.ToString();
        if (float.TryParse(stringObj, out float result))
        {
            return result;
        }
        return 0f;
    }

    public static int ToInt(object obj)
    {
        if (obj == null)
        {
            return 0;
        }
        var stringObj = obj.ToString();
        if (int.TryParse(stringObj, out int result))
        {
            return result;
        }
        return 0;
    }
}

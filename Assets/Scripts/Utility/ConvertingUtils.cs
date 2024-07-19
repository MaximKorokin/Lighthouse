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
        if (float.TryParse(stringObj, out float result1))
        {
            return result1;
        }
        if (bool.TryParse(stringObj, out bool result2))
        {
            return result2 ? 1 : 0;
        }
        return 0f;
    }
}

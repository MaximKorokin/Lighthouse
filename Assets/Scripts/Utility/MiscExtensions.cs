using UnityEngine;

public static class MiscExtensions
{
    public static string ToHexString(this Color color)
    {
        return $"{((byte)(color.r * 255)):X2}{((byte)(color.g * 255)):X2}{((byte)(color.b * 255)):X2}{((byte)(color.a * 255)):X2}";
    }

    public static float ToFloatValue(this TypingSpeed typingSpeed)
    {
        return typingSpeed switch
        {
            TypingSpeed.Normal => 0.05f,
            TypingSpeed.Slow => 0.1f,
            TypingSpeed.Fast => 0.01f,
            TypingSpeed.Instant => 0f,
            _ => 0,
        };
    }
}

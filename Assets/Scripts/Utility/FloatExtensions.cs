using System;

public static class FloatExtensions
{
    public const double Epsilon = 0.000001f;

    public static bool EqualsTo(this double num1, double num2, double epsilon = Epsilon)
    {
        return Math.Abs(num1 - num2) < epsilon;
    }
    public static bool EqualsTo(this float num1, float num2, double epsilon = Epsilon) => EqualsTo((double)num1, num2, epsilon);

    public static bool GreaterThan(this double num1, double num2, double epsilon = Epsilon)
    {
        return (double)num1 - num2 > epsilon;
    }
    public static bool GreaterThan(this float num1, float num2, double epsilon = Epsilon) => GreaterThan((double)num1, num2, epsilon);

    public static bool LessThan(this double num1, double num2, double epsilon = Epsilon)
    {
        return (double)num2 - num1 > epsilon;
    }
    public static bool LessThan(this float num1, float num2, double epsilon = Epsilon) => LessThan((double)num1, num2, epsilon);

    public static bool LessOrEqualsTo(this double num1, double num2, double epsilon = Epsilon)
    {
        return !GreaterThan(num1, num2, epsilon);
    }
    public static bool LessOrEqualsTo(this float num1, float num2, double epsilon = Epsilon) => LessOrEqualsTo((double)num1, num2, epsilon);

    public static bool GreaterOrEqualsTo(this double num1, double num2, double epsilon = Epsilon)
    {
        return !LessThan(num1, num2, epsilon);
    }
    public static bool GreaterOrEqualsTo(this float num1, float num2, double epsilon = Epsilon) => GreaterOrEqualsTo((double)num1, num2, epsilon);

    public static double RestrictFloatLength(this double num, double epsilon = Epsilon)
    {
        return (float)(((int)(num / epsilon)) * epsilon);
    }
    public static float RestrictFloatLength(this float num, double epsilon = Epsilon) => (float)RestrictFloatLength((double)num, epsilon);
}

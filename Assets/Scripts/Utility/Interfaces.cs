public interface ICopyable<in T>
{
    void CopyTo(T obj);
}

public interface IClonable<out T>
{
    T Clone();
}
using System;

public interface IInitializable<T>
{
    event Action<T> Initialized;

    void Initialize();
}

using System;
using UnityEngine;

public interface ICopyable<in T>
{
    void CopyTo(T obj);
}

public interface IClonable<out T>
{
    T Clone();
}

public interface IInitializable<T>
{
    event Action<T> Initialized;

    void Initialize();
}

public interface IEditorIcon
{
    string IconName { get; }
    Color IconColor { get; }
}

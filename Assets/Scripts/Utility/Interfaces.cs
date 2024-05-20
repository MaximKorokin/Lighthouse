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

public interface IInitializable
{
    void Initialize();
}

public interface IInitializable<T> : IInitializable
{
    event Action<T> Initialized;
}

public interface IEditorIcon
{
    string IconName { get; }
    Color IconColor { get; }
}

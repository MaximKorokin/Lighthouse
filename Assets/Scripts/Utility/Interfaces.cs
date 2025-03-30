using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICopyable<in T>
{
    void CopyTo(T obj);
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

public interface IContainsEnumerable<T> : IEnumerable<T>
{
    bool Contains(T item);
}

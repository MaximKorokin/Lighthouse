using System;
using UnityEngine;

public abstract class ActPhase : MonoBehaviour, IEditorIcon
{
    public event Action<ActPhase> Ended;

    protected void InvokeEnded()
    {
        Ended?.Invoke(this);
    }

    public abstract void Invoke();

    public virtual string IconName => "Empty.png";
    public virtual Color IconColor => Color.white;
}

using System;
using UnityEngine;

public abstract class ActRequirement : MonoBehaviour, IEditorIcon
{
    public event Action<ActRequirement> OnFulfilled;

    protected void InvokeFulfilled()
    {
        OnFulfilled?.Invoke(this);
    }

    public abstract bool IsFulfilled();

    public virtual string IconName => null;
    public virtual Color IconColor => Color.white;
}

using System;
using UnityEngine;

public abstract class ActRequirement : MonoBehaviour
{
    public event Action<ActRequirement> OnFulfilled;

    protected void InvokeFulfilled()
    {
        OnFulfilled?.Invoke(this);
    }

    public abstract bool IsFulfilled();
}

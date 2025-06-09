using System;
using System.Collections;
using UnityEngine;

public class Item : MovableWorldObject
{
    [field: SerializeField]
    public float InactiveTime { get; set; }
    public bool IsActive { get; set; }

    public event Action Activated;

    protected override void Awake()
    {
        base.Awake();
        IsActive = InactiveTime <= 0;
        if (!IsActive)
        {
            this.StartCoroutineSafe(InactiveCoroutine());
        }
    }

    private IEnumerator InactiveCoroutine()
    {
        yield return new WaitForSeconds(InactiveTime);
        IsActive = true;
        Activated?.Invoke();
    }
}

using System.Collections;
using UnityEngine;

public class TemporaryWorldObject : MovableWorldObject
{
    [field: SerializeField]
    public float LifeTime { get; set; }

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(LifeTimeCoroutine());
    }

    private IEnumerator LifeTimeCoroutine()
    {
        yield return new WaitForSeconds(LifeTime);
        DestroyWorldObject();
    }
}

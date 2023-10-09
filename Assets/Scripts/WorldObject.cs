using System;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    [field: SerializeField]
    public Stats0 Stats { get; protected set; }

    protected void Start()
    {
        Debug.Log(Stats.SizeScale);
        Stats.Changed += OnStatsChange;
    }
}

[Serializable]
public class Stats0
{
    [field: SerializeField]
    public float SizeScale { get; private set; }

    public event Action<Stats0> Changed;
}

[Serializable]
public class Stats1 : Stats0
{
    [field: SerializeField]
    public float HealthPoints { get; private set; }
}

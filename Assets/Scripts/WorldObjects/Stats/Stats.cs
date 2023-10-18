using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats
{
    [SerializeField]
    private Stat[] _stats;
    private Dictionary<StatName, float> _statsDictionary;

    public void Init()
    {
        _statsDictionary = _stats.ToDictionary(x => x.Name, x => x.Value);
    }

    public void Modify(Stats other)
    {
        if (_statsDictionary == null || other._statsDictionary == null)
        {
            return;
        }

        foreach (var statName in _statsDictionary.Keys.Where(x => other._statsDictionary.ContainsKey(x)))
        {
            _statsDictionary[statName] += other._statsDictionary[statName];
        }
    }

    public float this[StatName name]
    {
        get
        {
            _statsDictionary.TryGetValue(name, out var statValue);
            return statValue;
        }
        set
        {
            _statsDictionary[name] = value;
        }
    }
}

[Serializable]
public struct Stat
{
    [SerializeField]
    public StatName Name;
    [SerializeField]
    public float Value;
}

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
    private Dictionary<StatName, float> StatsDictionary
    {
        get
        {
            if (_statsDictionary == null)
            {
                _statsDictionary = _stats.ToDictionary(x => x.Name, x => x.Value);
            }
            return _statsDictionary;
        }
    }

    public void Modify(Stats other)
    {
        if (StatsDictionary == null || other.StatsDictionary == null)
        {
            return;
        }

        foreach (var statName in StatsDictionary.Keys.Where(x => other.StatsDictionary.ContainsKey(x)).ToArray())
        {
            StatsDictionary[statName] += other.StatsDictionary[statName];
        }
    }

    public float this[StatName name]
    {
        get
        {
            StatsDictionary.TryGetValue(name, out var statValue);
            return statValue;
        }
        set
        {
            StatsDictionary[name] = value;
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

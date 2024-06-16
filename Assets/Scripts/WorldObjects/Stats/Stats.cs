using System;
using System.Collections.Generic;
using System.Linq;
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

    public event Action Modified;

    public void Modify(Stats other, StatsModificationType modificationType)
    {
        if (StatsDictionary == null || other == null)
        {
            return;
        }

        foreach (var statName in StatsDictionary.Keys.Where(x => other.StatsDictionary.ContainsKey(x)).ToArray())
        {
            ModifyStat(statName, other[statName], modificationType);
        }

        Modified?.Invoke();
    }

    private void ModifyStat(StatName statName, float statValue, StatsModificationType modificationType)
    {
        switch (modificationType)
        {
            case StatsModificationType.Set:
                StatsDictionary[statName] = statValue;
                break;
            case StatsModificationType.Increment:
                StatsDictionary[statName] += statValue;
                break;
            case StatsModificationType.Decrement:
                StatsDictionary[statName] -= statValue;
                break;
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
            Modified?.Invoke();
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

public enum StatsModificationType
{
    Set,
    Increment,
    Decrement,
}

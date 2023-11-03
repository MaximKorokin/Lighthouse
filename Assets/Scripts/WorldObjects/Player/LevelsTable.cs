using System;
using UnityEngine;

/// <summary>
/// Index 0 exp means value to reach level 0 to level 1 and so on
/// </summary>
[Serializable]
public class LevelsTable
{
    [SerializeField]
    private int[] _levelsExperience;

    public int GetExpereinceNeeded(int level, int exp)
    {
        if (level > _levelsExperience.Length)
        {
            return int.MaxValue;
        }
        return _levelsExperience[level] - exp;
    }
}

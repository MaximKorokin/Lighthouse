using System;
using UnityEngine;

public class PlayerCreature : Creature
{
    [SerializeField]
    private LevelsTable _levelsTable;

    public int Level { get; private set; }

    public virtual float AutoLootRange => Stats[StatName.AutoLootRange] * Stats[StatName.SizeScale];

    public event Action LevelIncreased;

    private float _currentExperience;

    public void AddExperience(float expValue)
    {
        var expToUp = _levelsTable.GetExpereinceNeeded(Level, (int)_currentExperience);
        var expDelta = expToUp - expValue;
        if (expDelta <= 0)
        {
            Level++;
            LevelIncreased?.Invoke();
            AddExperience(-expDelta);
        }
        else
        {
            _currentExperience += expValue;
        }
    }

    protected override void OnStatsModified()
    {
        base.OnStatsModified();

        var autoLootCollider = Array.Find(GetComponents<CircleCollider2D>(), x => x.isTrigger);
        if (autoLootCollider != null)
        {
            autoLootCollider.radius = AutoLootRange;
        }
    }
}

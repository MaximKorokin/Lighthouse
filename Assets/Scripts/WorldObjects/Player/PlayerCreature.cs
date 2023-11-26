using System;
using UnityEngine;

public class PlayerCreature : Creature
{
    [SerializeField]
    private LevelingSystemSettings _levelingSystemSettings;

    public LevelingSystem LevelingSystem { get; private set; }

    public virtual float AutoLootRange => Stats[StatName.AutoLootRange] * Stats[StatName.SizeScale];

    protected override void Start()
    {
        base.Start();
        LevelingSystem = new LevelingSystem(_levelingSystemSettings);
        LevelingSystem.LevelIncreased += OnLevelIncreased;
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

    private void OnLevelIncreased(Effect effect)
    {
        effect.Invoke(new CastState(this));
    }
}

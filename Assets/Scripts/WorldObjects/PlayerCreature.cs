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

        var colliders = GetComponents<CircleCollider2D>();
        var autoLootCollider = Array.Find(colliders, x => x.isTrigger && (x.includeLayers & LayerMask.GetMask(Constants.PlayerLootingLayerName)) != 0);
        if (autoLootCollider != null)
        {
            autoLootCollider.radius = AutoLootRange;
        }

        var actionRangeCollider = Array.Find(colliders, x => x.isTrigger && x != autoLootCollider);
        if (actionRangeCollider != null)
        {
            actionRangeCollider.radius = ActionRange;
        }
    }

    private void OnLevelIncreased(Effect[] effects)
    {
        effects.Invoke(this);
    }
}

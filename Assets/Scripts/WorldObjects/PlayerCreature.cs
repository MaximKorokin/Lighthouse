using System.Linq;
using UnityEngine;

public class PlayerCreature : Creature
{
    [SerializeField]
    private LevelingSystemSettings _levelingSystemSettings;

    private LevelingSystem _levelingSystem;
    public LevelingSystem LevelingSystem => _levelingSystem ??= InitializeLevelingSystem();
    public virtual float AutoLootRange => Stats[StatName.AutoLootRange] * Stats[StatName.SizeScale];

    protected override void OnStatsModified()
    {
        base.OnStatsModified();

        var autoLootCollider = Colliders?.FirstOrDefault(x => x.isTrigger && (x.includeLayers & LayerMask.GetMask(Constants.PlayerLootingLayerName)) != 0);
        if (autoLootCollider != null && autoLootCollider is CircleCollider2D circleCollider)
        {
            circleCollider.radius = AutoLootRange;
        }
    }

    private LevelingSystem InitializeLevelingSystem()
    {
        var levelingSystem = new LevelingSystem(_levelingSystemSettings);
        levelingSystem.LevelIncreased += OnLevelIncreased;
        return levelingSystem;
    }

    private void OnLevelIncreased(Effect[] effects)
    {
        effects.Invoke(this);
    }
}

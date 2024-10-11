using System.Linq;
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
        if (_levelingSystemSettings != null)
        {
            LevelingSystem = new LevelingSystem(_levelingSystemSettings);
            LevelingSystem.LevelIncreased += OnLevelIncreased;
        }
    }

    protected override void OnStatsModified()
    {
        base.OnStatsModified();

        var autoLootCollider = Colliders?.FirstOrDefault(x => x.isTrigger && (x.includeLayers & LayerMask.GetMask(Constants.PlayerLootingLayerName)) != 0);
        if (autoLootCollider != null && autoLootCollider is CircleCollider2D circleCollider)
        {
            circleCollider.radius = AutoLootRange;
        }
    }

    private void OnLevelIncreased(Effect[] effects)
    {
        effects.Invoke(this);
    }
}

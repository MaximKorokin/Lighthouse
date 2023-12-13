using UnityEngine;

public class PlayerInputActor : SkilledActor
{
    [SerializeField]
    private EffectSettings _activeEffectSettings;

    private Skill _activeSkill;
    private bool _canUseActiveSkill;

    protected override void Awake()
    {
        base.Awake();
        if (_activeEffectSettings != null)
        {
            _activeSkill = new Skill(_activeEffectSettings);
        }
        InputManager.ActiveAbilityUsed += OnActiveAbilityUsed;
    }

    public override void Act(WorldObject worldObject)
    {
        base.Act(worldObject);
        if (!(WorldObject as DestroyableWorldObject).IsAlive)
        {
            return;
        }
        if (_canUseActiveSkill)
        {
            _canUseActiveSkill = false;
            if (_activeSkill.CanUse())
            {
                _activeSkill.Invoke(WorldObject);
            }
        }
    }

    private void OnActiveAbilityUsed()
    {
        _canUseActiveSkill = true;
    }

    private void OnDestroy()
    {
        InputManager.ActiveAbilityUsed -= OnActiveAbilityUsed;
    }
}
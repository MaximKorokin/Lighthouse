using UnityEngine;

public class PlayerInputActor : SkilledActor
{
    [SerializeField]
    private EffectSettings _activeEffectSettings;

    public Skill ActiveSkill { get; private set; }
    private bool _canUseActiveSkill;

    protected override void Awake()
    {
        base.Awake();
        if (_activeEffectSettings != null)
        {
            ActiveSkill = new Skill(_activeEffectSettings);
        }
        InputManager.ActiveAbilityUsed += OnActiveAbilityUsed;
    }

    public void UseActiveSkill(WorldObject worldObject)
    {
        if (_canUseActiveSkill)
        {
            _canUseActiveSkill = false;

            ActiveSkill.Invoke(worldObject);
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

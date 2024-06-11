using UnityEngine;

public class PlayerInputActor : SkilledActor
{
    [SerializeField]
    private EffectSettings _activeEffectSettings;
    [SerializeField]
    private EffectSettings _moveEffectSettings;

    public Skill ActiveSkill { get; private set; }
    public Skill MoveSkill { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (_activeEffectSettings != null)
        {
            ActiveSkill = new Skill(_activeEffectSettings);
            InputReader.ActiveAbilityInputRecieved += ActiveSkill.OnInputRecieved;
        }
        if (_moveEffectSettings != null)
        {
            MoveSkill = new Skill(_moveEffectSettings);
            InputReader.MoveAbilityInputRecieved += MoveSkill.OnInputRecieved;
        }
    }

    public void UseActiveSkill(WorldObject worldObject)
    {
        ActiveSkill.Invoke(worldObject);
    }

    public void UseMoveSkill(WorldObject worldObject)
    {
        MoveSkill.Invoke(worldObject);
    }

    private void OnDestroy()
    {
        if (ActiveSkill != null) InputReader.ActiveAbilityInputRecieved -= ActiveSkill.OnInputRecieved;
        if (MoveSkill != null) InputReader.MoveAbilityInputRecieved -= MoveSkill.OnInputRecieved;
    }
}

using UnityEngine;

public class SkillEffect : Effect
{
    [field: SerializeField]
    public Skill Skill { get; private set; }

    public override void Invoke(CastState castState)
    {
        if (castState.Target.TryGetComponent<SkilledActor>(out var actor))
        {
            Skill.Initialize();
            actor.AddSkill(Skill);
        }
    }
}

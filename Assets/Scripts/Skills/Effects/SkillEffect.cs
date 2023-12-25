using UnityEngine;

public class SkillEffect : Effect
{
    [field: SerializeField]
    public EffectSettings Skill { get; private set; }

    public override void Invoke(CastState castState)
    {
        var actor = castState.Target.GetComponent<SkilledActor>();
        if (actor != null)
        {
            actor.AddSkill(Skill);
        }
    }
}

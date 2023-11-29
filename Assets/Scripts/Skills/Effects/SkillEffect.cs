using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffect", menuName = "ScriptableObjects/Effects/SkillEffect", order = 1)]
public class SkillEffect : Effect
{
    [field: SerializeField]
    public Skill Skill { get; private set; }

    public override void Invoke(CastState castState)
    {
        var actor = castState.Target.GetComponent<SkilledActor>();
        if (actor != null)
        {
            actor.AddSkill(Skill);
        }
    }
}

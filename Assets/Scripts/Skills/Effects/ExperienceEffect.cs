using UnityEngine;

[CreateAssetMenu(fileName = "ExperienceEffect", menuName = "ScriptableObjects/Effects/ExperienceEffect", order = 1)]
public class ExperienceEffect : SimpleEffect
{
    public override void Invoke(CastState castState)
    {
        if (castState.Target is PlayerCreature playerCreature)
        {
            playerCreature.AddExperience(Value);
        }
    }
}

using UnityEngine;

public class ExperienceEffect : SimpleEffect
{
    public override void Invoke(CastState castState)
    {
        if (castState.Target is PlayerCreature playerCreature)
        {
            playerCreature.LevelingSystem.AddExperience(Value);
        }
    }
}

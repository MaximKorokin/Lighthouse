public class ExperienceEffect : SimpleValueEffect
{
    public override void Invoke(CastState castState)
    {
        if (castState.Target is PlayerCreature playerCreature)
        {
            playerCreature.LevelingSystem.AddExperience((int)Value);
        }
    }
}

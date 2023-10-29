using UnityEngine;

public class PlayerCreature : Creature
{
    public int Level { get; private set; }

    private float _currentExperience;

    public void AddExperience(float expValue)
    {
        _currentExperience += expValue;
    }
}

using UnityEngine;

public class PlayerCreature : Creature
{
    [field: SerializeField]
    public int Level { get; protected set; }

    public void AddExperience(int expValue)
    {

    }
}

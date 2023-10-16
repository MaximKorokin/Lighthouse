using UnityEngine;

public class LevelingCreature : Creature
{
    [field: SerializeField]
    public int Level { get; protected set; }

    [field: SerializeField]
    public float AutoLootRadius { get; protected set; }

    [field: SerializeField]
    public string[] /*Skill[]*/ Skills { get; protected set; }

    public void AddExperience(int expValue)
    {

    }

    public void AddSkill(string /*Skill*/ skill)
    {

    }
}

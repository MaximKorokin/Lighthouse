using UnityEngine;

public class SkillItem : Item
{
    [field: SerializeField]
    public string /*Skill*/ Skill { get; protected set; }
}

using UnityEngine;

[CreateAssetMenu(fileName = "LevelingSystemSettings", menuName = "ScriptableObjects/Settings/LevelingSystemSettings", order = 1)]
public class LevelingSystemSettings : ScriptableObject
{
    [field: SerializeField]
    public int AlternativesAmount { get; set; }
    [field: SerializeField]
    private int MaxEffectLevel { get; set; }
    /// <summary>
    /// Index 0 means value to reach from level 0 to level 1 and so on
    /// </summary>
    [field: SerializeField]
    public int[] LevelsExperience { get; set; }
    [field: SerializeField]
    public Effect[] Effects { get; set; }
}

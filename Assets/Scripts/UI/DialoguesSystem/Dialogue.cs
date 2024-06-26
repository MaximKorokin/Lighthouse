using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Settings/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    [field: SerializeField]
    public Speech[] Speeches { get; private set; }
}

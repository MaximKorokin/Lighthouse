using UnityEngine;

public class UIAwaker : MonoBehaviour
{
    private void Awake()
    {
        var levelingSystem = GetComponentInChildren<LevelingSystemUI>(true);
        if (levelingSystem != null)
        {
            levelingSystem.Initialize();
        }
    }
}

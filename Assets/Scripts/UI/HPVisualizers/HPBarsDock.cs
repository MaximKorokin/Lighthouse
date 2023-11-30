using UnityEngine;

public class HPBarsDock : MonoBehaviour
{
    public static HPBarsDock Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}

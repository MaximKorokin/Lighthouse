using UnityEngine;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Reload()
    {
        Game.ReloadScene();
    }
}

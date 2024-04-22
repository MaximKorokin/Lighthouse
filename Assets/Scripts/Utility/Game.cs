using UnityEngine;
using UnityEngine.SceneManagement;

public static class Game
{
    public static bool IsPaused { get ; private set; }

    public static void Pause()
    {
        Time.timeScale = 0;
        IsPaused = true;
    }

    public static void Resume()
    {
        Time.timeScale = 1;
        IsPaused = false;
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

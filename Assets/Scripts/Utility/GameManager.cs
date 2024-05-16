using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    private static int _pauseCalls;
    private static int PauseCalls { get => _pauseCalls; set => _pauseCalls = value < 0 ? 0 : value; }

    public static bool IsPaused { get ; private set; }

    public static void Pause()
    {
        PauseCalls++;
        Time.timeScale = 0;
        IsPaused = true;
        InputManager.IsControlInputBlocked = true;
    }

    public static void Resume()
    {
        PauseCalls--;
        if (PauseCalls <= 0)
        {
            Time.timeScale = 1;
            IsPaused = false;
            InputManager.IsControlInputBlocked = false;
        }
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

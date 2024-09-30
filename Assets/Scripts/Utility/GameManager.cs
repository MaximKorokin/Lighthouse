using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static BoolCounter IsPaused { get ; private set; }

    public static event Action Paused;
    public static event Action Resumed;

    public static void Pause()
    {
        Time.timeScale = 0;
        IsPaused.Set(true);
        InputReader.IsControlInputBlocked = true;

        Paused?.Invoke();
    }

    public static void Resume()
    {
        IsPaused.Set(false);
        if (!IsPaused)
        {
            Time.timeScale = 1;
            InputReader.IsControlInputBlocked = false;

            Resumed?.Invoke();
        }
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

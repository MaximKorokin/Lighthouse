using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    private static readonly BoolCounter _isPaused = new(false);
    public static bool IsPaused => _isPaused;

    public static event Action Paused;
    public static event Action Resumed;

    public static void Pause()
    {
        _isPaused.Set(true);
        if (IsPaused)
        {
            Time.timeScale = 0;
            InputReader.IsControlInputBlocked = true;

            Paused?.Invoke();
        }
    }

    public static void Resume()
    {
        _isPaused.Set(false);
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

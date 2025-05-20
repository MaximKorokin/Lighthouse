using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    private static readonly BoolCounter _isPaused = new(false);
    public static bool IsPaused => _isPaused;

    private static readonly BoolCounter _isControlInputBlocked = new(false);
    public static bool IsControlInputBlocked
    {
        get => _isControlInputBlocked;
        set => _isControlInputBlocked.Set(value);
    }

    public static event Action Paused;
    public static event Action Resumed;
    public static event Action SceneChanging;
    public static event Action<bool> InputBlockChanged;

    static GameManager()
    {
        _isControlInputBlocked.ValueChanged += v =>
        {
            InputBlockChanged?.Invoke(v);
        };
        SceneChanging += () =>
        {
            _isPaused.Reset(false);
            _isControlInputBlocked.Reset(false);
        };
    }

    public static void Pause()
    {
        _isPaused.Set(true);
        IsControlInputBlocked = true;
        if (IsPaused)
        {
            Time.timeScale = 0;
            Paused?.Invoke();
        }
    }

    public static void Resume()
    {
        _isPaused.Set(false);
        IsControlInputBlocked = false;
        if (!IsPaused)
        {
            Time.timeScale = 1;
            Resumed?.Invoke();
        }
    }

    public static void LoadScene(Constants.Scene sceneName, MonoBehaviour behaviour, Action finalAction = null)
    {
        SceneChanging?.Invoke();
        var operation = SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Single);
        behaviour.StartCoroutineSafe(CoroutinesUtils.WaitForAsyncOperation(operation), finalAction);
    }

    public static void ReloadScene()
    {
        SceneChanging?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

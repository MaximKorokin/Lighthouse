using System;
using UnityEngine;

public class GameManagerEventRequirement : ActRequirement
{
    [SerializeField]
    private GameManagerEvent _gameManagerEvent;

    private void Awake()
    {
        GameManager.Paused += OnPaused;
        GameManager.Resumed += OnResumed;
    }

    private void OnPaused()
    {
        if (_gameManagerEvent.HasFlag(GameManagerEvent.Paused))
        {
            InvokeFulfilled();
        }
    }

    private void OnResumed()
    {
        if (_gameManagerEvent.HasFlag(GameManagerEvent.Resumed))
        {
            InvokeFulfilled();
        }
    }

    public override bool IsFulfilled()
    {
        return (GameManager.IsPaused && _gameManagerEvent.HasFlag(GameManagerEvent.Paused))
            || (!GameManager.IsPaused && _gameManagerEvent.HasFlag(GameManagerEvent.Resumed));
    }

    private void OnDestroy()
    {
        GameManager.Paused -= OnPaused;
        GameManager.Resumed -= OnResumed;
    }

    [Flags]
    private enum GameManagerEvent
    {
        Paused = 1,
        Resumed = 2,
    }

    public override string IconName => _gameManagerEvent switch
    {
        GameManagerEvent.Paused => "Pause.png",
        GameManagerEvent.Resumed => "Play.png",
        _ => base.IconName,
    };
}

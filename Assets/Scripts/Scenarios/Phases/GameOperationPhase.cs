using UnityEngine;

public class GameOperationPhase : ActPhase
{
    [SerializeField]
    private GameOperation _operation;

    public override void Invoke()
    {
        switch (_operation)
        {
            case GameOperation.Pause:
                GameManager.Pause();
                break;
            case GameOperation.Resume:
                GameManager.Resume();
                break;
        }
        InvokeFinished();
    }

    public override string IconName => _operation switch
    {
        GameOperation.Pause => "Pause.png",
        GameOperation.Resume => "Play.png",
        _ => "Act.png"
    };
}

public enum GameOperation
{
    None = 0,
    Pause,
    Resume,
}

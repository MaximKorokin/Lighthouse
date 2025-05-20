using UnityEngine;

public class ScenePhase : ActPhase
{
    [SerializeField]
    private Constants.Scene _sceneName;

    public override void Invoke()
    {
        GameManager.LoadScene(_sceneName, this, InvokeFinished);
    }

    public override string IconName => "Door.png";
}

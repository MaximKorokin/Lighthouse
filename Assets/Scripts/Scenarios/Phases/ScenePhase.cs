using UnityEngine;

public class ScenePhase : ActPhase
{
    [SerializeField]
    private Constants.Scene _sceneName;

    public override void Invoke()
    {
        ScenesUtils.Load(_sceneName, this, InvokeFinished);
    }

    public override string IconName => base.IconName;
}

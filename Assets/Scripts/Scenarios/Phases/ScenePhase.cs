using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePhase : ActPhase
{
    [SerializeField]
    private Constants.Scenes _sceneName;

    public override void Invoke()
    {
        var operation = SceneManager.LoadSceneAsync(_sceneName.ToString(), LoadSceneMode.Single);
        this.StartCoroutineSafe(CoroutinesUtils.WaitForAsyncOperation(operation), InvokeFinished);
    }

    public override string IconName => base.IconName;
}

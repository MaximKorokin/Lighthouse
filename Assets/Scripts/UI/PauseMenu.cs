using UnityEngine;

public class PauseMenu : MonoBehaviorSingleton<PauseMenu>
{
    [SerializeField]
    private GameObject _pausePanel;

    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
    }

    public void EnterPause()
    {
        GameManager.Pause();
        _pausePanel.SetActive(true);
    }

    public void ExitPause()
    {
        GameManager.Resume();
        _pausePanel.SetActive(false);
    }

    public void ReloadScene()
    {
        GameManager.ReloadScene();
    }

    public void SwitchLanguage()
    {
        if (LocalizationManager.Language == SystemLanguage.Russian)
            LocalizationManager.ChangeLanguage(SystemLanguage.English);
        else
            LocalizationManager.ChangeLanguage(SystemLanguage.Russian);
    }
}

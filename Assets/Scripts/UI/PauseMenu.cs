using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pausePanel;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void EnterPause()
    {
        Game.Pause();
        _pausePanel.SetActive(true);
    }

    public void ExitPause()
    {
        Game.Resume();
        _pausePanel.SetActive(false);
    }

    public void ToggleFpsCounter()
    {

    }

    public void ToggleDebugButtons()
    {

    }

    public void ToggleAudio()
    {

    }

    public void ReloadScene()
    {
        Game.ReloadScene();
    }

    public void SwitchLanguage()
    {
        if (LocalizationManager.Language == SystemLanguage.Russian)
            LocalizationManager.ChangeLanguage(SystemLanguage.English);
        else
            LocalizationManager.ChangeLanguage(SystemLanguage.Russian);
    }
}

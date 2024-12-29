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

    private void Start()
    {
        UIStateManager.Observable.SetChangeListener(UIState.Pause, OnPauseStateChanged);
    }

    private void OnPauseStateChanged(bool value)
    {
        if (value) GameManager.Pause();
        else GameManager.Resume();
    }

    private void OnDestroy()
    {
        UIStateManager.Observable.RemoveChangeListener(UIState.Pause, OnPauseStateChanged);
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

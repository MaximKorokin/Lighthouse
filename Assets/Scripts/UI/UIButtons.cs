using UnityEngine;

public class UIButtons : MonoBehaviour
{
    private const UIState UIStatePauseKey = UIState.Pause;

    private void Start()
    {
        Application.targetFrameRate = 60;

        InputReader.BackInputRecieved += OnBackInputRecieved;
        UIStateManager.Observable.SetChangeListener(UIStatePauseKey, OnPauseStateChanged);
    }

    private void OnBackInputRecieved(bool value)
    {
        UIStateManager.Observable.Set(UIStatePauseKey, !UIStateManager.Observable.Get(UIStatePauseKey));
    }

    private void OnPauseStateChanged(bool value)
    {
        if (value) GameManager.Pause();
        else GameManager.Resume();
    }

    private void OnDestroy()
    {
        InputReader.BackInputRecieved -= OnBackInputRecieved;
        UIStateManager.Observable.RemoveChangeListener(UIStatePauseKey, OnPauseStateChanged);
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

    public void SkipPhase()
    {
        SessionDataStorage.Observable.Set(SessionDataKey.PhaseSkipInputRecieved, true.ToString());
    }

    public void LoadSceneLoader()
    {
        GameManager.LoadScene(Constants.Scene.DebugLoader, this);
    }
}

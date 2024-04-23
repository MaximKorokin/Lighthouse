using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Reload()
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

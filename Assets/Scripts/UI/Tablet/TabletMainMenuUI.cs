using UnityEngine;

public class TabletMainMenuUI : MonoBehaviour
{
    public void OpenMails()
    {
        SessionDataStorage.Observable.Set(SessionDataKey.TabletState, TabletState.Mails.ToString());
    }

    public void OpenCopypastas()
    {
        SessionDataStorage.Observable.Set(SessionDataKey.TabletState, TabletState.Copypastas.ToString());
    }

    public void OpenGames()
    {
        SessionDataStorage.Observable.Set(SessionDataKey.TabletState, TabletState.Games.ToString());
    }
}

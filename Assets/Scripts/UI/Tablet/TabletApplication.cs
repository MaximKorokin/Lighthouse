using UnityEngine;

public abstract class TabletApplication : MonoBehaviour
{
    public virtual void Back()
    {
        SessionDataStorage.Observable.Set(SessionDataKey.TabletState, TabletState.MainMenu.ToString());
    }
}

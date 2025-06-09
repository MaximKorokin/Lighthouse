using UnityEngine;

public class UIStateController : MonoBehaviour
{
    [SerializeField]
    private UIState _state;

    public void SetTrue()
    {
        UIStateManager.Observable.Set(_state, true);
    }

    public void SetFalse()
    {
        UIStateManager.Observable.Set(_state, false);
    }
}

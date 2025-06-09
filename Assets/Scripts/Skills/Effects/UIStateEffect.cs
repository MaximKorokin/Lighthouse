using UnityEngine;

public class UIStateEffect : Effect
{
    [SerializeField]
    private UIState _uiState;
    [SerializeField]
    private bool _enable;

    public override void Invoke(CastState castState)
    {
        UIStateManager.Observable.Set(_uiState, _enable);
    }
}

using System;
using System.Linq;
using UnityEngine;

public class TabletUI : MonoBehaviour
{
    [SerializeField]
    private LoadableUIProvider _mainMenuProvider;
    [SerializeField]
    private LoadableUIProvider _mailsProvider;
    [SerializeField]
    private LoadableUIProvider _copypastasProvider;
    [SerializeField]
    private LoadableUIProvider _gamesProvider;

    private LoadableUIProvider[] _loadables;

    private void Start()
    {
        _loadables = new[] { _mainMenuProvider, _mailsProvider, _copypastasProvider, _gamesProvider };

        SessionDataStorage.Observable.SetChangeListener(SessionDataKey.TabletState, SetState);
        SessionDataStorage.Observable.Set(SessionDataKey.TabletState, TabletState.MainMenu.ToString());
    }

    private void SetState(string stateString)
    {
        if (!Enum.TryParse<TabletState>(stateString, out var state))
        {
            Logger.Error($"{stateString} is not one of {nameof(TabletState)} values");
            return;
        }

        switch (state)
        {
            case TabletState.MainMenu:
                SetActiveLoadable(_mainMenuProvider);
                break;
            case TabletState.Mails:
                SetActiveLoadable(_mailsProvider);
                break;
            case TabletState.Copypastas:
                SetActiveLoadable(_copypastasProvider);
                break;
            case TabletState.Games:
                SetActiveLoadable(_gamesProvider);
                break;
        }
    }

    private void SetActiveLoadable(LoadableUIProvider loadable)
    {
        loadable.UIElement.gameObject.SetActive(true);

        _loadables.Except(loadable.Yield()).Where(x => x.HasLoaded).ForEach(x => x.UIElement.gameObject.SetActive(false));
    }

    private void OnDestroy()
    {
        SessionDataStorage.Observable.RemoveChangeListener(SessionDataKey.TabletState, SetState);
    }
}

public enum TabletState
{
    MainMenu = 1,
    Mails = 2,
    Copypastas = 3,
    Games = 4,
}

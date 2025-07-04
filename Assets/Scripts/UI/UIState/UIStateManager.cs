﻿public static class UIStateManager
{
    public static ObservableStateWrapper<UIState> Observable { get; private set; } = new();

    static UIStateManager()
    {
        // Forbid hiding ui elements when showing others
        Observable.ControlStateReset(UIState.Dialogue, false);
        Observable.ControlStateReset(UIState.LevelingSystem, false);
        Observable.ControlStateReset(UIState.UIButtons, false);

        Observable.Set(UIState.LevelingSystem, true);
        Observable.Set(UIState.UIButtons, true);
    }
}

public enum UIState
{
    Pause = 1,
    MovementTutorial = 8,
    ArcUI = 11,
    TabletUI = 21,
    Dialogue = 31,
    LevelingSystem = 41,
    UIButtons = 51,
}

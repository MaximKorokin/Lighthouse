using System;
using System.Linq;

public static class UIStateManager
{
    public static ObservableStateWrapper<UIState> Observable { get; private set; } = new();
}

public enum UIState
{
    Pause = 1,
    ArcUI = 11,
    PCUI = 21,
}

public static class SessionDataStorage
{
    public static ObservableDataStorageWrapper<SessionDataKey> Observable = new();
}

public enum SessionDataKey
{
    /// <summary>
    /// Requires values of <see cref="TabletState"/> enum
    /// </summary>
    TabletState = 100,
    TabletZenGameScore = 110,

    PhaseSkipInputRecieved = 228,

    SceneTransitionRequested = 1001,
}

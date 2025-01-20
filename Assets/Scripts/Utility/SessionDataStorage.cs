public static class SessionDataStorage
{
    public static ObservableDataStorageWrapper<SessionDataKey> Observable = new();
}

public enum SessionDataKey
{
    PhaseSkipInputRecieved = 228,
}

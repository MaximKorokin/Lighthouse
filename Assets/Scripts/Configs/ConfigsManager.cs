public static class ConfigsManager
{
    public static ObservablePlayerPrefsWrapper<ConfigKey> Observable { get; private set; } = new();
}

public enum ConfigKey
{
    [DefaultValue(0)]
    DebugMode = 0,
    [DefaultValue(0)]
    FpsCounter = 1,

    [DefaultValue(10)]
    SoundVolume = 20,
    [DefaultValue(10)]
    MusicVolume = 21,

    Language = 30,

    ViewHPVisualization = 100,
    ViewHPChangeVisualization = 101,

    ViewLevelingSystem = 110,

    ViewPauseButton = 120,
}

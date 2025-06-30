public static class ConfigsManager
{
    public static ObservablePlayerPrefsWrapper<ConfigKey> Observable { get; private set; } = new();
}

public enum ConfigKey
{
    [DefaultValue(false)]
    EnableDebugMode = 10,
    [DefaultValue(false)]
    ViewFpsCounter = 11,

    [DefaultValue("5")]
    SoundVolume = 20,
    [DefaultValue("5")]
    MusicVolume = 21,

    Language = 30,

    ViewHPVisualization = 100,
    ViewHPChangeVisualization = 101,

    ViewSkillsCDVisualization = 105,

    ViewExperienceBar = 110,

    ViewPauseButton = 120,
}

public static class Constants
{
    public const string LanguageNameLocalizationKey = "LanguageName";

    public const string PlayerLootingLayerName = "PlayerLooting";
    public const string ObstacleLayerName = "Obstacle";
    public const string RaycastTarget2DLayerName = "RaycastTarget2D";
    public const string FogLayerName = "Fog";

    /// <summary>
    /// New and old members must be exact names of scenes
    /// </summary>
    public enum Scene
    {
        KinsnapHQAttack = 10,
        KinsnapHQVisit = 11,
        LobbyFirstVisit = 100,
    }
}

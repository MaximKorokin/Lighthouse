using UnityEngine;

public static class GameObjectExtenions
{
    public static bool IsObstacle(this GameObject obj)
    {
        return obj.layer == LayerMask.NameToLayer(Constants.ObstacleLayerName);
    }

    public static void ToggleActive(this GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}

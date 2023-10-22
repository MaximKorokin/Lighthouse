using UnityEngine;

public static class ProjectileUtils
{
    /// <summary>
    /// If <paramref name="source"/> == <paramref name="target"/> it will try to find a new target.
    /// If no target found, it will not do anything.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="yaw"></param>
    /// <param name="effect"></param>
    public static void CreateProjectiles(ProjectileEffect effect, WorldObject source, WorldObject target)
    {
        var spreadStep = 0f;
        var currentYaw = 0f;
        if (effect.Amount > 1)
        {
            spreadStep = effect.Spread / (effect.Amount - 1);
            currentYaw = -effect.Spread / 2;
        }

        for (int i = 0; i < effect.Amount; i++)
        {
            var projectile = Object.Instantiate(effect.Projectile, source.transform.position, Quaternion.identity);
            projectile.SetEffect(effect);
            var controller = projectile.GetComponent<ControllerBase>();
            if (source == target)
            {
                if (!controller.TryFindTarget(effect.TargetType, source, currentYaw))
                {
                    projectile.gameObject.SetActive(false);
                    Object.Destroy(projectile.gameObject);
                }
            }
            else
            {
                controller.SetTarget(target, currentYaw);
            }
            currentYaw += spreadStep;
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

public static class WorldObjectsSpawner
{
    public static Coroutine Spawn(MonoBehaviour caller, WorldObjectsSpawnerSettings settings, Action<DestroyableWorldObject> perSpawnCallback = null)
    {
        return caller.StartCoroutine(SpawningCoroutine(settings, perSpawnCallback));
    }

    private static IEnumerator SpawningCoroutine(WorldObjectsSpawnerSettings settings, Action<DestroyableWorldObject> perSpawnCallback = null)
    {
        var spawnedAmount = 0;
        var aliveAmount = 0;

        while (spawnedAmount < settings.Amount)
        {
            if (aliveAmount < settings.MaxAliveAmount)
            {
                var obj = UnityEngine.Object.Instantiate(settings.Prefab);
                spawnedAmount++;
                obj.name += $"_{spawnedAmount}";

                aliveAmount++;
                obj.OnDestroying(() => aliveAmount--);
                if (settings.SpawnEffect != null)
                {
                    settings.SpawnEffect.GetEffects().Invoke(obj);
                }

                perSpawnCallback?.Invoke(obj);
                yield return new WaitForSeconds(settings.Interval);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

using UnityEngine;

public class SpawningPhase : ActPhase
{
    [SerializeField]
    private WorldObjectsSpawnerSettings _settings;
    [SerializeField]
    private Transform _transformPosition;
    [SerializeField]
    private SpawnActEndCondition _usedCondition;

    private int _spawnCount;
    private int _destroyedCount;

    public Vector2 SpawnPoint => _transformPosition.position;

    public override void Invoke()
    {
        WorldObjectsSpawner.Spawn(this, _settings, OnSpawned);
    }

    private void OnSpawned(DestroyableWorldObject worldObject)
    {
        _spawnCount++;
        worldObject.OnDestroying(OnDestroying);
        worldObject.transform.position = SpawnPoint;

        if (_usedCondition == SpawnActEndCondition.EndSpawning && _spawnCount == _settings.Amount)
        {
            InvokeEnded();
        }
    }

    private void OnDestroying()
    {
        _destroyedCount++;

        if (_usedCondition == SpawnActEndCondition.AllDestoyed && _spawnCount == _settings.Amount && _destroyedCount == _spawnCount)
        {
            InvokeEnded();
        }
    }

    public override string IconName => "WOSpawning.png";
}

public enum SpawnActEndCondition
{
    EndSpawning,
    AllDestoyed
}

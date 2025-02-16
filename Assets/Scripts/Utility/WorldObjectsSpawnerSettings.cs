using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerSettings", menuName = "ScriptableObjects/Settings/Spawner", order = 1)]
public class WorldObjectsSpawnerSettings : ScriptableObject
{
    [field: SerializeField]
    public DestroyableWorldObject Prefab { get; private set; }
    [field: SerializeField]
    public EffectSettings SpawnEffect { get; private set; }
    [field: SerializeField]
    public float Interval { get; private set; }
    [field: SerializeField]
    public int Amount { get; private set; }
    [field: SerializeField]
    public int MaxAliveAmount { get; private set; }
}

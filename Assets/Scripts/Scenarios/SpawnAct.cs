using UnityEngine;

public class SpawnAct : ScenarioAct
{
    [SerializeField]
    private DestroyableWorldObject _prefab;
    [SerializeField]
    private SpawnActUsedCondition _usedCondition;

    protected override void Act()
    {

    }
}

public enum SpawnActUsedCondition
{
    EndSpawning,
    AllDestoyed
}

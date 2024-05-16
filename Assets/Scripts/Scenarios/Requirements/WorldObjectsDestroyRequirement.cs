using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldObjectsDestroyRequirement : ActRequirement
{
    [SerializeField]
    private DestroyableWorldObject[] _worldObjects;

    private HashSet<DestroyableWorldObject> _destroyedWorldObjects = new(); 

    private void Awake()
    {
        _worldObjects.ForEach(x => x.OnDestroying(() => OnWorldObjectDestroying(x)));
    }

    private void OnWorldObjectDestroying(DestroyableWorldObject worldObject)
    {
        if (_destroyedWorldObjects.Add(worldObject) && _worldObjects.All(x => _destroyedWorldObjects.Contains(x)))
        {
            InvokeFulfilled();
        }
    }

    public override bool IsFulfilled()
    {
        return _worldObjects.All(x => _destroyedWorldObjects.Contains(x));
    }

    public override string IconName => "WODestoying.png";
}

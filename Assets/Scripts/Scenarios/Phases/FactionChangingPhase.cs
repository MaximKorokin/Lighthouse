using System.Collections.Generic;
using UnityEngine;

public class FactionChangingPhase : ActPhase
{
    [SerializeField]
    private WorldObject[] _worldObjects;
    [SerializeField]
    private Faction _faction;

    public IEnumerable<WorldObject> WorldObjects => _worldObjects;

    public override void Invoke()
    {
        _worldObjects.ForEach(x => x.SetFaction(_faction));
        InvokeEnded();
    }

    public override string IconName => "WOSwitching.png";
}

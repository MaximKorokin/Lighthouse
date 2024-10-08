﻿using UnityEngine;

public class ZoneEffect : ComplexEffect
{
    [field: SerializeField]
    public PeriodicActor Zone { get; private set; }
    [field: SerializeField]
    public float DistanceFromParent { get; private set; }
    [field: SerializeField]
    public float InvokationCooldown { get; private set; }

    public override void Invoke(CastState castState)
    {
        CreateZone(castState);
    }

    protected virtual PeriodicActor CreateZone(CastState castState)
    {
        var zone = Object.Instantiate(Zone);
        zone.Cooldown = InvokationCooldown;
        zone.AddSkill(new Skill(Effects, 0));
        zone.SetCastState(castState);
        zone.transform.position = castState.GetTargetPosition();
        return zone;
    }
}

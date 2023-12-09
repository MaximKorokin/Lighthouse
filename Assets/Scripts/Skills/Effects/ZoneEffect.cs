using UnityEngine;

public class ZoneEffect : ComplexEffect
{
    [field: SerializeField]
    public PeriodicActor Zone { get; private set; }

    public override void Invoke(CastState castState)
    {
        CreateZone(this, castState);
    }

    private static void CreateZone(ZoneEffect zoneEffect, CastState castState)
    {
        var zone = Object.Instantiate(zoneEffect.Zone);
        zone.SetEffects(zoneEffect.Effects, castState);
        zone.transform.position = castState.Source.transform.position;
    }
}
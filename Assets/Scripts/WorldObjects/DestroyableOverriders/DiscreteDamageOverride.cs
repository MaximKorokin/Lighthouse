using UnityEngine;

public class DiscreteDamageOverride : DestroyableOverrider
{
    [SerializeField]
    private float _disceteDamage = 1;

    protected override void Damaged(ref float damageValue)
    {
        if (damageValue > 0) damageValue = _disceteDamage;
    }
}

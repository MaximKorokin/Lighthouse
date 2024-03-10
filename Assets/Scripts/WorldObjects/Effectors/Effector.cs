using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorldObject))]
public class Effector : MonoBehaviour
{
    [SerializeField]
    private EffectSettings[] _startEffectsSettings;

    protected virtual void Start()
    {
        var worldObject = GetComponent<WorldObject>();
        foreach (var settings in _startEffectsSettings)
        {
            InvokeEffects(settings.GetEffects(), new CastState(worldObject));
        }
    }

    protected static void InvokeEffects(IEnumerable<Effect> effects, CastState castState) => effects.ForEach(x => x.Invoke(castState));
}

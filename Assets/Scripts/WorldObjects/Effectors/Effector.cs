using System.Linq;
using UnityEngine;

[RequireComponent(typeof(WorldObject))]
public class Effector : MonoBehaviour
{
    [SerializeField]
    private EffectSettings[] _startEffectsSettings;

    protected virtual void Start()
    {
        var _startEffects = _startEffectsSettings.SelectMany(x => x.GetEffects()).ToArray();
        var obj = GetComponent<WorldObject>();
        InvokeEffects(_startEffects, obj);
    }

    protected static void InvokeEffects(Effect[] effects, WorldObject obj) => effects.ForEach(x => x.Invoke(obj));
}

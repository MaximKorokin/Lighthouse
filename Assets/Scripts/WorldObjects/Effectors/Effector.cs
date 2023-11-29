using UnityEngine;

[RequireComponent(typeof(WorldObject))]
public class Effector : MonoBehaviour
{
    [SerializeField]
    private Effect[] _startEffects;

    protected virtual void Start()
    {
        var obj = GetComponent<WorldObject>();
        _startEffects.ForEach(x => x.Invoke(obj));
    }

    protected static void InvokeEffects(Effect[] effects, WorldObject obj) => effects.ForEach(x => x.Invoke(obj));
}

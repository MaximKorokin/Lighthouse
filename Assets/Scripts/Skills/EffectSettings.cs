using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "ScriptableObjects/Settings/Effect", order = 1)]
public class EffectSettings : ScriptableObject
{
    [field: SerializeField]
    public EffectPreview Preview { get; private set; }
    [field: SerializeField]
    public float Cooldown { get; private set; }
    [SerializeReference]
    private Effect[] _effects;

    public Effect[] GetEffects()
    {
        return Instantiate(this)._effects;
    }
}

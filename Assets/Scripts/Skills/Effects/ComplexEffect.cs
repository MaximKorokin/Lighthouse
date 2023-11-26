using UnityEngine;

[CreateAssetMenu(fileName = "ComplexEffect", menuName = "ScriptableObjects/Effects/ComplexEffect", order = 1)]
public class ComplexEffect : Effect
{
    [field: SerializeField]
    public Effect[] Effects { get; private set; }

    public override void Invoke(CastState castState)
    {
        foreach (var effect in Effects)
        {
            effect.Invoke(castState);
        }
    }
}

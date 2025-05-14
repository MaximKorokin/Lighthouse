using UnityEngine;

public class RandomEffect : Effect
{
    [SerializeField]
    [Range(0f, 1f)]
    private float _successRatio;
    [SerializeReference]
    private Effect[] _successEffects;
    [SerializeReference]
    private Effect[] _failureEffects;

    public override void Invoke(CastState castState)
    {
        if (Random.Range(0f, 1f) <= _successRatio)
        {
            _successEffects?.Invoke(castState);
        }
        else
        {
            _failureEffects?.Invoke(castState);
        }
    }
}

using UnityEngine;

public class MovableLayersEffect : Effect
{
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private bool _exclude;

    public override void Invoke(CastState castState)
    {
        if (castState.GetTarget() is MovableWorldObject movable)
        {
            movable.RigidbodyExtender.SetExcludeLayers(_layerMask, _exclude);
        }
    }
}

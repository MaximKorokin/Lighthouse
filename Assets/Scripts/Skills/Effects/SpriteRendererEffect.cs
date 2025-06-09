using UnityEngine;

public class SpriteRendererEffect : Effect
{
    [SerializeField]
    private SortingLayer _sortingLayer;

    public override void Invoke(CastState castState)
    {
        var target = castState.GetTarget();
        if (target.TryGetComponent(out SpriteRenderer spriteRenderer) ||
            target.transform.GetChild(0).TryGetComponent(out spriteRenderer))
        {
            spriteRenderer.sortingLayerName = _sortingLayer.ToString();
        }
        else
        {
            Logger.Warn($"Target object {target} doesn't contain {typeof(Animator)}");
        }
    }
}

using UnityEngine;

public class GenericSimpleAnimator : AnimatorBase
{
    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        SetOrdering((Vector2)transform.position);
    }

    public void SetLayerAndOrderingOffset(string layer, float orderingOffset)
    {
        SpriteRenderer.sortingLayerName = layer;
        OrderingOffset = orderingOffset;
    }
}

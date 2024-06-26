using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public class HurtVignetteVisualizer : HPVignetteVisualizer
{
    [SerializeField]
    private float _showTime;
    [SerializeField]
    [Range(0f, 1f)]
    private float _alpha;

    private float _alphaStep;

    protected override void Awake()
    {
        base.Awake();

        _alphaStep = _alpha / _showTime * Time.deltaTime;
    }

    public override void VisualizeAmount(float prev, float cur, float max)
    {
        if (prev > cur)
        {
            Overlay.Color = new(Overlay.Color.r, Overlay.Color.g, Overlay.Color.b, _alpha);
        }
    }

    private void Update()
    {
        if (Overlay.Color.a > 0)
        {
            Overlay.Color = new(Overlay.Color.r, Overlay.Color.g, Overlay.Color.b, Mathf.Clamp(Overlay.Color.a - _alphaStep, 0, _alpha));
        }
    }
}

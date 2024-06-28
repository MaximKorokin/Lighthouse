using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public class LowHPVignetteVisualizer : HPVignetteVisualizer
{
    [SerializeField]
    [Range(0f, 1f)]
    private float _hpThreshold;
    [SerializeField]
    [Range(0f, 1f)]
    private float _minAlpha;
    [SerializeField]
    [Range(0f, 1f)]
    private float _maxAlpha;
    [SerializeField]
    private float _blinkTime;

    private float _alphaStep;
    private bool _isHPUnderThreshold, _isAlphaGrowing;

    protected override void Awake()
    {
        base.Awake();

        if (_maxAlpha < _minAlpha)
        {
            Logger.Log($"{nameof(_maxAlpha)} is less than {nameof(_minAlpha)}");
        }

        _alphaStep = (_maxAlpha - _minAlpha) / _blinkTime * 2 * Time.deltaTime;
    }

    public override void VisualizeAmount(float prev, float cur, float max)
    {
        if (Overlay == null) return;

        _isHPUnderThreshold = cur <= max * _hpThreshold;

        if (!_isHPUnderThreshold && Overlay.Color.a != 0)
        {
            Overlay.Color = new(Overlay.Color.r, Overlay.Color.g, Overlay.Color.b, 0);
        }
    }

    private void Update()
    {
        if (!_isHPUnderThreshold)
        {
            return;
        }

        Overlay.Color = new(
            Overlay.Color.r,
            Overlay.Color.g,
            Overlay.Color.b,
            Mathf.Clamp(Overlay.Color.a + (_isAlphaGrowing ? 1 : -1) * _alphaStep, _minAlpha, _maxAlpha));

        if (Overlay.Color.a == _maxAlpha)
        {
            _isAlphaGrowing = false;
        }
        if (Overlay.Color.a == _minAlpha)
        {
            _isAlphaGrowing = true;
        }
    }
}

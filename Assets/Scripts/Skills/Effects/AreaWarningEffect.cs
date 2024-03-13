using UnityEngine;

public class AreaWarningEffect : IteratingEffect<LineRenderer>
{
    [SerializeField]
    private float _startRadius;
    [SerializeField]
    private float _endRadius;

    protected override float Interval { get => Time.deltaTime; set => throw new System.NotImplementedException(); }

    protected override void StartIterating(CastState castState, LineRenderer parameter)
    {
        var renderer = LineRenderersPool.Take(null);
        renderer.positionCount = 2;
        var targetPosition = castState.GetTargetPosition();
        renderer.SetPosition(0, targetPosition);
        renderer.SetPosition(1, targetPosition + new Vector2(1e-6f, 0));
        renderer.numCapVertices = 10;
        renderer.startWidth = _startRadius;
        renderer.endWidth = _startRadius;

        base.StartIterating(castState, renderer);
    }

    protected override void StopIterating(CastState castState, LineRenderer renderer)
    {
        LineRenderersPool.Return(renderer);
    }

    protected override void Iterate(CastState castState, LineRenderer renderer)
    {
        var radiusStep = (_endRadius - _startRadius) / Duration * Interval;
        renderer.startWidth += radiusStep;
        renderer.endWidth += radiusStep;
    }
}

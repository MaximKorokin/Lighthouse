using System;
using UnityEngine;

public class TraceWarningEffect : IteratingEffect<LineRenderer>
{
    [SerializeField]
    private float _startWidth;
    [SerializeField]
    private float _endWidth;

    protected override float IterationTime { get => Time.deltaTime; set => throw new InvalidOperationException(); }

    protected override void StartIterating(CastState castState, LineRenderer parameter)
    {
        var renderer = LineRenderersPool.Take(null);
        renderer.positionCount = 2;
        renderer.numCapVertices = 1;
        renderer.startWidth = _startWidth;
        renderer.endWidth = _startWidth;

        base.StartIterating(castState, renderer);
    }

    protected override void Iterate(CastState castState, LineRenderer renderer)
    {
        var targetPosition = castState.GetTargetPosition();
        renderer.SetPosition(0, castState.Source.transform.position);
        renderer.SetPosition(1, targetPosition);

        var radiusStep = (_endWidth - _startWidth) / Duration * IterationTime;
        renderer.startWidth += radiusStep;
        renderer.endWidth += radiusStep;
    }

    protected override void StopIterating(CastState castState, LineRenderer renderer)
    {
        LineRenderersPool.Return(renderer);
    }
}

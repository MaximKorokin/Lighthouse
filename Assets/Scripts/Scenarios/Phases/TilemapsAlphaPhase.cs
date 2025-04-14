using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapsAlphaPhase : ActPhase
{
    private static readonly Dictionary<Tilemap, BoolCounter> _requestsCounters = new();

    [SerializeField]
    [Range(0, 1)]
    private float _alpha = 0;
    [SerializeField]
    private Tilemap[] _tilemaps;
    [SerializeField]
    private float _time;

    public override void Invoke()
    {
        if (_time == 0)
        {
            foreach (var tilemap in _tilemaps)
            {
                tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, _alpha);
            }
            InvokeFinished();
        }
        else
        {
            _tilemaps.ForEach(tilemap =>
            {
                var delta = _alpha != 1;
                _requestsCounters.AddOrModify(tilemap, () => new BoolCounter(delta), val => { val.Set(delta); return val; });

                // Hiding tilemap is priority operation
                // Will execute if HIDING or VIEW count <= 0
                if (delta || !_requestsCounters[tilemap])
                {
                    CoroutinesHandler.StartUniqueCoroutine(tilemap, TransitionCoroutine(tilemap, _alpha, _alpha - tilemap.color.a, _time));
                }
            });
            InvokeFinished();
        }
    }

    private static IEnumerator TransitionCoroutine(Tilemap tilemap, float targetAlpha, float stepMultiplier, float time)
    {
        yield return new WaitForSeconds(0.1f);
        while (tilemap.color.a != targetAlpha)
        {
            var step = stepMultiplier / time * Time.deltaTime;
            var newAlpha = stepMultiplier > 0
                ? Mathf.Clamp(tilemap.color.a + step, 0, targetAlpha)
                : Mathf.Clamp(tilemap.color.a + step, targetAlpha, 1);
            tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return new WaitForEndOfFrame();
        }
    }

    public override string IconName => "Eye.png";
    public override Color IconColor =>
        _tilemaps?.Length > 0
            ? (_alpha switch
                {
                    > 0.6f => MyColors.Green,
                    < 0.4f => MyColors.Red,
                    _ => Color.white,
                })
            : MyColors.Gray;
}

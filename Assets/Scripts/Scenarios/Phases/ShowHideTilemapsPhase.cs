using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowHideTilemapsPhase : ActPhase
{
    [SerializeField]
    private Tilemap[] _tilemaps;
    [SerializeField]
    private float _time;
    [SerializeField]
    private bool _show;

    public override void Invoke()
    {
        if (_time == 0)
        {
            foreach (var tilemap in _tilemaps)
            {
                if (_show)
                {
                    tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, 1);
                }
                else
                {
                    tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, 0);
                }
            }

            InvokeEnded();
        }
        else
        {
            StartCoroutine(TransitionCoroutine());
        }
    }

    private IEnumerator TransitionCoroutine()
    {
        var targetAlpha = _show ? 1 : 0;
        var stepMultiplier = _show ? 1 : -1;
        while (_tilemaps.Any(x => x.color.a != targetAlpha))
        {
            foreach (var tilemap in _tilemaps)
            {
                var step = stepMultiplier / _time * Time.deltaTime;
                var newAlpha = Mathf.Clamp01(tilemap.color.a + step);
                tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public override string IconName => "Eye.png";
    public override Color IconColor => _show ? MyColors.Green : MyColors.Red;
}

using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowHideTilemapsPhase : ActPhase
{
    [SerializeField]
    private Tilemap[] _tilemapsToShow;
    [SerializeField]
    private Tilemap[] _tilemapsToHide;
    [SerializeField]
    private float _time;

    public override void Invoke()
    {
        if (_time == 0)
        {
            foreach (var tilemap in _tilemapsToShow)
            {
                tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, 1);
            }
            foreach (var tilemap in _tilemapsToHide)
            {
                tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, 0);
            }
            InvokeFinished();
        }
        else
        {
            if (_tilemapsToShow.Length > 0) _tilemapsToShow.ForEach(x => CoroutinesHandler.StartUniqueCoroutine(x, TransitionCoroutine(x, 1, 1), InvokeFinished));
            if (_tilemapsToHide.Length > 0) _tilemapsToHide.ForEach(x => CoroutinesHandler.StartUniqueCoroutine(x, TransitionCoroutine(x, 0, -1), InvokeFinished));
        }
    }

    private IEnumerator TransitionCoroutine(Tilemap tilemap, float targetAlpha, float stepMultiplier)
    {
        yield return new WaitForSeconds(0.1f);
        while (tilemap.color.a != targetAlpha)
        {
            var step = stepMultiplier / _time * Time.deltaTime;
            var newAlpha = Mathf.Clamp01(tilemap.color.a + step);
            tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return new WaitForEndOfFrame();
        }
    }

    public override string IconName => "Eye.png";
    public override Color IconColor =>
        _tilemapsToShow.Length > 0 && _tilemapsToHide.Length > 0
            ? Color.white
            : _tilemapsToShow.Length > 0
                ? MyColors.Green
                : _tilemapsToHide.Length > 0
                    ? MyColors.Red
                    : MyColors.Gray;
}

using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowHideTilemapsPhase : ActPhase
{
    [SerializeField]
    [Range(0, 1)]
    private float _showAlpha = 1;
    [SerializeField]
    private Tilemap[] _tilemapsToShow;
    [SerializeField]
    [Range(0, 1)]
    private float _hideAlpha = 0;
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
                tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, _showAlpha);
            }
            foreach (var tilemap in _tilemapsToHide)
            {
                tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, _hideAlpha);
            }
            InvokeFinished();
        }
        else
        {
            if (_tilemapsToShow.Length > 0) _tilemapsToShow.ForEach(x => CoroutinesHandler.StartUniqueCoroutine(x, TransitionCoroutine(x, _showAlpha, _showAlpha - x.color.a), InvokeFinished));
            if (_tilemapsToHide.Length > 0) _tilemapsToHide.ForEach(x => CoroutinesHandler.StartUniqueCoroutine(x, TransitionCoroutine(x, _hideAlpha, _hideAlpha - x.color.a), InvokeFinished));
        }
    }

    private IEnumerator TransitionCoroutine(Tilemap tilemap, float targetAlpha, float stepMultiplier)
    {
        yield return new WaitForSeconds(0.1f);
        while (tilemap.color.a != targetAlpha)
        {
            var step = stepMultiplier / _time * Time.deltaTime;
            var newAlpha = stepMultiplier > 0
                ? Mathf.Clamp(tilemap.color.a + step, 0, _showAlpha)
                : Mathf.Clamp(tilemap.color.a + step, _hideAlpha, 1);
            tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return new WaitForEndOfFrame();
        }
    }

    public override string IconName => "Eye.png";
    public override Color IconColor =>
        _tilemapsToShow?.Length > 0 && _tilemapsToHide?.Length > 0
            ? Color.white
            : _tilemapsToShow?.Length > 0
                ? MyColors.Green
                : _tilemapsToHide?.Length > 0
                    ? MyColors.Red
                    : MyColors.Gray;
}

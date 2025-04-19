using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapsAlphaPhase : ActPhase
{
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
            _tilemaps.ForEach(tilemap => tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, _alpha));
        }
        else
        {
            _tilemaps.ForEach(tilemap => CoroutinesHandler.StartUniqueCoroutine(tilemap, CoroutinesUtils.TilemapAlphaCoroutine(tilemap, _alpha, _alpha - tilemap.color.a, _time)));
        }
        InvokeFinished();
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

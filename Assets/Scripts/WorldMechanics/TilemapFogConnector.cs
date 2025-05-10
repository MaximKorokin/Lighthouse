using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapFogConnector : MonoBehaviour
{
    [SerializeField]
    private TilemapFog[] _fogs;

    private TilemapFog _middleFog;

    private static readonly Dictionary<Tilemap, BoolCounter> _requestsCounters = new();

    private void Start()
    {
        _middleFog = this.GetRequiredComponent<TilemapFog>();
        _middleFog.TilemapHiding += OnTilemapHiding;
        _middleFog.TilemapShowing += OnTilemapShowing;

        foreach (var fog in _fogs)
        {
            fog.TilemapHiding += OnTilemapHiding;
            fog.TilemapShowing += OnTilemapShowing;

            _middleFog.AddTilemap(fog.Tilemap);
            fog.AddTilemap(_middleFog.Tilemap);
        }
    }

    private void OnTilemapShowing(Tilemap tilemap, ref bool shouldContinue)
    {
        _requestsCounters.AddOrModify(tilemap, () => new BoolCounter(false), val => { val.Set(true); return val; });
        // Hiding tilemap is priority operation
        shouldContinue = _requestsCounters[tilemap];
    }

    private void OnTilemapHiding(Tilemap tilemap, ref bool shouldContinue)
    {
        if (!shouldContinue) return;

        _requestsCounters.AddOrModify(tilemap, () => new BoolCounter(false), val => { val.Set(false); return val; });
    }
}

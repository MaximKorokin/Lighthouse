using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapFog : MonoBehaviour
{
    [SerializeField]
    private bool _showOnStart;
    [SerializeField]
    private float _time;
    [SerializeField]
    [Range(0, 1)]
    private float _showAlpha = 1;
    [SerializeField]
    [Range(0, 1)]
    private float _hideAlpha = 0;

    private readonly List<Tilemap> _tilemaps = new();
    private TriggeredWorldObjectsCollection _triggeredObjectsCollection;

    public event RefAction<Tilemap, bool> TilemapShowing;
    public event RefAction<Tilemap, bool> TilemapHiding;

    private Tilemap _tilemap;
    public Tilemap Tilemap => gameObject.LazyGetComponent(ref _tilemap);

    private void Start()
    {
        _tilemaps.Add(Tilemap);
        if (_showOnStart)
        {
            ShowTilemap(Tilemap);
        }

        _triggeredObjectsCollection = new TriggeredWorldObjectsCollection(this.GetRequiredComponent<WorldObjectFindingTriggerDetector>(), x => x is PlayerCreature);
        _triggeredObjectsCollection.Triggered += Triggered;
    }

    private void Triggered(WorldObject worldObject, bool entered)
    {
        _tilemaps.ForEach(tilemap =>
        {
            var shouldContinue = true;
            if (entered)
            {
                TilemapHiding?.Invoke(tilemap, ref shouldContinue);
                if (shouldContinue) HideTilemap(tilemap);
            }
            else
            {
                TilemapShowing?.Invoke(tilemap, ref shouldContinue);
                if (shouldContinue) ShowTilemap(tilemap);
            }
        });
    }

    private void ShowTilemap(Tilemap tilemap)
    {
        CoroutinesHandler.StartUniqueCoroutine(tilemap, CoroutinesUtils.WaitForSeconds(0.1f).Then(CoroutinesUtils.InterpolationCoroutine(
            () => tilemap.color.a,
            a => tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, a),
            _showAlpha,
            _time)));
    }

    private void HideTilemap(Tilemap tilemap)
    {
        CoroutinesHandler.StartUniqueCoroutine(tilemap, CoroutinesUtils.WaitForSeconds(0.1f).Then(CoroutinesUtils.InterpolationCoroutine(
            () => tilemap.color.a,
            a => tilemap.color = new(tilemap.color.r, tilemap.color.g, tilemap.color.b, a),
            _hideAlpha,
            _time)));
    }

    public void AddTilemap(Tilemap tilemap)
    {
        if (!_tilemaps.Contains(tilemap)) _tilemaps.Add(tilemap);
    }
}

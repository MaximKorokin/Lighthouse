using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(WorldObjectFindingTriggerDetector))]
public class TilemapRaycastFog : MonoBehaviour
{
    [SerializeField]
    private int _revealRadius = 1;

    private Tilemap _tilemap;
    private TriggeredWorldObjectsCollection _triggeredObjectsCollection;

    private WorldObject _worldObject;

    private HashSet<Vector3Int> _hiddenTiles = new();

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        _triggeredObjectsCollection = new TriggeredWorldObjectsCollection(GetComponent<WorldObjectFindingTriggerDetector>(), x => x is PlayerCreature);
        _triggeredObjectsCollection.Triggered += Triggered;
    }

    private void Triggered(WorldObject worldObject, bool entered)
    {
        if (entered)
        {
            if (_worldObject == null)
            {
                _worldObject = worldObject;
            }
        }
        else
        {
            _worldObject = null;
            _hiddenTiles.ForEach(ShowTile);
        }
    }

    private void Update()
    {
        if (_worldObject == null) return;

        ShowAround(_worldObject.transform.position);
    }

    private void ShowAround(Vector2 position)
    {
        var center = _tilemap.WorldToCell(position);

        var tiles = new List<Vector3Int>();
        for (int x = -_revealRadius; x <= _revealRadius; x++)
        {
            for (int y = -_revealRadius; y <= _revealRadius; y++)
            {
                var tilePosition = new Vector3Int(center.x + x, center.y + y, 0);

                if (x * x + y * y <= _revealRadius * _revealRadius)
                {
                    tiles.Add(tilePosition);
                }
            }
        }

        var filter = new ContactFilter2D()
        {
            useLayerMask = true,
            layerMask = LayerMask.GetMask(Constants.ObstacleLayerName),
            useTriggers = false,
        };

        var raycastHits = new RaycastHit2D[1];
        var reachableTiles = tiles
            .Where(x => Physics2D.Raycast(
                    (Vector3)position,
                    (Vector3)(x - center),
                    filter,
                    raycastHits,
                    (x - center).magnitude * _tilemap.cellSize.x) == 0
                || (raycastHits.First().point.x % _tilemap.cellSize.x != 0
                    && (x - center).magnitude * _tilemap.cellSize.x <= raycastHits.First().distance))
            .ToArray();

        reachableTiles.ForEach(HideTile);
        var tilesToShow = _hiddenTiles.Except(reachableTiles).ToArray();
        tilesToShow.ForEach(ShowTile);
        _hiddenTiles.RemoveRange(tilesToShow);
        _hiddenTiles.AddRange(reachableTiles);
    }

    private void ShowTile(Vector3Int position)
    {
        _tilemap.SetColor(position, Color.black);
        _tilemap.SetTileFlags(position, TileFlags.LockColor);
    }

    private void HideTile(Vector3Int position)
    {
        _tilemap.SetTileFlags(position, TileFlags.None);
        _tilemap.SetColor(position, new Color(.5f, .5f, .5f, .5f));
    }
}

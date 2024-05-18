using System.Collections.Generic;
using UnityEngine;

public class MarkingSystem : MonoBehaviorSingleton<MarkingSystem>
{
    [SerializeField]
    private Transform _markersParent;
    [SerializeField]
    private Marker _markerTemplate;

    private static readonly Dictionary<Transform, Marker> _markers = new();

    public static void AddMarker(Transform target)
    {
        if (_markers.ContainsKey(target))
        {
            return;
        }
        var marker = Instantiate(Instance._markerTemplate, Instance._markersParent);
        marker.MarkingTarget = target;
        marker.gameObject.SetActive(true);
        _markers[target] = marker;
    }

    public static void RemoveMarker(Transform target)
    {
        if (_markers.ContainsKey(target))
        {
            Destroy(_markers[target].gameObject);
            _markers.Remove(target);
        }
    }
}

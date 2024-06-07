using System;
using UnityEngine;

[RequireComponent(typeof(ScenarioAct))]
public class ActVisualizer : MonoBehaviour
{
    [SerializeField]
    private ActVisualizationType _visualizationType;

    private ScenarioAct _act;

    private void Awake()
    {
        _act = GetComponent<ScenarioAct>();
        _act.Initialized += x => AddVisualization(x);
        _act.Ended += x => RemoveVisualization(x);
    }

    private void AddVisualization(ScenarioAct act)
    {
        if (_visualizationType.HasFlag(ActVisualizationType.Marker))
        {
            MarkingSystem.AddMarker(act.transform);
        }
    }

    private void RemoveVisualization(ScenarioAct act)
    {
        MarkingSystem.RemoveMarker(act.transform);
    }
}

[Flags]
public enum ActVisualizationType
{
    None = 0,
    Marker = 1,
    Text = 2,
}

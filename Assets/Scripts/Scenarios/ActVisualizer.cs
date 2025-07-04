using System;
using UnityEngine;

public class ActVisualizer : MonoBehaviour
{
    [SerializeField]
    private ActVisualizationType _visualizationType;

    private ScenarioAct _act;

    private void Awake()
    {
        _act = this.GetRequiredComponent<ScenarioAct>();
        _act.Initialized += x => AddVisualization(x);
        _act.Finished += x => RemoveVisualization(x);
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

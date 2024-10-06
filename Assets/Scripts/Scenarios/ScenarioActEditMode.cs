using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteAlways]
#endif
public partial class ScenarioAct : MonoBehaviour
{
    /// <summary>
    /// It is assigned automatically based on Acts hierarchy. Its value is true when an act have no parent.
    /// </summary>
    [HideInInspector]
    [SerializeField]
    private bool _selfInitializable = true;

#if UNITY_EDITOR
    private List<ScenarioAct> _previousChildren = new();

    private void Update()
    {
        if (Application.isPlaying)
        {
            return;
        }

        _requirements ??= new();
        _requirements.Where(x => x == null).ToArray().ForEach(x => _requirements.Remove(x));
        foreach (var requirement in GetComponents<ActRequirement>())
        {
            if (!_requirements.Contains(requirement))
            {
                _requirements.Add(requirement);
            }
        }

        _phases ??= new();
        _phases?.Where(x => x == null).ToArray().ForEach(x => _phases.Remove(x));
        foreach (var phase in GetComponents<ActPhase>())
        {
            if (!_phases.Contains(phase))
            {
                _phases.Add(phase);
            }
        }

        _childrenActs ??= new();
        _childrenActs.Where(x => x == null).ToArray().ForEach(x => _childrenActs.Remove(x));
        _childrenActs.Where(x => x != this).ForEach(x => x._selfInitializable = false);
        _previousChildren.Except(_childrenActs).ForEach(x => x._selfInitializable = true);
        _previousChildren = _childrenActs.ToList();
    }

    private void OnDestroy()
    {
        _childrenActs?.Where(x => x != null).ForEach(x => x._selfInitializable = true);
    }
#endif
}

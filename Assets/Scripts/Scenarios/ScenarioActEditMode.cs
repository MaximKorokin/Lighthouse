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
    [InitialEditorValue(true)]
    [HideInInspector]
    [SerializeField]
    private bool _selfInitializable = true;

#if UNITY_EDITOR
    private List<ScenarioAct> _previousChildrenActs = new();

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

        _phases = GetComponents<ActPhase>().ToList();

        _childrenActs ??= new();
        _childrenActs.Where(x => x == null).ToArray().ForEach(x => _childrenActs.Remove(x));
        _childrenActs.Where(x => x != this).ForEach(x => x._selfInitializable = false);
        _previousChildrenActs.Except(_childrenActs).ForEach(x => x._selfInitializable = true);
        _previousChildrenActs = _childrenActs.ToList();
    }

    private void OnDestroy()
    {
        _childrenActs?.Where(x => x != null).ForEach(x => x._selfInitializable = true);
    }
#endif
}

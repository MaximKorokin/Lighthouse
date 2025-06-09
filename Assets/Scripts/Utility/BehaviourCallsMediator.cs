using System;
using System.Linq;
using UnityEngine;

public class BehaviourCallsMediator : MonoBehaviour
{
    private static readonly PrioritizedList<Action> _awakeList = new();
    private static bool _invoked = false;

    protected void Awake()
    {
        _invoked = true;
        _awakeList.ToArray().ForEach(x => x.Invoke());
    }

    /// <summary>
    /// If called after Mediator's Awake, will invoke instantly
    /// </summary>
    /// <param name="awakable"></param>
    /// <param name="priority"></param>
    public static void RequestLateAwakeCall(int priority, Action action)
    {
        if (_invoked) action.Invoke();
        else _awakeList.Add(action, priority);
    }

    private void OnDestroy()
    {
        _invoked = false;
        _awakeList.Clear();
    }
}

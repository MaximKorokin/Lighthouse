﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ActorBase : MonoBehaviour
{
    public BoolCounter _canAct = new(true);
    public bool CanAct { get => _canAct; set => _canAct.Set(value); }

    private WorldObject _worldObject;
    public WorldObject WorldObject => gameObject.LazyGetComponent(ref _worldObject);

    public event Action Acting;

    public void Act(PrioritizedTargets targets)
    {
        if (CanAct)
        {
            ActInternal(targets);
        }
    }

    protected virtual void ActInternal(PrioritizedTargets targets)
    {
        Acting?.Invoke();
    }
}

public struct PrioritizedTargets
{
    public PrioritizedTargets(WorldObject target, IEnumerable<WorldObject> targets, IEnumerable<WorldObject> primaryTargets, IEnumerable<WorldObject> secondaryTargets)
    {
        MainTarget = target;
        Targets = targets;
        PrimaryTargets = primaryTargets;
        SecondaryTargets = secondaryTargets;
    }
    public PrioritizedTargets(WorldObject target, IEnumerable<WorldObject> targets) : this(target, targets, targets, Enumerable.Empty<WorldObject>()) { }
    public PrioritizedTargets(IEnumerable<WorldObject> targets) : this(targets.FirstOrDefault(), targets) { }

    public WorldObject MainTarget;
    public readonly IEnumerable<WorldObject> Targets;
    public readonly IEnumerable<WorldObject> PrimaryTargets;
    public readonly IEnumerable<WorldObject> SecondaryTargets;
}

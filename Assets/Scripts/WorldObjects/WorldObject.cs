using System;
using UnityEngine;

public abstract class WorldObject : MonoBehaviour
{
    [field: SerializeField]
    public PositioningType PositioningType { get; protected set; }
    [field: SerializeField]
    public PositioningType TriggeringType { get; protected set; }
    private Animator Animator { get; set; }

    [SerializeField]
    private Stats _stats;
    protected Stats Stats => _stats;

    public virtual float ActionRange => Stats[StatName.ActionRange] * Stats[StatName.SizeScale];

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Stats.Init();
        OnStatsModified();
    }

    public void ModifyStats(Stats otherStats)
    {
        Stats.Modify(otherStats);
        OnStatsModified();
    }

    /// <summary>
    /// Called on Awake and each time <see cref="ModifyStats"/> is called
    /// </summary>
    protected virtual void OnStatsModified()
    {
        var sizeScale = Stats[StatName.SizeScale];
        if (sizeScale != transform.localScale.z)
        {
            transform.localScale = Vector3.one * sizeScale;
        }
    }

    protected void SetAnimatorValue<T>(string name, T value = default) where T : struct
    {
        if (Animator == null)
        {
            return;
        }

        switch (Array.Find(Animator.parameters, x => x.name == name)?.type)
        {
            case AnimatorControllerParameterType.Bool:
                Animator.SetBool(name, Convert.ToBoolean(value));
                break;
            case AnimatorControllerParameterType.Trigger:
                Animator.SetTrigger(name);
                break;
            case AnimatorControllerParameterType.Int:
                Animator.SetInteger(name, Convert.ToInt32(value));
                break;
            case AnimatorControllerParameterType.Float:
                Animator.SetFloat(name, Convert.ToSingle(value));
                break;
        }
    }
}

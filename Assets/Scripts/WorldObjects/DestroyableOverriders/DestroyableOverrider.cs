using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public abstract class DestroyableOverrider : MonoBehaviour
{
    protected DestroyableWorldObject Destroyable { get; private set; }

    public virtual void Awake()
    {
        Destroyable = GetComponent<DestroyableWorldObject>();
        Destroyable.Damaged += Damaged;
    }

    protected abstract void Damaged(ref float damageValue);
}

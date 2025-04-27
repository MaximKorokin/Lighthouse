using UnityEngine;

[RequireComponent(typeof(DestroyableWorldObject))]
public abstract class DestroyableOverrider : MonoBehaviour
{
    protected DestroyableWorldObject Destroyable { get; private set; }

    protected virtual void Start()
    {
        Destroyable = GetComponent<DestroyableWorldObject>();
        Destroyable.Damaged += Damaged;
    }

    protected abstract void Damaged(ref float damageValue);
}

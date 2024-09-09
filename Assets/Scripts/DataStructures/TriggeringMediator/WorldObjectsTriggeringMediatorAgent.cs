using UnityEngine;

[RequireComponent(typeof(WorldObject))]
public class WorldObjectsTriggeringMediatorAgent : MonoBehaviour
{
    protected WorldObject WorldObject { get; private set; }

    protected virtual void Start()
    {
        WorldObject = GetComponent<WorldObject>();
        WorldObjectsTriggeringMediator.Instance.AddItem(WorldObject);

        WorldObject.Destroyed += OnDestroyed;
    }

    protected virtual void OnDestroyed()
    {
        WorldObjectsTriggeringMediator.Instance.RemoveItem(WorldObject);
    }
}

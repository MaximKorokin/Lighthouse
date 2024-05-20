using System.Linq;
using UnityEngine;

public class ComponentOperationEffect : Effect
{
    [SerializeField]
    private ComponentType _component;
    [SerializeField]
    private ComponentOperationType _operation;

    public override void Invoke(CastState castState)
    {
        var component = GetComponent(castState.Target.gameObject, _component);
        if (component != null)
        {
            InvokeOperation(component, _operation);
        }
    }

    private static Component GetComponent(GameObject gameObject, ComponentType type)
    {
        return type switch
        {
            ComponentType.Collider2D => gameObject.GetComponents<Collider2D>().FirstOrDefault(x => !x.isTrigger),
            _ => null,
        };
    }

    private static void InvokeOperation(Component component, ComponentOperationType operation)
    {
        switch (operation)
        {
            case ComponentOperationType.Enable:
                if (component is Behaviour enableBehaviour) enableBehaviour.enabled = true;
                break;
            case ComponentOperationType.Disable:
                if (component is Behaviour disableBehaviour) disableBehaviour.enabled = false;
                break;
        }
    }
}

public enum ComponentType
{
    Collider2D,
}

public enum ComponentOperationType
{
    Enable,
    Disable,
}

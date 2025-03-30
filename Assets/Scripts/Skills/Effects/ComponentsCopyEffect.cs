using System.Linq;
using System.Reflection;
using UnityEngine;

public class ComponentsCopyEffect : Effect
{
    [field: SerializeReference]
    public GameObject Source { get; private set; }

    public override void Invoke(CastState castState)
    {
        var target = castState.GetTarget().gameObject;

        // Create a copy of Source
        var sourceCopy = Object.Instantiate(Source);
        sourceCopy.SetActive(false);

        target.SetActive(false);
        // Copy components on itself
        CopyComponents(sourceCopy, target);
        // Copy components on children
        foreach (var childSource in sourceCopy.transform.Cast<Transform>().Select(x => x.gameObject))
        {
            var childTarget = new GameObject(childSource.name);
            childTarget.transform.SetParent(target.transform, false);
            CopyComponents(childSource, childTarget);
        }
        target.SetActive(true);

        // Destroy copy of Source
        Object.Destroy(sourceCopy);
    }

    private static void CopyComponents(GameObject source, GameObject target)
    {
        var targetComponents = target.GetComponents<Component>();
        var sourceComponents = source.GetComponents<Component>();

        foreach (var sourceComponent in sourceComponents)
        {
            var sourceType = sourceComponent.GetType();
            // Ignore component if already exists on Target
            if (targetComponents.Any(x => x.GetType() == sourceType)) continue;

            var targetComponent = target.AddComponent(sourceType);

            CopyComponent(sourceComponent, targetComponent);
        }
    }

    private static void CopyComponent<T>(T source, T target) where T : Component
    {
        if (source.TryCopyTo(target)) return;
        if (source is MonoBehaviour)
        {
            if (source is ICopyable<T> copyable)
            {
                copyable.CopyTo(target);
                return;
            }
            ShallowClone(source, target);
        }
        else
        {
            Logger.Warn($"Could not copy component {source}");
        }
    }

    private static void ShallowClone(Component source, Component target)
    {
        var sourceType = source.GetType();
        // Iterate through all public and serialized fields
        foreach (var field in ReflectionUtils.GetAllFields(sourceType, true, typeof(Component), BindingFlags.Instance | BindingFlags.Public).Concat(
            ReflectionUtils.GetFieldsWithAttributes(sourceType, true, typeof(SerializeField))))
        {
            field.SetValue(target, field.GetValue(source));
        }
        // Iterate through all public properties
        foreach (var property in ReflectionUtils.GetAllProperties(sourceType, true, typeof(Component), BindingFlags.Instance | BindingFlags.Public))
        {
            if (property.CanWrite) property.SetValue(target, property.GetValue(source));
        }
    }
}

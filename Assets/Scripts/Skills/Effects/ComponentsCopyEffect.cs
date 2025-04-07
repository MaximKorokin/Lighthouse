using System.Collections.Generic;
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
        var sourceCopy = CopyGameObject(Source);

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

    private static GameObject CopyGameObject(GameObject source)
    {
        source.SetActive(false);
        var sourceCopy = Object.Instantiate(source);
        sourceCopy.SetActive(false);
        source.SetActive(true);
        return sourceCopy;
    }

    private static void CopyComponents(GameObject source, GameObject target)
    {
        const int MaxCyclesLimit = 1000;

        var targetComponents = target.GetComponents<Component>().ToList();
        var sourceComponents = new Queue<Component>(source.GetComponents<Component>());

        var cyclesCount = 0;
        while (sourceComponents.Count > 0)
        {
            if (++cyclesCount >= MaxCyclesLimit)
            {
                Logger.Error($"Max cycles limit reached in {nameof(ComponentsCopyEffect)}. Something is broken!");
                return;
            }

            var sourceComponent = sourceComponents.Dequeue();
            var sourceType = sourceComponent.GetType();

            // Ignore component if already exists on Target
            if (targetComponents.Any(x => x.GetType() == sourceType)) continue;

            // Lower priority if contains unfulfilled RequireComponent attribute
            var requireComponentAttribute = sourceType.GetCustomAttributes<RequireComponent>(true);
            if (requireComponentAttribute
                .SelectMany(x => x.m_Type0.YieldWith(x.m_Type1, x.m_Type2))
                .Distinct()
                .Where(x => x != null)
                .Except(targetComponents.Select(x => x.GetType()), new TypeWithBaseEqualityComparer())
                .Any())
            {
                sourceComponents.Enqueue(sourceComponent);
                continue;
            }

            var targetComponent = target.AddComponent(sourceType);
            targetComponents.Add(targetComponent);
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
        foreach (var field in ReflectionUtils
            .GetAllFields(sourceType, true, typeof(MonoBehaviour), BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
            .Concat(ReflectionUtils.GetFieldsWithAttributes(sourceType, true, typeof(SerializeField))))
        {
            field.SetValue(target, field.GetValue(source));
        }
        // Iterate through all public properties
        foreach (var property in ReflectionUtils
            .GetAllProperties(sourceType, true, typeof(MonoBehaviour), BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
        {
            if (property.CanWrite) property.SetValue(target, property.GetValue(source));
        }
    }

    private class TypeWithBaseEqualityComparer : IEqualityComparer<System.Type>
    {
        public bool Equals(System.Type x, System.Type y)
        {
            return x.Equals(y) || x.IsSubclassOf(y) || y.IsSubclassOf(x);
        }

        public int GetHashCode(System.Type obj)
        {
            return 1;
        }
    }
}

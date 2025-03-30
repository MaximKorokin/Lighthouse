using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class ReflectionUtils
{
    public static IEnumerable<Type> GetSubclasses<T>(bool searchInTypeAssembly = false) where T : class
    {
        var assembly = searchInTypeAssembly ? Assembly.GetAssembly(typeof(T)) : Assembly.GetAssembly(typeof(Effect));
        return assembly.GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(T)));
    }

    public static T CreateInstance<T>(Type type, params object[] constructorArgs)
    {
        return (T)Activator.CreateInstance(type, constructorArgs);
    }

    public static IEnumerable<FieldInfo> GetAllFields(Type type, bool recursive, Type recursiveUpTo = null, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly)
    {
        if (type == null)
        {
            return Enumerable.Empty<FieldInfo>();
        }
        var fields = type.GetFields(flags);
        return (recursive && type != recursiveUpTo)
            ? GetAllFields(type.BaseType, true, recursiveUpTo, flags).Concat(fields)
            : fields;
    }

    public static IEnumerable<PropertyInfo> GetAllProperties(Type type, bool recursive = false, Type recursiveUpTo = null, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly)
    {
        if (type == null)
        {
            return Enumerable.Empty<PropertyInfo>();
        }
        var properties = type.GetProperties(flags);
        return (recursive && type != recursiveUpTo)
            ? GetAllProperties(type.BaseType, true, recursiveUpTo, flags).Concat(properties)
            : properties;
    }

    public static IEnumerable<FieldInfo> GetFieldsWithAttributes(Type type, bool recursive, params Type[] attributes)
    {
        if (type == null || attributes == null || attributes.Length == 0)
        {
            return Enumerable.Empty<FieldInfo>();
        }
        return GetAllFields(type, recursive).Where(x => Array.Exists(attributes, y => x.IsDefined(y, false)));
    }

    public static bool HasAttribute(this FieldInfo field, Type attribute)
    {
        return field.IsDefined(attribute, false);
    }

    public static FieldInfo GetBackingField(Type type, string propertyName)
    {
        return type.GetField(string.Format("<{0}>k__BackingField", propertyName), BindingFlags.Instance | BindingFlags.NonPublic);
    }
}

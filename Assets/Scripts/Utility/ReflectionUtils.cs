using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class ReflectionUtils
{
    public static IEnumerable<Type> GetSubclasses<T>() where T : class
    {
        return Assembly.GetAssembly(typeof(T)).GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(T)));
    }

    public static T CreateInstance<T>(Type type, params object[] constructorArgs)
    {
        return (T)Activator.CreateInstance(type, constructorArgs);
    }

    public static IEnumerable<FieldInfo> GetAllFields(Type type, bool recursive = false)
    {
        if (type == null)
        {
            return Enumerable.Empty<FieldInfo>();
        }
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        var fields = type.GetFields(flags);
        return recursive ? GetAllFields(type.BaseType, true).Concat(fields) : fields;
    }

    public static IEnumerable<FieldInfo> GetFieldsWithAttributes(Type type, bool recursive, params Type[] attributes)
    {
        if (type == null || attributes == null || attributes.Length == 0)
        {
            return Enumerable.Empty<FieldInfo>();
        }
        return GetAllFields(type, recursive).Where(x => Array.Exists(attributes, y => x.IsDefined(y, false)));
    }
}
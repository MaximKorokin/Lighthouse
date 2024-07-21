using System;
using UnityEngine;

public class DataMappingAttribute : PropertyAttribute
{
    public Type DBType { get; set; }

    public DataMappingAttribute(Type dbType)
    {
        DBType = dbType;
    }
}

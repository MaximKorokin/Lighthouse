using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableWorldObject : WorldObject
{
    [field: SerializeField]
    public Stats1 Stats { get; protected set; }
}

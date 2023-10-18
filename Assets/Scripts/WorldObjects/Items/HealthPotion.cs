using UnityEngine;

public class HealthPotion : Item
{
    [field: SerializeField]
    public float Health { get; protected set; }
}

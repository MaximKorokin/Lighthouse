using UnityEngine;

[CreateAssetMenu(fileName = "ItemEffect", menuName = "ScriptableObjects/Effects/ItemEffect", order = 1)]
public class ItemEffect : Effect
{
    [field: SerializeField]
    public Item Item { get; private set; }
    [field: SerializeField]
    [field: Range(0f, 1f)]
    public float DropRatio { get; private set; }

    public override void Invoke(CastState castState)
    {
        if (Random.Range(0, DropRatio) <= DropRatio)
        {
            CreateItem(this, castState);
        }
    }

    private static void CreateItem(ItemEffect effect, CastState castState)
    {
        Instantiate(effect.Item, castState.Source.transform.position, Quaternion.identity);
    }
}

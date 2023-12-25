using UnityEngine;

public class ItemEffect : Effect
{
    [field: SerializeField]
    public Item Item { get; private set; }
    [field: SerializeField]
    [field: Range(0f, 1f)]
    public float DropRatio { get; private set; }
    [field: Space]
    [field: SerializeField]
    [field: Range(0, 1)]
    public float InitialForce { get; private set; }
    [field: SerializeField]
    public float InactiveTime { get; private set; }

    public override void Invoke(CastState castState)
    {
        if (Random.Range(0f, 1f) <= DropRatio)
        {
            CreateItem(castState);
        }
    }

    private void CreateItem(CastState castState)
    {
        Item.gameObject.SetActive(false);
        var item = Object.Instantiate(Item, castState.Source.transform.position, Quaternion.identity);
        Item.gameObject.SetActive(true);

        item.InactiveTime = InactiveTime;
        if (InitialForce != 0)
        {
            item.Direction = Random.insideUnitCircle.normalized * InitialForce;
        }

        item.gameObject.SetActive(true);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabletZenGameShard : MonoBehaviour
{
    [field: SerializeField]
    public TabletZenGameShardType ShardType { get; private set; }
    [SerializeField]
    private float _destroyTime;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.color = ShardType switch
        {
            TabletZenGameShardType.Red => Color.red,
            TabletZenGameShardType.Green => Color.green,
            TabletZenGameShardType.Blue => Color.blue,
            _ => Color.white,
        };
    }

    private void OnEnable()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(_destroyTime);
        Destroy(gameObject);
    }

    public void SetShardType(TabletZenGameShardType shardType)
    {
        ShardType = shardType;
    }
}

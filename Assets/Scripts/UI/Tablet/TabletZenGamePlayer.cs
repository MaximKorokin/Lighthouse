using UnityEngine;

public class TabletZenGamePlayer : MonoBehaviour
{
    [SerializeField]
    private float _baseSpeed;

    public Vector2 TargetPosition { get; set; }

    private RectTransform _rectTransform;
    private float _speed;

    private void Awake()
    {
        _rectTransform = transform as RectTransform;

        _speed = _baseSpeed;
    }

    private void Update()
    {
        _rectTransform.anchoredPosition = Vector2.MoveTowards(_rectTransform.anchoredPosition, TargetPosition, _speed * Time.deltaTime);
    }

    public void SetShard(TabletZenGameShardType shardType)
    {
        var currentScore = ConvertingUtils.ToInt(SessionDataStorage.Observable.Get(SessionDataKey.TabletZenGameScore));
        switch (shardType)
        {
            case TabletZenGameShardType.Red:
                currentScore += 5;
                _speed = Mathf.Clamp(_speed + _baseSpeed * 0.1f, _baseSpeed, _baseSpeed * 2);
                transform.localScale = Mathf.Clamp(transform.localScale.x - 0.1f, 0.5f, 1.5f) * Vector3.one;
                break;
            case TabletZenGameShardType.Green:
                currentScore += 10;
                break;
            case TabletZenGameShardType.Blue:
                currentScore += 5;
                _speed = Mathf.Clamp(_speed - _baseSpeed * 0.1f, _baseSpeed, _baseSpeed * 2);
                transform.localScale = Mathf.Clamp(transform.localScale.x + 0.1f, 0.5f, 1.5f) * Vector3.one;
                break;
            default:
                Logger.Error("Unsupported shard type");
                break;
        }
        SessionDataStorage.Observable.Set(SessionDataKey.TabletZenGameScore, currentScore.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.TryGetComponent<TabletZenGameShard>(out var shard))
        {
            SetShard(shard.ShardType);
            Destroy(shard.gameObject);
        }
    }
}

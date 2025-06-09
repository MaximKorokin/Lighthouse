using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabletZenGame : TabletGame
{
    private static readonly TabletZenGameShardType[] ShardTypes = new[] { TabletZenGameShardType.Red, TabletZenGameShardType.Green, TabletZenGameShardType.Blue };

    [SerializeField]
    private TabletZenGameShard _shardTemplate;
    [SerializeField]
    private RectTransform _shardsParent;
    [SerializeField]
    private float _shardsSpawnRate;
    [SerializeField]
    private float _shardsMaxAmount;
    [Space]
    [SerializeField]
    private TMP_Text _pointsText;

    private void Awake()
    {
        _pointsText.text = "0";
        SessionDataStorage.Observable.SetChangeListener(SessionDataKey.TabletZenGameScore, SetScore);
    }

    private void OnEnable()
    {
        StartCoroutine(ShardsSpawnCoroutine());
    }

    private IEnumerator ShardsSpawnCoroutine()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            if (_shardsParent.Cast<Transform>().Count(x => x.gameObject.activeSelf) < _shardsMaxAmount)
            {
                var shard = Instantiate(_shardTemplate);
                shard.transform.SetParent(_shardsParent, false);
                (shard.transform as RectTransform).anchoredPosition = new Vector2(
                    Random.Range(-_shardsParent.rect.width / 2, _shardsParent.rect.width / 2),
                    Random.Range(-_shardsParent.rect.height / 2, _shardsParent.rect.height / 2));
                shard.SetShardType(ShardTypes[Random.Range(0, ShardTypes.Length)]);
                shard.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(1f / _shardsSpawnRate);
        }
    }

    private void SetScore(string value)
    {
        _pointsText.text = value;
    }

    private void OnDestroy()
    {
        SessionDataStorage.Observable.RemoveChangeListener(SessionDataKey.TabletZenGameScore, SetScore);
    }
}

public enum TabletZenGameShardType
{
    Red = 1,
    Green = 2,
    Blue = 3,
}

using UnityEngine;

public abstract class TabletListApplication<TV, TM> : TabletApplication where TV : TabletListApplicationPreview<TM>
{
    [SerializeField]
    private TV _listEntryTemplate;
    [SerializeField]
    private RectTransform _listParent;
    [SerializeField]
    private TabletListApplicationDisplay<TM> _listItemDisplay;

    protected abstract TM[] GetList();

    private void Awake()
    {
        PopulateList();
    }

    protected virtual void PopulateList()
    {
        foreach (var game in GetList())
        {
            var listEntry = Instantiate(_listEntryTemplate);
            listEntry.transform.SetParent(_listParent, false);
            listEntry.SetModel(game);
            listEntry.gameObject.SetActive(true);
            listEntry.Clicked += m =>
            {
                _listItemDisplay.SetModel(m);
                _listParent.gameObject.SetActive(false);
                _listItemDisplay.gameObject.SetActive(true);
            };
        }
    }

    public override void Back()
    {
        if (_listItemDisplay.gameObject.activeSelf)
        {
            _listParent.gameObject.SetActive(true);
            _listItemDisplay.gameObject.SetActive(false);
        }
        else
        {
            base.Back();
        }
    }
}

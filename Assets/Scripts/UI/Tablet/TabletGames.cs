using UnityEngine;

public class TabletGames : TabletListApplication<TabletGamePreview, TabletGameModel>
{
    [SerializeField]
    private TabletGameModel[] _games;

    protected override TabletGameModel[] GetList()
    {
        return _games;
    }
}

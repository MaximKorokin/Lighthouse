using UnityEngine;

public class TabletGameDisplay : TabletListApplicationDisplay<TabletGameModel>
{
    private RectTransform _currentGame;

    public override void SetModel(TabletGameModel model)
    {
        if (_currentGame != null) _currentGame.gameObject.SetActive(false);
        _currentGame = model.GameProvider.UIElement;
        _currentGame.gameObject.SetActive(true);
    }
}

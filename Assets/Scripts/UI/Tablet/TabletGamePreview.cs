using System;
using TMPro;
using UnityEngine;

public class TabletGamePreview : TabletListApplicationPreview<TabletGameModel>
{
    [SerializeField]
    private TMP_Text _gameName;

    public override void SetModel(TabletGameModel model)
    {
        base.SetModel(model);

        _gameName.text = model.Name;
    }
}

[Serializable]
public class TabletGameModel
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public LoadableUIProvider GameProvider { get; private set; }
}

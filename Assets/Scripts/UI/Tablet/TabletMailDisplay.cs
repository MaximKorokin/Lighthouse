using UnityEngine;

public class TabletMailDisplay : TabletListApplicationDisplay<TabletMailModel>
{
    [SerializeField]
    private TextLocalizer _senderText;
    [SerializeField]
    private TextLocalizer _titleText;
    [SerializeField]
    private TextLocalizer _bodyText;

    public override void SetModel(TabletMailModel mail)
    {
        mail.IsRead = true;

        _senderText.SetText($"{mail.Sender}");
        _titleText.SetText($"<i>{mail.Title}</i>");
        _bodyText.SetText($"{mail.Body}");
    }
}

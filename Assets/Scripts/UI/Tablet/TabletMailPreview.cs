using UnityEngine;
using UnityEngine.UI;

public class TabletMailPreview : TabletListApplicationPreview<TabletMailModel>
{
    [SerializeField]
    private Image _readStatusIcon;
    [SerializeField]
    private Sprite _readSprite;
    [SerializeField]
    private Sprite _unreadSprite;
    [Space]
    [SerializeField]
    private TextLocalizer _senderText;
    [SerializeField]
    private TextLocalizer _titleText;

    private void OnEnable()
    {
        // Update mail view
        SetModel(Model);
    }

    public override void SetModel(TabletMailModel mail)
    {
        if (mail == null) return;

        base.SetModel(mail);

        _senderText.SetText($"<b>{mail.Sender}</b>");
        _titleText.SetText($"<b><i>{mail.Title}</i></b>");

        _readStatusIcon.sprite = mail.IsRead ? _readSprite : _unreadSprite;
    }
}

public class TabletMailModel
{
    public bool IsRead { get; set; } = false;

    public string Sender { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public TabletMailModel(string sender, string title, string body)
    {
        Sender = sender;
        Title = title;
        Body = body;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
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
    private TMP_Text _senderText;
    [SerializeField]
    private TMP_Text _titleText;

    private void OnEnable()
    {
        // Update mail view
        SetModel(Model);
    }

    public override void SetModel(TabletMailModel mail)
    {
        if (mail == null) return;

        base.SetModel(mail);
        
        LocalizationManager.SetLanguageChangeListener(_senderText, $"<b>{mail.Sender}</b>", text => _senderText.text = text);
        LocalizationManager.SetLanguageChangeListener(_titleText, $"<b><i>{mail.Title}</i></b>", text => _titleText.text = text);

        _readStatusIcon.sprite = mail.IsRead ? _readSprite : _unreadSprite;
    }

    private void OnDestroy()
    {
        LocalizationManager.RemoveLanguageChangeListener(_senderText);
        LocalizationManager.RemoveLanguageChangeListener(_titleText);
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

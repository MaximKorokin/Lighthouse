using TMPro;
using UnityEngine;

public class TabletMailDisplay : TabletListApplicationDisplay<TabletMailModel>
{
    [SerializeField]
    private TMP_Text _senderText;
    [SerializeField]
    private TMP_Text _titleText;
    [SerializeField]
    private TMP_Text _bodyText;

    public override void SetModel(TabletMailModel mail)
    {
        mail.IsRead = true;

        LocalizationManager.SetLanguageChangeListener(_senderText, $"{mail.Sender}", text => _senderText.text = text);
        LocalizationManager.SetLanguageChangeListener(_titleText, $"<i>{mail.Title}</i>", text => _titleText.text = text);
        LocalizationManager.SetLanguageChangeListener(_bodyText, $"{mail.Body}", text => _bodyText.text = text);
    }

    private void OnDestroy()
    {
        LocalizationManager.RemoveLanguageChangeListener(_senderText);
        LocalizationManager.RemoveLanguageChangeListener(_titleText);
        LocalizationManager.RemoveLanguageChangeListener(_bodyText);
    }
}

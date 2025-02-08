public class TabletMails : TabletListApplication<TabletMailPreview, TabletMailModel>
{
    protected override TabletMailModel[] GetList()
    {
        return new[] {
            new TabletMailModel(
                "!(TabletMail1Sender)",
                "!(TabletMail1Title)",
                "!(TabletMail1Body)"),
            new TabletMailModel(
                "!(TabletMail2Sender)",
                "!(TabletMail2Title)",
                "!(TabletMail2Body)"),
            new TabletMailModel(
                "!(TabletMail3Sender)",
                "!(TabletMail3Title)",
                "!(TabletMail3Body)"),
        };
    }
}

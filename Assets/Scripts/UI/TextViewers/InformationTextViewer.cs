public class InformationTextViewer : AudioTextViewer
{
    public static InformationTextViewer Instance;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    protected override void OnViewStarted()
    {
        Typewriter.Text.enabled = true;
    }

    protected override void OnViewFinished()
    {
        Typewriter.Text.enabled = false;
    }
}

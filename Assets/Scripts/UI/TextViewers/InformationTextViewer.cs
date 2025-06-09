public class InformationTextViewer : AudioTextViewer
{
    protected override void OnViewStarted()
    {
        Typewriter.Text.enabled = true;
    }

    protected override void OnViewFinished()
    {
        Typewriter.Text.enabled = false;
    }
}

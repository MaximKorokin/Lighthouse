public class SpeechBubblePool : ObjectsPool<SpeechBubbleViewer, object>
{
    protected override void Initialize(SpeechBubbleViewer speechBubble, object _)
    {
        speechBubble.gameObject.SetActive(true);
    }

    protected override void Deinitialize(SpeechBubbleViewer speechBubble)
    {

    }
}

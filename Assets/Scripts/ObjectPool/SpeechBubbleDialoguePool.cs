class SpeechBubbleDialoguePool : ObjectsPool<SpeechBubbleDialogueViewer, object>
{
    protected override void Initialize(SpeechBubbleDialogueViewer speechBubble, object _)
    {
        speechBubble.gameObject.SetActive(true);
    }

    protected override void Deinitialize(SpeechBubbleDialogueViewer speechBubble)
    {

    }
}

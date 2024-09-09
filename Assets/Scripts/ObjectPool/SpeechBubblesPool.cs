class SpeechBubblesPool : ObjectsPool<SpeechBubbleController, SpeechBubbleController>
{
    protected override void Initialize(SpeechBubbleController speechBubble, SpeechBubbleController param)
    {
        if (speechBubble == null)
        {
            return;
        }

        speechBubble.gameObject.SetActive(true);
    }

    protected override void Deinitialize(SpeechBubbleController speechBubble)
    {

    }
}

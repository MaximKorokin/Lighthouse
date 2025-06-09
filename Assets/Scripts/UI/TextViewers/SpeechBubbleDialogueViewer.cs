using System.Linq;

public class SpeechBubbleDialogueViewer : DialogueViewerBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnViewStarted()
    {
        var characterObjects = CharactersMapper.GetMappedObjects(CurrentSpeech.CharacterPreviewId);
        if (characterObjects == null || !characterObjects.Any())
        {
            Logger.Warn($"Could not find any mapped characters with id {CurrentSpeech.CharacterPreviewId}");
        }
        var canvasController = characterObjects.Select(x => x.GetComponent<WorldCanvasProvider>()).FirstOrDefault(x => x != null);
        if (canvasController == null)
        {
            Logger.Warn($"Could not find any mapped characters with id {CurrentSpeech.CharacterPreviewId} and attached {nameof(WorldCanvasProvider)}");
        }

        transform.SetParent(canvasController.CanvasController.SpeechBubbleParent.transform, false);

        var characterPreview = CharactersPreviewsDataBase.FindById(CurrentSpeech.CharacterPreviewId);
        if (characterPreview != null)
        {
            SetTypingSound(characterPreview.TypingSound);
        }
    }
}

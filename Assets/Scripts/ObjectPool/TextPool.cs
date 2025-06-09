using TMPro;

class TextPool : ObjectsPool<TMP_Text, object>
{
    protected override void Initialize(TMP_Text text, object _)
    {
        text.gameObject.SetActive(true);
    }

    protected override void Deinitialize(TMP_Text text)
    {

    }
}

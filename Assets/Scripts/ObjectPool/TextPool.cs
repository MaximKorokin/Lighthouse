using TMPro;

class TextPool : ObjectsPool<TMP_Text, TMP_Text>
{
    protected override void Initialize(TMP_Text text, TMP_Text _)
    {
        if (text == null)
        {
            return;
        }

        text.gameObject.SetActive(true);
    }

    protected override void Deinitialize(TMP_Text text)
    {

    }
}

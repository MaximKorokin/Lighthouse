using TMPro;
using UnityEngine;

public class TextLocalizer : MonoBehaviour
{
    [SerializeField]
    private string _text;

    private TMP_Text _textObject;
    public TMP_Text TextObject => gameObject.LazyGetComponent(ref _textObject);

    private void Start()
    {
        if (!string.IsNullOrEmpty(_text))
        {
            LocalizationManager.SetLanguageChangeListener(TextObject, _text, text => TextObject.text = text);
        }
    }

    public void SetText(string text)
    {
        LocalizationManager.SetLanguageChangeListener(TextObject, text, text => TextObject.text = text);
    }

    private void OnDestroy()
    {
        if (_textObject != null && _textObject.gameObject.activeInHierarchy)
        {
            LocalizationManager.RemoveLanguageChangeListener(_textObject);
        }
    }
}

using TMPro;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TMP_Text))]
public class TextLocalizer : MonoBehaviour
{
    [SerializeField]
    private string _text;
    private TMP_Text _textObject;

    private void Start()
    {
        _textObject = GetComponent<TMP_Text>();
        LocalizationManager.SetLanguageChangeListener(_textObject, _text, text => _textObject.text = text);
    }

    private void OnValidate()
    {
        if (_textObject != null)
        {
            LocalizationManager.SetLanguageChangeListener(_textObject, _text, text => _textObject.text = text);
        }
    }

    private void OnDestroy()
    {
        LocalizationManager.RemoveLanguageChangeListener(_textObject);
    }
}

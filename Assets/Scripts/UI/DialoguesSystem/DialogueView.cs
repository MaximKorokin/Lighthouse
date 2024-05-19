using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueView : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Image _characterIcon;
    [SerializeField]
    private TMP_Text _characterNameText;
    [SerializeField]
    private TypewriterText _speechText;

    private Dialogue _currentDialogue;
    private int _currentSpeechIndex;

    public event Action DialogueEnded;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_currentDialogue == null)
        {
            return;
        }
        if (_speechText.IsTyping)
        {
            _speechText.ForceCurrentText();
        }
        else if (_currentDialogue.Speeches.Length <= ++_currentSpeechIndex)
        {
            DialogueEnded?.Invoke();
        }
        else
        {
            FillCurrentView();
        }
    }

    public void SetDialogue(Dialogue dialogue)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        if (dialogue == null)
        {
            Logger.Warn("Dialogue is null");
            DialogueEnded?.Invoke();
            return;
        }
        _currentDialogue = dialogue;
        _currentSpeechIndex = 0;
        FillCurrentView();
    }

    private void FillCurrentView()
    {
        var currentSpeech = _currentDialogue.Speeches[_currentSpeechIndex];
        LocalizationManager.SetLanguageChangeListener(
            _speechText,
            currentSpeech.Text,
            text => _speechText.SetText(text, currentSpeech.TypingSpeed.ToFloatValue()));

        var characterPreview = CharactersPreviewsDataBase.FindById(currentSpeech.CharacterPreviewId);
        if (characterPreview != null)
        {
            _characterIcon.sprite = characterPreview.Icon;
            LocalizationManager.SetLanguageChangeListener(
                _characterNameText,
                $"<color=#{characterPreview.Color.ToHexString()}>{characterPreview.Name}</color>",
                text => _characterNameText.text = text);
        }
    }

    private void OnDestroy()
    {
        LocalizationManager.RemoveLanguageChangeListener(_speechText);
        LocalizationManager.RemoveLanguageChangeListener(_characterNameText);
    }
}

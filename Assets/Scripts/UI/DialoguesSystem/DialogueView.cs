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

    public event Action DialogueFinished;

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
            DialogueFinished?.Invoke();
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
            DialogueFinished?.Invoke();
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
            text => _speechText.SetText(text, currentSpeech.TypingSpeed));

        var characterPreview = CharactersPreviewsDataBase.FindById(currentSpeech.CharacterPreviewId);
        if (characterPreview != null)
        {
            _characterIcon.sprite = characterPreview.Icon;
            var displayName = string.IsNullOrWhiteSpace(characterPreview.DisplayName) ? characterPreview.Name : characterPreview.DisplayName;
            LocalizationManager.SetLanguageChangeListener(
                _characterNameText,
                $"<color=#{characterPreview.Color.ToHexString()}>{displayName}</color>",
                text => _characterNameText.text = text);
        }
    }

    private void OnDestroy()
    {
        LocalizationManager.RemoveLanguageChangeListener(_speechText);
        LocalizationManager.RemoveLanguageChangeListener(_characterNameText);
    }
}

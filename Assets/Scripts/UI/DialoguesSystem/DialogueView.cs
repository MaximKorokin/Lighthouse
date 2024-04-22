using System;
using System.Linq;
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
        if (dialogue == null)
        {
            Logger.Error("Dialogue is null");
            return;
        }
        _currentDialogue = dialogue;
        _currentSpeechIndex = 0;
        FillCurrentView();
    }

    private void FillCurrentView()
    {
        var currentSpeech = _currentDialogue.Speeches[_currentSpeechIndex];
        _speechText.SetText(currentSpeech.Text, currentSpeech.TypingSpeed.ToFloatValue());

        var characterPreview = CharactersPreviewsDataBase.FindById(currentSpeech.CharacterPreviewId);
        if (characterPreview != null)
        {
            _characterIcon.sprite = characterPreview.Icon;
            _characterNameText.text = $"<color=#{characterPreview.Color.ToHexString()}>{characterPreview.Name}</color>";
        }
    }
}

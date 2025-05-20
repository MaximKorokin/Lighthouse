using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ArcUI : MonoBehaviour
{
    [SerializeField]
    private ArcUIActionLog _actionLog;
    [Space]
    [SerializeField]
    private float _itemDisplayTime;
    [Space]
    [SerializeField]
    private GameObject[] _menuItems;
    [SerializeField]
    private GameObject[] _settingsItems;
    [SerializeField]
    private GameObject[] _transferItems;

    private readonly string[] _menuActionLogLines = new string[2] { "> menu", "Loading Menu..." };
    private readonly string[] _settingsActionLogLines = new string[2] { "> settings", "Loading Avatar Settings..." };
    private readonly string[] _transferActionLogLines = new string[4] { "> transfer", "Booting...", "Verifying...", "Entering..." };

    private void OnEnable()
    {
        ViewMainMenu();
    }

    public void ViewMainMenu()
    {
        DisableAllItems();
        this.StartCoroutineSafe(CoroutinesUtils.YieldNull()
            .Then(() => _actionLog.SetPrompt(false))
            .Then(AppendActionLogLines(_menuActionLogLines, 0.2f))
            .Then(EnableItems(_menuItems))
            .Then(() => _actionLog.SetPrompt(true)));
    }

    public void ViewAvatarSettings()
    {
        DisableAllItems();
        this.StartCoroutineSafe(CoroutinesUtils.YieldNull()
            .Then(() => _actionLog.SetPrompt(false))
            .Then(AppendActionLogLines(_settingsActionLogLines, 0.2f))
            .Then(EnableItems(_settingsItems))
            .Then(() => _actionLog.SetPrompt(true)));
    }

    public void ViewTransfer()
    {
        DisableAllItems();
        this.StartCoroutineSafe(CoroutinesUtils.YieldNull()
            .Then(() => _actionLog.SetPrompt(false))
            .Then(EnableItems(_transferItems))
            .Then(AppendActionLogLines(_transferActionLogLines, 1))
            .Then(() => _actionLog.SetPrompt(true))
            .Then(() => SessionDataStorage.Observable.Set(SessionDataKey.SceneTransitionRequested, "true"))
            .Then(CoroutinesUtils.WaitForSeconds(0.5f))
            .Then(() => UIStateManager.Observable.Set(UIState.ArcUI, false)));
    }

    private void DisableAllItems()
    {
        _menuItems.Concat(_settingsItems).Concat(_transferItems).ForEach(x => x.SetActive(false));
    }

    private IEnumerator EnableItems(IList<GameObject> objects)
    {
        return objects
            .Enumerate(x => CoroutinesUtils.WaitForSeconds(_itemDisplayTime)
                .Then(() => x.SetActive(true)));
    }

    private IEnumerator AppendActionLogLines(IList<string> lines, float timeForLine)
    {
        return lines
            .Enumerate(x => EnumeratorUtils.Yield(() => _actionLog.AppendActionLogLine(x))
                .Then(CoroutinesUtils.WaitForSeconds(timeForLine)));
    }

    [Serializable]
    private class ArcUIActionLog
    {
        private const string ActionLogPrompt = "> _";
        private const int MaxLines = 100;

        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private int _maxVisibleLines;

        private readonly List<string> _lines = new();

        public void AppendActionLogLine(string text)
        {
            _lines.Add(text);
            if (_lines.Count > MaxLines)
            {
                _lines.RemoveRange(0, _lines.Count - MaxLines);
            }
            _text.text = string.Join('\n', _lines.Skip(_lines.Count - _maxVisibleLines));
        }

        public void SetPrompt(bool shouldHavePrompt)
        {
            var hasPromptLeading = _lines.Count > 0 && _lines[^1] == ActionLogPrompt;
            if (shouldHavePrompt)
            {
                if (!hasPromptLeading)
                {
                    AppendActionLogLine(ActionLogPrompt);
                }
            }
            else
            {
                if (hasPromptLeading)
                {
                    _lines.RemoveAt(_lines.Count - 1);
                }
            }
        }
    }
}

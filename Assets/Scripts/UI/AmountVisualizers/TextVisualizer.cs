using System.Collections;
using UnityEngine;
using TMPro;

public abstract class TextVisualizer : MonoBehaviour
{
    [SerializeField]
    private bool _useSingle;
    [Header("Multiple")]
    [SerializeField]
    private float _showTime;
    [SerializeField]
    private Vector2 _horizontalRandomMovementRange;
    [SerializeField]
    private Vector2 _verticalRandomMovementRange;

    private TMP_Text _singleText;

    protected virtual void Start()
    {
        if (_useSingle)
        {
            _singleText = TextPool.Take(null);
        }
    }

    protected virtual void OnDestroy()
    {
        if (_singleText != null)
        {
            TextPool.Return(_singleText);
        }
    }

    public virtual TMP_Text VisualizeText(string visualizeString)
    {
        var text = _useSingle ? _singleText : TextPool.Take(null);
        if (!_useSingle)
        {
            StartCoroutine(TextLifetimeCoroutine(text));
        }

        if (double.TryParse(visualizeString, out var result))
        {
            visualizeString = string.Format("{0:0.#}", result);
        }
        text.text = visualizeString;
        return text;
    }

    private IEnumerator TextLifetimeCoroutine(TMP_Text text)
    {
        if (text == null)
        {
            yield break;
        }

        var returnTime = Time.time + _showTime;
        var movementDelta = new Vector3(
            Random.Range(_horizontalRandomMovementRange.x, _horizontalRandomMovementRange.y) / 10,
            Random.Range(_verticalRandomMovementRange.x, _verticalRandomMovementRange.y) / 10);
        while (Time.time < returnTime)
        {
            yield return new WaitForEndOfFrame();
            text.transform.localPosition += movementDelta;
        }
        TextPool.Return(text);
    }
}
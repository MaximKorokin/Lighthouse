using System.Collections;
using UnityEngine;
using TMPro;

public abstract class TextVisualizer : MonoBehaviour
{
    [Header("Multiple")]
    [SerializeField]
    private float _showTime;
    [SerializeField]
    private Vector2 _horizontalRandomMovementRange;
    [SerializeField]
    private Vector2 _verticalRandomMovementRange;

    public virtual TMP_Text VisualizeText(string visualizeString)
    {
        var text = TextPool.Take(null);
        text.StartCoroutine(TextLifetimeCoroutine(text));

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
            Random.Range(_horizontalRandomMovementRange.x, _horizontalRandomMovementRange.y),
            Random.Range(_verticalRandomMovementRange.x, _verticalRandomMovementRange.y));
        while (Time.time < returnTime)
        {
            yield return new WaitForEndOfFrame();
            text.transform.localPosition += movementDelta * Time.deltaTime;
        }
        TextPool.Return(text);
    }
}

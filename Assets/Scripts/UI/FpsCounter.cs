using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class FpsCounter : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 3)]
    private float _updateInterval;
    private TMP_Text _text;

    private int _framesPassedFromLastUpdate;
    private Coroutine _updateCoroutine;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _updateCoroutine = StartCoroutine(UpdateCoroutine());
    }

    private void Update()
    {
        _framesPassedFromLastUpdate++;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite", Justification = "Coroutine")]
    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_updateInterval);
            _text.text = ((int)(_framesPassedFromLastUpdate / _updateInterval)).ToString();
            _framesPassedFromLastUpdate = 0;
        }
    }

    private void OnDestroy()
    {
        if (_updateCoroutine != null)
        {
            StopCoroutine(_updateCoroutine);
        }
    }
}

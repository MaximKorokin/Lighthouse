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
    }

    private void OnEnable()
    {
        if (_updateCoroutine != null)
        {
            StopCoroutine(_updateCoroutine);
        }
        _updateCoroutine = StartCoroutine(UpdateCoroutine());
    }

    private void Update()
    {
        _framesPassedFromLastUpdate++;
    }

    private IEnumerator UpdateCoroutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_updateInterval);
            _text.text = ((int)(_framesPassedFromLastUpdate / _updateInterval)).ToString();
            _framesPassedFromLastUpdate = 0;
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class HPBar
{
    [SerializeField]
    private BarController _barController;
    [SerializeField]
    private Transform _barParent;
    [SerializeField]
    private float _timeToDisappear;

    private Coroutine _disappearCoroutine;

    public void VizualizeHPAmount(float value, float max)
    {
        if (_barController == null)
        {
            return;
        }

        _barController.SetFillRatio(value / max, true);

        if (_timeToDisappear <= 0 || _barParent == null)
        {
            return;
        }

        _barParent.gameObject.SetActive(true);
        if (_disappearCoroutine != null)
        {
            _barController.StopCoroutine(_disappearCoroutine);
        }
        _disappearCoroutine = _barController.StartCoroutine(DisappearCoroutine());
    }

    private IEnumerator DisappearCoroutine()
    {
        yield return new WaitForSeconds(_timeToDisappear);
        _barParent.gameObject.SetActive(false);
    }
}

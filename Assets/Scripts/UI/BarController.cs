using System;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    [SerializeField]
    private Image _barImage;
    [SerializeField]
    private GradientBarControllerData _gradientData;
    [SerializeField]
    private SmoothBarControllerData _smoothData;
    [SerializeField]
    private FadeBarControllerData _fadeData;

    private const float DefaultAlphaValue = 1;

    public void SetFillRatio(float value)
    {
        if (_barImage == null)
        {
            return;
        }
        if (_fadeData.TimeToFade > 0)
        {
            IncrementAlphaValue(DefaultAlphaValue);
        }

        _barImage.fillAmount = value;
        if (_gradientData.ShouldUseGradient)
        {
            _barImage.color = _gradientData.Gradient.Evaluate(value);
        }
    }

    private void Update()
    {
        SetSmoothBar();
        Fade();
    }

    private void SetSmoothBar()
    {
        if (_barImage == null || _smoothData.SmoothBarImage == null)
        {
            return;
        }
        var fillDifference = _barImage.fillAmount - _smoothData.SmoothBarImage.fillAmount;
        if (fillDifference != 0)
        {
            var fillStep = (fillDifference > 0 ? 1 : -1) * _smoothData.SmoothSpeed * Time.deltaTime;
            if (Mathf.Abs(fillDifference) - Mathf.Abs(fillStep) > 0)
            {
                _smoothData.SmoothBarImage.fillAmount += fillStep;
            }
            else
            {
                _smoothData.SmoothBarImage.fillAmount = _barImage.fillAmount;
            }
        }
    }

    private void Fade()
    {
        if (_fadeData.TimeToFade <= 0)
        {
            return;
        }
        var fadeStep = -1 / _fadeData.TimeToFade * Time.deltaTime;
        IncrementAlphaValue(fadeStep);
    }

    private void IncrementAlphaValue(float alpha)
    {
        var colorStep = new Color(0, 0, 0, alpha);
        if (_barImage != null)
        {
            _barImage.color += colorStep;
        }
        if (_smoothData.SmoothBarImage != null)
        {
            _smoothData.SmoothBarImage.color += colorStep;
        }
        if (_fadeData.AdditionalImagesToFade != null)
        {
            foreach (var image in _fadeData.AdditionalImagesToFade)
            {
                image.color += colorStep;
            }
        }
    }
}

[Serializable]
public struct GradientBarControllerData
{
    public bool ShouldUseGradient;
    public Gradient Gradient;
}

[Serializable]
public struct SmoothBarControllerData
{
    public Image SmoothBarImage;
    public float SmoothSpeed;
}

[Serializable]
public struct FadeBarControllerData
{
    public float TimeToFade;
    public Image[] AdditionalImagesToFade;
}

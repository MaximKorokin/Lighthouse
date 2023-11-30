using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour, ICopyable<BarController>
{
    [field: SerializeField]
    public Image BackgroundImage { get; set; }
    [field: SerializeField]
    public Image BarImage { get; set; }
    [field: Header("Smooth")]
    [field: SerializeField]
    public Image SmoothBarImage { get; set; }
    [field: SerializeField]
    public float SmoothSpeed { get; set; }
    [field: Header("Gradient")]
    [field: SerializeField]
    public bool ShouldUseGradient { get; set; }
    [field: SerializeField]
    public Gradient Gradient { get; set; }
    [field: Header("Fade")]
    [field: SerializeField]
    public float TimeToFade { get; set; }

    private const float DefaultAlphaValue = 1;

    private float _currentRatio;

    public void SetFillRatio(float value)
    {
        if (BarImage == null || value < 0 || value > 1)
        {
            return;
        }
        if (TimeToFade > 0)
        {
            IncrementAlphaValue(DefaultAlphaValue);
        }

        _currentRatio = value;
        BarImage.fillAmount = _currentRatio;
        if (ShouldUseGradient)
        {
            BarImage.color = Gradient.Evaluate(_currentRatio);
        }
    }

    private void Update()
    {
        SetSmoothBar();
        Fade();
    }

    private void SetSmoothBar()
    {
        if (SmoothBarImage == null)
        {
            return;
        }
        var fillDifference = _currentRatio - SmoothBarImage.fillAmount;
        if (fillDifference != 0)
        {
            var fillStep = (fillDifference > 0 ? 1 : -1) * SmoothSpeed * Time.deltaTime;
            if (Mathf.Abs(fillDifference) - Mathf.Abs(fillStep) > 0)
            {
                SmoothBarImage.fillAmount += fillStep;
            }
            else
            {
                SmoothBarImage.fillAmount = _currentRatio;
            }
        }
    }

    private void Fade()
    {
        if (TimeToFade <= 0)
        {
            return;
        }
        var fadeStep = -1 / TimeToFade * Time.deltaTime;
        IncrementAlphaValue(fadeStep);
    }

    private void IncrementAlphaValue(float alpha)
    {
        var colorStep = new Color(0, 0, 0, alpha);
        if (BackgroundImage != null)
        {
            BackgroundImage.color += colorStep;
        }
        if (BarImage != null)
        {
            BarImage.color += colorStep;
        }
        if (SmoothBarImage != null)
        {
            SmoothBarImage.color += colorStep;
        }
    }

    public void CopyTo(BarController obj)
    {
        if (obj == null)
        {
            return;
        }
        GetComponent<RectTransform>().CopyTo(obj.GetComponent<RectTransform>());

        CopyImage(BackgroundImage, obj.BackgroundImage);
        CopyImage(BarImage, obj.BarImage);
        CopyImage(SmoothBarImage, obj.SmoothBarImage);

        obj.SmoothSpeed = SmoothSpeed;
        obj.ShouldUseGradient = ShouldUseGradient;
        obj.Gradient = Gradient;
        obj.TimeToFade = TimeToFade;

        obj._currentRatio = _currentRatio;
    }

    private void CopyImage(Image source, Image target)
    {
        if (target == null)
        {
            return;
        }
        if (source == null)
        {
            target.enabled = false;
            return;
        }
        target.enabled = true;
        source.CopyTo(target);
        source.GetComponent<RectTransform>().CopyTo(target.GetComponent<RectTransform>());
    }
}

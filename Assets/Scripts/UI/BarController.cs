using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour, ICopyable<BarController>
{
    [field: SerializeField]
    public Image BackgroundImage { get; set; }
    [field: SerializeField]
    public Image ForegroundImage { get; set; }
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
    public float TimeToStartFade { get; set; }
    [field: SerializeField]
    public float TimeToFade { get; set; }

    private const float DefaultAlphaValue = 1;

    public float CurrentFillRatio { get; private set; }
    private float _currentStartFadeTime;

    public void SetFillRatio(float value)
    {
        if (BarImage == null || value < 0 || value > 1 || value is float.NaN)
        {
            return;
        }
        if (TimeToFade > 0)
        {
            _currentStartFadeTime = Time.time + TimeToStartFade;
            IncrementAlphaValue(DefaultAlphaValue);
        }

        CurrentFillRatio = value;
        BarImage.fillAmount = CurrentFillRatio;
        if (ShouldUseGradient)
        {
            BarImage.color = Gradient.Evaluate(CurrentFillRatio);
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
        var fillDifference = CurrentFillRatio - SmoothBarImage.fillAmount;
        if (fillDifference != 0)
        {
            var fillStep = (fillDifference > 0 ? 1 : -1) * SmoothSpeed * Time.deltaTime;
            if (Mathf.Abs(fillDifference) - Mathf.Abs(fillStep) > 0)
            {
                SmoothBarImage.fillAmount += fillStep;
            }
            else
            {
                SmoothBarImage.fillAmount = CurrentFillRatio;
            }
        }
    }

    private void Fade()
    {
        if (TimeToFade <= 0 || Time.time < _currentStartFadeTime)
        {
            return;
        }
        var fadeStep = -1 / TimeToFade * Time.deltaTime;
        IncrementAlphaValue(fadeStep);
    }

    private void IncrementAlphaValue(float alphaStep)
    {
        var newAlpha = Mathf.Clamp01(BackgroundImage.color.a + alphaStep);
        if (BackgroundImage != null)
        {
            SetAlpha(BackgroundImage, newAlpha);
        }
        if (ForegroundImage != null)
        {
            SetAlpha(ForegroundImage, newAlpha);
        }
        if (BarImage != null)
        {
            SetAlpha(BarImage, newAlpha);
        }
        if (SmoothBarImage != null)
        {
            SetAlpha(SmoothBarImage, newAlpha);
        }
    }

    private void SetAlpha(Image image, float alpha)
    {
        image.color = new Color(
            image.color.r,
            image.color.g,
            image.color.b,
            alpha);
    }

    public void CopyTo(BarController obj)
    {
        if (obj == null)
        {
            return;
        }
        GetComponent<RectTransform>().CopyTo(obj.GetComponent<RectTransform>());

        CopyImage(BackgroundImage, obj.BackgroundImage);
        CopyImage(ForegroundImage, obj.ForegroundImage);
        CopyImage(BarImage, obj.BarImage);
        CopyImage(SmoothBarImage, obj.SmoothBarImage);

        obj.SmoothSpeed = SmoothSpeed;
        obj.ShouldUseGradient = ShouldUseGradient;
        obj.Gradient = Gradient;
        obj.TimeToFade = TimeToFade;
        obj.TimeToStartFade = TimeToStartFade;

        obj.CurrentFillRatio = CurrentFillRatio;
    }

    private static void CopyImage(Image source, Image target)
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

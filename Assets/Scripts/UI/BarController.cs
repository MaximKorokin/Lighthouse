using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BarController : MonoBehaviour
{
    [SerializeField]
    private Gradient _gradient;

    private Image _image;
    private Image Image
    {
        get => _image = _image != null ? _image : GetComponent<Image>();
    }

    public void SetFillRatio(float value, bool shouldUseGradient = false)
    {
        Image.fillAmount = value;
        if (shouldUseGradient)
        {
            Image.color = _gradient.Evaluate(value);
        }
    }
}

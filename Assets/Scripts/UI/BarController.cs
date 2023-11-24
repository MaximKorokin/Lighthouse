using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BarController : MonoBehaviour
{
    [SerializeField]
    private Gradient _gradient;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetFillRatio(float value)
    {
        _image.fillAmount = value;
    }
}

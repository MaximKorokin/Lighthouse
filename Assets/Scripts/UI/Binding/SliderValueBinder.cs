using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderValueBinder : ValueBinder
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public override object GetCurrentValue()
    {
        return _slider.value;
    }

    public override void SetValue(object obj)
    {
        _slider.value = ConvertingUtils.ToFloat(obj);
    }
}

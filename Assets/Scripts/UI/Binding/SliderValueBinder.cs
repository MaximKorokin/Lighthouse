using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderValueBinder : ConfigValueBinder<float>
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public override float GetCurrentValue()
    {
        return _slider.value;
    }

    public override void SetValue(float value)
    {
        _slider.value = value;
    }

    public override float ConvertToValue(string str)
    {
        return ConvertingUtils.ToFloat(str);
    }
}

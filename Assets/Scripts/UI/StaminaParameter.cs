using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaParameter : UIParameter<float>
{
    [SerializeField] private Image imageStaminaValue;
    public override void ChangeValue(float value)
    {
        var settingValue = value / 100f;
        imageStaminaValue.fillAmount = Mathf.Clamp(settingValue, 0f, 1f);
    }
}

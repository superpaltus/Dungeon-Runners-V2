using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaParameter : UIParameterValue<float>
{
    [SerializeField] private Image imageStaminaValue;

    public override void OnStart(GameObject player)
    {
        player.GetComponent<Stamina>().StaminaChanged += ChangeValue;
    }

    public override void ChangeValue(float value)
    {
        var settingValue = value / 100f;
        imageStaminaValue.fillAmount = Mathf.Clamp(settingValue, 0f, 1f);
    }
}

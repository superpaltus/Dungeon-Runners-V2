using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceParameter : UIParameterValue<float>
{
    [SerializeField] private Slider sliderDistanceValue;

    private bool isSet = false;

    private float startValue = 0f;

    public override void OnStart(GameObject player)
    {
        player.GetComponent<Movement>().DistanceChanged += ChangeValue;
    }

    public override void ChangeValue(float value)
    {
        if (!isSet)
        {
            startValue = value;
            isSet = true;
        }
        sliderDistanceValue.value = value / startValue;
    }
}

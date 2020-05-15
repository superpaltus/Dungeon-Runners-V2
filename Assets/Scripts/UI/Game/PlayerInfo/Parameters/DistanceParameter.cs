using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceParameter : UIParameterValue<float>
{
    [SerializeField] private Text textDistanceValue;

    public override void OnStart(GameObject player)
    {
        player.GetComponent<Movement>().DistanceChanged += ChangeValue;
    }

    public override void ChangeValue(float value)
    {
        textDistanceValue.text = value.ToString("0.0");
    }
}

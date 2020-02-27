using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldParameter : UIParameter<int>
{
    [SerializeField] private Text textGoldValue;

    public override void ChangeValue(int value)
    {
        textGoldValue.text = value.ToString();
    }
}

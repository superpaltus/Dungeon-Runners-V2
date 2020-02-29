using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldParameter : UIParameterValue<int>
{
    [SerializeField] private Text textGoldValue;

    public override void OnStart(GameObject player)
    {
        player.GetComponent<PlyerGoldCollector>().GoldChanged += ChangeValue;
    }

    public override void ChangeValue(int value)
    {
        textGoldValue.text = value.ToString();
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private UIParameter[] uIParametersT;

    private void OnEnable()
    {
        if (player != null)
        {
            foreach (var uIParameter in uIParametersT)
            {
                uIParameter.OnStart(player);
            }
        }
    }
}

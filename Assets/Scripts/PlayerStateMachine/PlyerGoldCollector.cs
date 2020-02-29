using System;
using UnityEngine;

public class PlyerGoldCollector : MonoBehaviour, IDamageable
{
    public Action<int> GoldChanged;
    private int currentGold;

    public void SetDamage(int damage)
    {
        ChangeGold(-damage);
    }

    public void ChangeGold(int value)
    {
        currentGold = Mathf.Clamp(currentGold += value, 0, int.MaxValue);
        GoldChanged?.Invoke(currentGold);
        Debug.Log($"Current gold is: {currentGold}");
    }
}

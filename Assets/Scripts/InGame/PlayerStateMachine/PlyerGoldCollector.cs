using System;
using UnityEngine;

public class PlyerGoldCollector : MonoBehaviour, IDamageable
{
    [SerializeField] private GoldCoin goldCoinPrefab;
    [SerializeField] private float offset = 1f; 
    public Action<int> GoldChanged;
    private int currentGold;

    public void SetDamage(int damage)
    {
        ChangeGold(-damage);
    }

    public void ChangeGold(int value)
    {
        int goldBefore = currentGold;
        currentGold = Mathf.Clamp(currentGold += value, 0, int.MaxValue);
        GoldChanged?.Invoke(currentGold);
        Debug.Log($"Current gold is: {currentGold}");

        if (value < 0)
        {
            int spawningGoldCoinsNumber = goldBefore - currentGold;

            for (int i = 0; i < spawningGoldCoinsNumber; i++)
            {
                var spawnPosition = transform.position + Vector3.up * offset;
                var newGold = Instantiate(goldCoinPrefab, spawnPosition, Quaternion.identity);
                newGold.Spread();
            }
        }
    }
}

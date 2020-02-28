using UnityEngine;
using System.Collections;
using System;

public class Stamina : MonoBehaviour
{
    // For displaying stamina value by filling image on UI, it is nessesary to hold stamina value
    // betweeen 0f and 100f.

    public Action<float> StaminaChanged;
    public Action StaminaExpired;
    private float currentValue = 100f;

    public bool Spend(float value)
    {
        var valueAfterSpending = Mathf.Clamp(currentValue - value, 0f, 100f);

        if (valueAfterSpending >= 0f)
        {
            currentValue = valueAfterSpending;
            StaminaChanged?.Invoke(currentValue);
            return true;
        }
        else
        {
            StaminaExpired?.Invoke();
            return false;
        }
    }

    public void Regain()
    {
        currentValue = 100f;
    }
}

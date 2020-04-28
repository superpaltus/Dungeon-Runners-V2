using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelTimer : MonoBehaviour
{
    [SerializeField] private Text timerText;
    
    private float currentTime;
    private bool isActive;

    private void Update()
    {
        if (isActive)
        {
            currentTime -= Time.deltaTime;
            int currentTimeInt = (int)currentTime;
            timerText.text = $"{currentTimeInt / 60}:{currentTimeInt % 60}";

            if (currentTime <= 0)
            {
                Debug.Log("End timer complite. Level ends."); // TODO: connect end level elements
            }
        }
    }

    public void SetTime(float time)
    {
        currentTime = time;
    }

    public void Activate()
    {
        isActive = true;
    }
}

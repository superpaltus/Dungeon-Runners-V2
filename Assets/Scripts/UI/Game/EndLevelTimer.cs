using System;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelTimer : MonoBehaviour
{
    public static Action Complite;

    [SerializeField] private Text timerText;
    [SerializeField] private float timeToSet = 10f;
    
    private float currentTime;
    private bool isActive;

    private void OnEnable()
    {
        EndLevelTrigger.EndLevelReached += Activate;
    }

    private void OnDisable()
    {
        EndLevelTrigger.EndLevelReached -= Activate;
    }

    private void Update()
    {
        if (isActive)
        {
            currentTime -= Time.deltaTime;
            int currentTimeInt = (int)currentTime;
            timerText.text = $"{currentTimeInt}";

            if (currentTime <= 0)
            {
                Debug.Log("End timer complite. Level ends."); // TODO: connect end level elements
                isActive = false;
                Complite?.Invoke();
                timerText.gameObject.SetActive(false);
            }
        }
    }

    public void Activate()
    {
        timerText.gameObject.SetActive(true);
        currentTime = timeToSet;
        isActive = true;
    }
}

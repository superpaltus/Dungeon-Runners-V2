using System;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    public static Action EndLevelReached;

    public static Vector3 endLevelPosition;

    private bool isActivate = false;

    private void Start()
    {
        endLevelPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!isActivate && other.GetComponent<Movement>())
        {
            Debug.Log("You reached end of a level!");
            isActivate = true;
            EndLevelReached?.Invoke();
        }
    }
}

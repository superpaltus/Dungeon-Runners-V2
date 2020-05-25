using System;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    public static Action EndLevelReached;

    public static Vector3 endLevelPosition;

    private void Start()
    {
        endLevelPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Movement>())
        {
            Debug.Log("You reached end of a level!");
            EndLevelReached?.Invoke();
        }
    }
}

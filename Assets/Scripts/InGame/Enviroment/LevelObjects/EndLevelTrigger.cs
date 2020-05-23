using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
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
        }
    }
}

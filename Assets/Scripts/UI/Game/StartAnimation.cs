using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    public void OnAnimationStart()
    {
        Time.timeScale = 0f;
        Debug.Log("Time now is: " + Time.timeScale);
    }

    public void OnAnimationEnd()
    {
        Time.timeScale = 1f;
        Debug.Log("Time now is: " + Time.timeScale);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static Sounds instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip coin;

    private void OnEnable()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHit()
    {
        audioSource.PlayOneShot(hit);
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jump);
    }

    public void PlayWin()
    {
        audioSource.PlayOneShot(win);
    }

    public void PlayCoin()
    {
        audioSource.PlayOneShot(coin);
    }
}

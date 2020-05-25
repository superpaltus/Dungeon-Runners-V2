using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    
    public void SetDamage(int damage)
    {
        Sounds.instance.PlayHit();
        health = Mathf.Max(health - damage, 0);
        if (health == 0) Destroy(gameObject);
    }
}
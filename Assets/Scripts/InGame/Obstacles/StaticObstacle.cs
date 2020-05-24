using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StaticObstacle : MonoBehaviour
{
    [SerializeField] private float pushForce;
    [SerializeField] private int damageInGold;
    [SerializeField] private float stunTime;
    

    private void OnCollisionStay2D(Collision2D other)
    {
        var movement = other.gameObject.GetComponent<Movement>();
        if (movement == null) return;
        var direction = (other.transform.position - transform.position).normalized;
        movement.TakeDamage(direction, pushForce, damageInGold, stunTime);
    }
}
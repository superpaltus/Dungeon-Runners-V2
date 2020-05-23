using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Spikes : MonoBehaviour
{
    [SerializeField] private float attackPower = 10f;
    [SerializeField] private int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerMovement = collision.gameObject.GetComponent<Movement>();

        if (playerMovement != null)
        {
            var attackImpulseDirection = (playerMovement.transform.position - transform.parent.position).normalized;
            playerMovement.TakeDamage(attackImpulseDirection, attackPower, damage);
        }
    }
}

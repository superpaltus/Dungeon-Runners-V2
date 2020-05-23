using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class WeaponColliderAttack : MonoBehaviour
{
    [SerializeField] private float attackPower = 500f;
    [SerializeField] private int damage = 10;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerMovement = collision.gameObject.GetComponent<Movement>();

        if (playerMovement != null)
        {
            var attackImpulseDirection = (playerMovement.transform.position - transform.parent.position).normalized;
            playerMovement.TakeDamage(attackImpulseDirection, attackPower, damage);
            transform.parent.gameObject.SetActive(false);
        }

    }
}

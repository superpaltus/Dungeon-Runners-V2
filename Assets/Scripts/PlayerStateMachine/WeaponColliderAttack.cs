using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class WeaponColliderAttack : MonoBehaviour
{
    [SerializeField] private float attackPower = 500f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerMovement = collision.gameObject.GetComponent<Movement>();

        if (playerMovement != null)
        {
            playerMovement.SetState(new Stunned(playerMovement));
            var attackImpulseDirection = (playerMovement.transform.position - transform.parent.position).normalized;
            playerMovement.Rigidbody2d.AddForce(attackImpulseDirection * attackPower);
            transform.parent.gameObject.SetActive(false);
        }

    }
}

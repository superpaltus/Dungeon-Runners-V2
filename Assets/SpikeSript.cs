using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSript : MonoBehaviour
{
    [SerializeField] private float attackPower = 500f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerMovement = collision.gameObject.GetComponent<Movement>();

        if (playerMovement != null)
        {
            playerMovement.SetState(new Stunned(playerMovement));
            var attackImpulseDirection = (playerMovement.transform.position - transform.position).normalized;
            playerMovement.Rigidbody2d.AddForce(attackImpulseDirection * attackPower);
            //transform.parent.gameObject.SetActive(false);
        }

        var AddScore = collision.gameObject.GetComponent<ScoreCounter>();
        if (AddScore != null)
        {
            AddScore.ChangeScore(-10);
        }

    }
}

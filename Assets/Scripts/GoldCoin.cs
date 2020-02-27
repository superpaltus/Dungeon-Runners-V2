using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class GoldCoin : MonoBehaviour
{
    [SerializeField] private int goldValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerGoldCollector = collision.GetComponent<PlyerGoldCollector>();
        
        if (playerGoldCollector != null)
        {
            playerGoldCollector.ChangeGold(goldValue);
            Destroy(gameObject);
        }
    }
}

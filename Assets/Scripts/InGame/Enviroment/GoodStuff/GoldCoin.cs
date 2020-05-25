using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class GoldCoin : MonoBehaviour
{
    [SerializeField] private int goldValue = 1;
    [SerializeField] private float spreadOffset = 1f;
    [SerializeField] private float spreadForce = 5f;

    public Rigidbody2D Rigidbody2d { get; private set; }
    public Collider2D Collider2d { get; private set; }

    private void Awake()
    {
        Rigidbody2d = GetComponent<Rigidbody2D>();
        Collider2d = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerGoldCollector = collision.GetComponent<PlyerGoldCollector>();
        
        if (playerGoldCollector != null)
        {
            Sounds.instance.PlayCoin();
            playerGoldCollector.ChangeGold(goldValue);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerGoldCollector = collision.gameObject.GetComponent<PlyerGoldCollector>();

        if (playerGoldCollector != null)
        {
            Sounds.instance.PlayCoin();
            playerGoldCollector.ChangeGold(goldValue);
            Destroy(gameObject);
        }
    }

    public void Spread()
    {
        Rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        Rigidbody2d.mass = 0.01f;
        Collider2d.isTrigger = false;
        var spreadDirection = new Vector2(Random.Range(-.5f, .5f), Random.Range(.5f, 1f)).normalized;
        Debug.Log($"spread direction{spreadDirection}");
        Rigidbody2d.AddForce(spreadDirection * spreadForce, ForceMode2D.Impulse);

    }
}

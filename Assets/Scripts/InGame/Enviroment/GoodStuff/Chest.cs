using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Sprite openChest;
    [SerializeField] private GoldCoin goldCoinPrefab;
    [SerializeField] private float spawnCoinsOffsetY = 1f;
    [SerializeField] private int coinsPerHit = 5;
    [SerializeField] private int lifes = 3;

    private Animator hitAnimation;

    private bool isOpen;

    private void Start()
    {
        hitAnimation =  GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<WeaponColliderAttack>() && !isOpen)
        {
            lifes--;
            hitAnimation.SetTrigger("isHit");
            for (int i = 0; i < coinsPerHit; i++)
            {
                var spawnPosition = transform.position + Vector3.up * spawnCoinsOffsetY;
                var newGold = Instantiate(goldCoinPrefab, spawnPosition, Quaternion.identity);
                newGold.Spread();
            }

            if (lifes <= 0)
            {
                GetComponent<SpriteRenderer>().sprite = openChest;
                isOpen = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestBlock : MonoBehaviour
{
    public float HP = 1;
    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) Die();
    }
    public void TakeDmg(float dmg)
    {
        HP -= dmg;
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}

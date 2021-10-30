using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int enemyHealth = 5;
    [SerializeField] private int damagedReceivedOnHit = 5;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player Bullet")
        {
            enemyHealth = enemyHealth - damagedReceivedOnHit;
            Destroy(other.gameObject);
            Debug.Log("Enemy was hit by a bullet!");
        }
    }

// Update is called once per frame
void Update()
    {
        if (enemyHealth < 0)
            Destroy(this.gameObject);
    }
}

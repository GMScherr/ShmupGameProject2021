using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int enemyHealth = 5;
    [SerializeField] private int damagedReceivedOnHit = 5;
    [SerializeField] private int damageReceivedBomb = 50;
    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private float deathTimer = 5;
    [SerializeField] private int ammoCount = 0;
    [SerializeField] private float ammoLossRate = 0;
    private float ammoLossRateAux;
    [SerializeField] private bool isFiring = true;
    [SerializeField] private GameObject AmmoCrate;
    private bool crateHasNotDropped = true;

    public void setFiringTrue ()
    {
        isFiring = true;
    }
    public void setFiringFalse ()
    {
        isFiring = false;
    }
    public bool getFiringStatus()
    {
        return isFiring;
    }

    void Start()
    {
        ammoLossRateAux = ammoLossRate;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player Bullet")
        {
            enemyHealth = enemyHealth - damagedReceivedOnHit;
            Destroy(other.gameObject);
            Debug.Log("Enemy was hit by a bullet!");
        }
        if (other.tag == "Bomb")
        {
            enemyHealth = enemyHealth - damageReceivedBomb;
            Debug.Log("Enemy was hit by a bomb!");
        }
    }

void Update()
    {
        if ((deathTimer <= 0) && (destroyOnDeath))
            Destroy(this.gameObject);
        if (deathTimer < 0)
            deathTimer = 0;
        if (enemyHealth <= 0)
            deathTimer = deathTimer - Time.deltaTime;
        ammoLossRateAux = ammoLossRateAux - Time.deltaTime;
        if ((ammoLossRateAux < 0)&&(isFiring))
        {
            ammoLossRateAux = ammoLossRate;
            ammoCount--;
        }
        if ((ammoCount <= 0) || (enemyHealth <= 0))
            setFiringFalse();
        if ((enemyHealth <= 0) && (crateHasNotDropped) && (ammoCount > 0))
        {
            crateHasNotDropped = false;
            GameObject CurrentCrate = Instantiate(AmmoCrate, transform.position, Quaternion.identity);
            CurrentCrate.GetComponent<AmmoCrateController>().setAmmoCount(ammoCount);
        }
    }
}

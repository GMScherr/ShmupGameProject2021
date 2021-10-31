using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int enemyHealth = 5;
    [SerializeField] private int damagedReceivedOnHit = 5;
    [SerializeField] private int ammoCount = 0;
    [SerializeField] private float ammoLossRate = 0;
    private float ammoLossRateAux;
    [SerializeField] private bool isFiring = true;

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
    }

// Update is called once per frame
void Update()
    {
        if (enemyHealth < 0)
            Destroy(this.gameObject);
        ammoLossRateAux = ammoLossRateAux - Time.deltaTime;
        if ((ammoLossRateAux < 0)&&(isFiring))
        {
            ammoLossRateAux = ammoLossRate;
            ammoCount--;
        }
        if (ammoCount <= 0)
            setFiringFalse();
    }
}

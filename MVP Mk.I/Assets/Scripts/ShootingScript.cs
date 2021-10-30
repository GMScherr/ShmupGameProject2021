using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public float shotTime = 0.5f; //In seconds
    private float auxShotTime;
    public GameObject bulletPrefab;
    private PlayerMovementScript player;
    public float enemySpeedX = 3;
    void Start()
    {
        ResetCoolDown();
        player = FindObjectOfType<PlayerMovementScript>();
    }

    void ResetCoolDown()
    {
        auxShotTime = shotTime;
    }

    void TrackPlayer()
    {
        float enemyX = transform.position.x;
        float playerX = player.transform.position.x;
        if (playerX > enemyX)
            enemyX = enemyX + (enemySpeedX * Time.deltaTime);
        else
            enemyX = enemyX - (enemySpeedX * Time.deltaTime);
        transform.position = new Vector3(enemyX, this.transform.position.y, this.transform.position.z);
    }

    void Shoot() {
        ResetCoolDown();
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);

    }

    void Update()
    {
        auxShotTime = auxShotTime - Time.deltaTime;
        if (auxShotTime < 0)
            Shoot();
    }

    private void LateUpdate()
    {
        TrackPlayer();
    }
}

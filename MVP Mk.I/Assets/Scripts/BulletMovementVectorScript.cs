using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script that sets the velocity of a bullet. Speed parameters and modifications to the default lifespan (5 seconds) are the responsability of the GameObject who instantiated the bullet.
public class BulletMovementVectorScript : MonoBehaviour
{
    
    private float bulletSpeedX;
    private float bulletSpeedY;
    [SerializeField] private float bulletLifeTime = 5;
    private Rigidbody2D RB;
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    public void setBulletSpeedX (float speed)
    {
        this.bulletSpeedX = speed;
    }

    public void setBulletSpeedY(float speed)
    {
        this.bulletSpeedY = speed;
    }

    public void setBulletLifespan(float lifespan)
    {
        bulletLifeTime = lifespan;
    }

    void Update()
    {
        bulletLifeTime = bulletLifeTime - Time.deltaTime;
        if (bulletLifeTime < 0)
            Destroy(this.gameObject);
        RB.velocity = new Vector3(bulletSpeedX, bulletSpeedY, 0);
        //RB.velocity = Quaternion.Euler(new Vector3(0, 0, 30))*RB.velocity;
        //Moves the shot stream by (0,0,theta) degrees to the side of the usual point of shot
        //I've been staring at this line of code and still have no idea why it works, I just know it does. Is it because of weird Quartenion BS ? Who knows.
    }
}

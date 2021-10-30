using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovementVectorScript : MonoBehaviour
{
    //Script that sets the velocity of a bullet. Speed parameters and modifications to the default lifespan (5 seconds) are the responsability of the GameObject who instantiated the bullet.
    private float bulletSpeedX;
    private float bulletSpeedY;
    [SerializeField] private float bulletLifeTime = 5;
    private Rigidbody2D RB;
    [SerializeField] private bool alignToVector = false;
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

    void Update()
    {
        bulletLifeTime = bulletLifeTime - Time.deltaTime;
        if (bulletLifeTime < 0)
            Destroy(this.gameObject);
        RB.velocity = new Vector3(bulletSpeedX, bulletSpeedY, 0);
        if (alignToVector)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Vector2.SignedAngle(Vector2.up,RB.velocity)));
        //RB.velocity = Quaternion.Euler(new Vector3(0, 0, 30))*RB.velocity;
        //Moves the shot stream by (0,0,theta) degrees to the side of the usual point of shot

    }
}

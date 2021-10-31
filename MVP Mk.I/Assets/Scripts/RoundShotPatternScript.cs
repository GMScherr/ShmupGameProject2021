using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Pattern script for a round shot pattern. By tinkering with its parameters you may change pattern behaviour as follows :
//-Shot Interval describes, in seconds, the amount of time waited between each shot. It is set to 0.0005 by default, but mind you that this is *ridiculously* fast and will flood the screen. 0.2 is more acceptable.
//-The X and Y speed parameters define the velocity of the bullet in the X and Y axis. When both values are the same, the pattern is circular. By making them different one can create elliptical patterns.
//-Angle is the angle of the first shot stream in a pattern with multiple streams. 0 degrees points at the 3 o'clock position.
//-Angle increment is the amount the pattern will rotate after each shot if it has been chosen to be spiral.
//-Number of Shots defines how many streams of bullets the pattern will fire.
//-Spiral Pattern defines if the pattern will slowly rotate instead of firing static streams around the object who fires.
//-Clockwise Spin defines the direction of rotation, should the pattern rotate.

//Tips on using this script :
//This script is best used in conjunction with other scripts. Spiral mode is ideal for both moving and static enemies, which static is better used on moving enemies. Static can be used to create impassable
//"barriers" in order to confine the player's movement to one sector of the screen. A combination of slow bullet speed, a large amount of streams and a low shot interval can be used to "flood" the screen.
//Careful not to make the pattern needlessly hard, though. A large interval with many streams of fast bullets can be used in order to create a "pulsating" pattern. Best done in conjunction with an aimed
//pattern in order to increase its difficulty. If used alone, best done by using multiple enemies with pulsating patterns at once, forcing the player to either kill them fast or be overwhelmed. Multiple
//instances of this script centered on the same location can be used to create interesting patterns. Examples :
//-Two instances of this script with few streams of fast bullets with a small interval can be used to replicate the red bullet pattern seen in DaiOuJou's famous "Washing Machine" pattern.
//-Three instances of this script, two of them firing many streams of slowly contra-rotating fast bullets plus another one doing the "pulsating" pattern can be used to faithfully recreate Aya's final Spellcard
// from Mountain of Faith. Trust me, I've tried and it works. Takes a bit of tweaking of the parameters and my own bullet assets aren't as pretty as ZUN's, but it works. Also Aya best girl.
public class RoundShotPatternScript : MonoBehaviour
{
    [SerializeField] private float shotInterval = 0.0005f;
    [SerializeField] private bool isFiring = true;
    private float shotIntervalAux;
    [SerializeField] private GameObject bullet;
    [SerializeField] private EnemyHealthController Resources;
    [SerializeField] private float bulletSpeedX = 60;
    [SerializeField] private float bulletSpeedY = 60;
    [SerializeField] private float angle = 0;
    [SerializeField] private float angleIncrement = 38;
    [SerializeField] private int numberOfShots = 1;
    [SerializeField] private bool spiralPattern = true;
    [SerializeField] private bool clockWiseSpin = false;

    public void setFiringStatusTrue()
    {
        isFiring = true;
    }
    public void setFiringSatusFalse()
    {
        isFiring = false;
    }

    void Start()
    {
        shotIntervalAux = shotInterval;
    }

    void Shoot()
    {
        if (spiralPattern)
            angle = angle + angleIncrement;
        shotIntervalAux = shotInterval;
        float shotOffset = 360 / numberOfShots;
        for (int i = 0; i < numberOfShots; i++)
        {
            GameObject currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            float bulletSpeedXAux = bulletSpeedX;
            if (clockWiseSpin)
                bulletSpeedXAux = -bulletSpeedXAux;
            //Reversing either the X or Y velocity components reverses the direction of spin. In this case, x has been chosen.
            currentBullet.GetComponent<BulletMovementVectorScript>().setBulletSpeedX(bulletSpeedXAux * Mathf.Cos((angle + shotOffset*i)* Mathf.PI / 180));
            currentBullet.GetComponent<BulletMovementVectorScript>().setBulletSpeedY(bulletSpeedY * Mathf.Sin((angle + shotOffset*i)* Mathf.PI / 180));
        }
    }
    void Update()
    {
        shotIntervalAux = shotIntervalAux - Time.deltaTime;
        if(Resources != null)
            isFiring = Resources.getFiringStatus();
        if ((shotIntervalAux < 0)&&(isFiring))
            Shoot();
        if (angle > 360)
            angle = angle - 360;
    }
}

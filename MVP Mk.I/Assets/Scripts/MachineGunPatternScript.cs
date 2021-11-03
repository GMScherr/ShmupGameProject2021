using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Pattern script for a single bullet stream. By tinkering with its parameters you may change pattern behaviour as follows :
//-Shot Interval describes, in seconds, the amount of time waited between each shot.
//-Is Firing enables and disables the gun from firing. Use it to make enemies fire in bursts instead of constant fire.
//-The X and Y speed parameters define the velocity of the bullet in the X and Y axis. Careful with how you set your velocities, things can get pretty weird if you don't know what you're doing.
//-Bullet Scatter defines whether or not the stream will have pinpoint accuracy at the point of aim. If activated, each bullet will fire at a slightly different location. This location is defined by a point randomly
//chosen within a circle of radius circularErrorProbable around the target being aimed at. If set to false, all shots will land true at the aimed position.
//-Track Player defines whether the stream will aim at the player or not. If set to true, a GameObject must be provided in order for the tracking to work. If set to false, the point of aim will default to Aim Vector.
//-Aim Vector is the default aiming vector. It is usually set to (0,0), meaning the shots will fly with only the Y component (Vertically down). Aim Vector can be manually adjusted in order to set the stream's aim.
//-Aim At Vector toggles what behaviour the Aim Vector will have. If it is on, the stream will fire at that vector relative to the world ((0,0) will fire at the world origin). If it is toggled off, it will multiply
//your bullet's velocity by the provided vector ((0,0) will halt all bullet movement). Toggling Track Player will toggle Aim At Vector on during Start time. These parameters are not to be changed during runtime.

//Tips on using this script :
//This script should be your game's most basic script, don't be shy about spamming the living hell out of it. In conjunction with moving enemies, this script can be used to make very simple but effective shot
//patterns. When using scatter, make sure not to use too much scatter or else the bullets will seem to fly randomly instead of in a proper stream, which can be very frustrating in a game that requires precision.
//Also mind you : since scatter does its job by choosing a random position within a circle around the player, the script may fire on the opposite direction should the circle overlap with the object containing this
//script. If you do not want that to happen it is recommended you implement a method to set scatter to false should the player get closer than the Circular Error Probable radius. I chose not to implement it into this
//script, instead opting to leave this up to the controller of the enemies containing the script. I also opted to not do that by default because the somewhat random scatter when the player is above the enemy gives
//the effect of an enemy firing upwards, which is acceptable and even ideal. That and staying too close to the object containing the script only gives a false sense of security : it is just as dangerous to stay
//right over the gun as it is to keep your distance, if not more. Players who try to "exploit" this effect will be at a disadvantage.

public class MachineGunPatternScript : MonoBehaviour
{
    [SerializeField] private float shotInterval = 0.0005f;
    [SerializeField] private bool isFiring = true;
    private float shotIntervalAux;
    [SerializeField] private GameObject bullet;
    [SerializeField] private EnemyHealthController Resources;
    [SerializeField] private float bulletSpeedX = 60;
    [SerializeField] private float bulletSpeedY = 60;
    [SerializeField] private bool bulletScatter = true;
    [SerializeField] private float circularErrorProbable = 5;
    [SerializeField] private bool trackPlayer = true;
    [SerializeField] private Vector2 aimVector;
    [SerializeField] private bool aimAtVector = false;
    [SerializeField] private GameObject trackedPlayer;
    private Vector2 pointOfAim;

    //Getters and setters for the above fields :
    public float getShotInterval()
    {
        return shotInterval;
    }
    public float getShotIntervalAux()
    {
        return shotIntervalAux;
    }
    public bool getfiringStatus()
    {
        return isFiring;
    }
    public void setFiringStatusTrue()
    {
        isFiring = true;
    }
    public void setFiringSatusFalse()
    {
        isFiring = false;
    }

    public void setScatterTrue()
    {
        bulletScatter = true;
    }

    public void setScatterFalse()
    {
        bulletScatter = false;
    }

    public void setScatterValue (bool value)
    {
        bulletScatter = value;
    }
    void Start()
    {
        shotIntervalAux = shotInterval;
        if (trackPlayer)
            aimAtVector = true;
    }

    void Shoot()
    {
        shotIntervalAux = shotInterval;
        if ((bulletScatter)&&(trackPlayer))
        {
            float r = Random.Range(0, circularErrorProbable);
            r = (2 / r * r) * r;
            //The above operation makes the scatter more random. Don't ask me how it works, the internet told me it does and it does.
            float theta = Random.Range(0, 360);
            pointOfAim.x = trackedPlayer.transform.position.x + r * Mathf.Cos(theta);
            pointOfAim.y = trackedPlayer.transform.position.y + r * Mathf.Sin(theta);
        }
        if ((!bulletScatter)&&(trackPlayer))
        {
            pointOfAim.x = trackedPlayer.transform.position.x;
            pointOfAim.y = trackedPlayer.transform.position.y;
        }
        if ((bulletScatter)&&(!trackPlayer))
        {
            float r = Random.Range(0, circularErrorProbable);
            r = (2 / r * r) * r;
            float theta = Random.Range(0, 360);
            pointOfAim.x = aimVector.x + r * Mathf.Cos(theta);
            pointOfAim.y = aimVector.y + r * Mathf.Sin(theta);
        }
        if ((!bulletScatter) && (!trackPlayer))
        {
            pointOfAim = aimVector;
        }
        Vector2 firingVector;
        firingVector.x = pointOfAim.x - transform.position.x;
        firingVector.y = pointOfAim.y - transform.position.y;
        if(!aimAtVector)
            firingVector = pointOfAim;
        firingVector.Normalize();
        GameObject currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        currentBullet.GetComponent<BulletMovementVectorScript>().setBulletSpeedX(bulletSpeedX * firingVector.x);
        currentBullet.GetComponent<BulletMovementVectorScript>().setBulletSpeedY(bulletSpeedY * firingVector.y);
    }
    void Update()
    {

     shotIntervalAux = shotIntervalAux - Time.deltaTime;
     if (Resources != null)
         isFiring = Resources.getFiringStatus();
     if ((shotIntervalAux < 0)&&(isFiring))
        Shoot();
     if (shotIntervalAux < 0)
            shotIntervalAux = shotInterval;
    }
}

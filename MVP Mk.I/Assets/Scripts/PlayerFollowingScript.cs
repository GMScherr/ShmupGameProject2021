using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that makes its attached object track the player. This script must be attached a GameObject which contains a Rigidbody2D because it works through velocity instead of translation. Its fields are :
//-Tracked Player : reference to the player to be followed. Should work for any GameObject if you want to get creative with it.
//-Speed X and Speed Y : the speed which will be imparted on the object once it starts tracking.
//-Tracking on X and Tracking on Y : switches that enable or disable the following behaviour in the given axis.
//-Minimum Distance Before Track : this determines the tolerance limit of the tracked object before this object starts moving. By default it is 0.5, therefore the tracked object has 1 game unit to move around before
//it is tracked. This avoids the tracking object from becoming "jittery" when it arrives on its position.

public class PlayerFollowingScript : MonoBehaviour
{
    [SerializeField] private GameObject trackedPlayer;
    [SerializeField] private float objectSpeedX = 0;
    [SerializeField] private float objectSpeedY = 0;
    [SerializeField] private bool trackingOnX = true;
    [SerializeField] private bool trackingOnY = false;
    [SerializeField] private float minimumDistanceBeforeTrack = 0.5f;
    private Rigidbody2D RB;
    private Vector2 velocityVector;
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        velocityVector.x = 0;
        velocityVector.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackingOnX)
        {
            if ((transform.position.x < trackedPlayer.transform.position.x + minimumDistanceBeforeTrack) && (transform.position.x > trackedPlayer.transform.position.x - minimumDistanceBeforeTrack))
                velocityVector.x = 0;
            else
            {
                if (trackedPlayer.transform.position.x > transform.position.x)
                    velocityVector.x = objectSpeedX;
                if (trackedPlayer.transform.position.x < transform.position.x)
                    velocityVector.x = -objectSpeedX;
            }
        }
        if (trackingOnY)
        {
            if ((transform.position.y < trackedPlayer.transform.position.y + minimumDistanceBeforeTrack) && (transform.position.y > trackedPlayer.transform.position.y - minimumDistanceBeforeTrack))
                velocityVector.y = 0;
            else
            {
                if (trackedPlayer.transform.position.y > transform.position.y)
                    velocityVector.y = objectSpeedY;
                if (trackedPlayer.transform.position.y < transform.position.y)
                    velocityVector.y = -objectSpeedY;
            }
        }

        RB.velocity = velocityVector;
    }
}

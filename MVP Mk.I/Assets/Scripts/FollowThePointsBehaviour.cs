using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code that describes the movement of the object in given points, like in a "connect the dots" game. It possesses three arrays which must be of the same size for this script to properly work. They are:
//Vector List : an array containing the points you want your object to move torwards.
//Timer List : an array containing the amount of different times you want the object to wait before it starts moving torwards the point.
//Speed List : an array containing the different speeds which you want your object to have when moving torwards a point.
//All arrays are indexed using the same pointer arrayIndex. An example of how to use this script is as follows :
//-The first array contains the values (5,2) and (3,4). The second array contains the values 25 and 30. The third array contains the values 10 and 20.
//-After 25 seconds have passed, the object will start moving torwards point (5,2) with the velocity 10. After that, it will wait 30 seconds. One the 30 seconds have expired, it will move to (3,4) with velocity 20.
//With this script one can describe literally any movement manually : circular movement, accelerating movement, zigzag movement, sine wave movement ... just depends on one's patience to fill it all by hand.
//Might implement getters and setters so that other functions could instantiate this and fill it in so that more complicated movements don't have to be filled in manually. Time and necessity will tell.
public class FollowThePointsBehaviour : MonoBehaviour
{
    [SerializeField] private Vector2[] vectorList;
    [SerializeField] private float[] timerList;
    [SerializeField] private float[] speedList;
    private Rigidbody2D RB;
    private int arrayIndex;

    private void followNextPoint()
    {
        Vector2 newVelocityVector = new Vector2 (vectorList[arrayIndex].x - transform.position.x,vectorList[arrayIndex].y - transform.position.y);
        newVelocityVector.Normalize();
        RB.velocity = new Vector2(newVelocityVector.x * speedList[arrayIndex],newVelocityVector.y * speedList[arrayIndex]);
        arrayIndex++;
    }

    void Start()
    {
        arrayIndex = 0;
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (arrayIndex < vectorList.Length)
        {
            timerList[arrayIndex] = timerList[arrayIndex] - Time.deltaTime;
            if (timerList[arrayIndex] < 0)
                followNextPoint();
        }
    }
}

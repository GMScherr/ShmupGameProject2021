using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Behaviour that used to be part of Bullet Movement Vector Script. It has been turned into its own script so it can be used on other things, such as enemies. This script simply rotates an object so the position
//which originally designated its top will point towards its own velocity vector. Thus, for your sprite to work with this script, make sure sure that the front of the object points to the top by default.
public class AlignToVectorScript : MonoBehaviour
{
    private Rigidbody2D RB;
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, RB.velocity)));
    }
}

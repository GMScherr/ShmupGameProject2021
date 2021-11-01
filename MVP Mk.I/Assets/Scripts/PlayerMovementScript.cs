using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovementScript : MonoBehaviour
{
    private bool upKey;
    private bool rightKey;
    private bool downKey;
    private bool leftKey;
    private bool leftShiftKey;
    [SerializeField] private float speed_modifier = 1;
    [SerializeField] private float speed_modifier_focus = 0.5f;
    private Rigidbody2D RB;
    private Vector2 velocityVector;


    // Start is called before the first frame update
    void Start()
    {
        upKey = false;
        rightKey = false;
        downKey = false;
        leftKey = false;
        leftShiftKey = false;
        RB = GetComponent<Rigidbody2D>();
        velocityVector = new Vector2 (0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        upKey = Input.GetKey(KeyCode.UpArrow);
        rightKey = Input.GetKey(KeyCode.RightArrow);
        downKey = Input.GetKey(KeyCode.DownArrow);
        leftKey = Input.GetKey(KeyCode.LeftArrow);
        leftShiftKey = Input.GetKey(KeyCode.LeftShift);

        float movement_speed;
        if (leftShiftKey)
            movement_speed = speed_modifier_focus;
        else
            movement_speed = speed_modifier;

        if (upKey)
            velocityVector.y = movement_speed;
        else
            if (downKey)
                velocityVector.y = -movement_speed;
            else
                velocityVector.y = 0;

        if (rightKey)
            velocityVector.x = movement_speed;
        else
            if (leftKey)
            velocityVector.x = -movement_speed;
            else
                velocityVector.x = 0;

        RB.velocity = velocityVector;
    }
}

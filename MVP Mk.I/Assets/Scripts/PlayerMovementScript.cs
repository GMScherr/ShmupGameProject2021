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
    public float speed_modifier = 1;
    public float speed_modifier_focus = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        upKey = false;
        rightKey = false;
        downKey = false;
        leftKey = false;
        leftShiftKey = false;
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
            movement_speed = speed_modifier_focus * Time.deltaTime;
        else
            movement_speed = speed_modifier * Time.deltaTime;
        if (upKey)
            transform.Translate(0f, movement_speed, 0f);
        if (rightKey)
            transform.Translate(movement_speed, 0f, 0f);
        if (downKey)
            transform.Translate(0f, -movement_speed, 0f);
        if (leftKey)
            transform.Translate(-movement_speed, 0f, 0f);
    }
}

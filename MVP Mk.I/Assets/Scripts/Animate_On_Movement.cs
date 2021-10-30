using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate_On_Movement : MonoBehaviour
{
    private bool rightKey;
    private bool leftKey;
    private bool leftShiftKey;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rightKey = false;
        leftKey = false;
        leftShiftKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        rightKey = Input.GetKey(KeyCode.RightArrow);
        leftKey = Input.GetKey(KeyCode.LeftArrow);
        leftShiftKey = Input.GetKey(KeyCode.LeftShift);

        if (leftShiftKey)
        {
            rightKey = false;
            leftKey = false;
        }
        animator.SetBool("Turning_Right", rightKey);
        animator.SetBool("Turning_Left", leftKey);
            
    }
}

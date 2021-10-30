using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWingTransparent : MonoBehaviour
{
    private bool rightKey;
    private bool leftKey;
    private bool leftShiftKey;
    public float waitingTime;
    SpriteRenderer SRender;

    // Start is called before the first frame update
    void Start()
    {
        rightKey = false;
        leftKey = false;
        leftShiftKey = false;
        waitingTime = 0.0f;
        SRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rightKey = Input.GetKey(KeyCode.RightArrow);
        leftKey = Input.GetKey(KeyCode.LeftArrow);
        leftShiftKey = Input.GetKey(KeyCode.LeftShift);

        if (rightKey || leftKey)
            waitingTime = waitingTime + Time.deltaTime;
        if (waitingTime > 0.05f)
            waitingTime = 0.05f;

        if (!rightKey && !leftKey)
            waitingTime = waitingTime - Time.deltaTime;
        if (waitingTime < 0)
            waitingTime = 0;

        if (waitingTime > 0.04f)
            SRender.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        else
            SRender.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        
        if(leftShiftKey)
            SRender.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWeaponActiveTimer : MonoBehaviour
{
    [SerializeField] private EnemyHealthController controller;
    [SerializeField] private float timeUntilActivate;
    private bool hasActivated = false;
    [SerializeField] private float timeActive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilActivate = timeUntilActivate - Time.deltaTime;
        if ((timeUntilActivate < 0) && (!hasActivated))
        {
            hasActivated = true;
            controller.setFiringTrue();
        }
        if (hasActivated)
        {
            timeActive = timeActive - Time.deltaTime;
            if (timeActive < 0)
                controller.setFiringFalse();
        }
    }
}

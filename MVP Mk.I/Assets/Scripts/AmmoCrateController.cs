using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrateController : MonoBehaviour
{
    [SerializeField] private int ammoCount;
    [SerializeField] private float crateLifeTime = 5;

    public void setAmmoCount(int amountToSet)
    {
        ammoCount = amountToSet;
    }
    public int getAmmoCount()
    {
        return ammoCount;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        crateLifeTime = crateLifeTime - Time.deltaTime;
        if (crateLifeTime < 0)
            Destroy(this.gameObject);
    }
}

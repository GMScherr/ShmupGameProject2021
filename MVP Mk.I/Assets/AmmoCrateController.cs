using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrateController : MonoBehaviour
{
    [SerializeField] private int ammoCount;

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
        
    }
}

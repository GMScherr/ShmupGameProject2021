using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunControllerScript : MonoBehaviour
{
    [SerializeField] private MachineGunPatternScript MG_Stats;
    [SerializeField] private PlayerResourceController Resources;
    private bool fireKey;
    private bool reloadKey;
    private float shotInterval;
    private float shotIntervalAux;
    [SerializeField] private float reloadCooldown;
    [SerializeField] private float reloadCooldownTime = 2;
    private bool canReload = true;
    void Start()
    {
        fireKey = false;
        reloadKey = false;
        shotInterval = MG_Stats.getShotInterval();
        shotIntervalAux = shotInterval;
        reloadCooldown = reloadCooldownTime;
    }

    void Update()
    {
        fireKey = Input.GetKey(KeyCode.Z);
        if ((Resources.getMG_Ammo() > 0) && (fireKey) && (reloadCooldown == reloadCooldownTime))
        {
            MG_Stats.setFiringStatusTrue();
        }
        if ((!fireKey)||(Resources.getMG_Ammo() < 1)||(reloadCooldown != reloadCooldownTime))
            MG_Stats.setFiringSatusFalse();
        if (MG_Stats.getfiringStatus())
        {
            shotIntervalAux = shotIntervalAux - Time.deltaTime;
            if (shotIntervalAux < 0)
            {
                shotIntervalAux = shotInterval;
                Resources.decreaseMG_Ammo();
            }
        }

        reloadKey = Input.GetKey(KeyCode.Space);
        if (reloadCooldown == reloadCooldownTime)
            canReload = true;
        if (reloadKey)
            if (Resources.getNumberOfMagazines() > 0)
                if (canReload)
                {
                    Resources.reloadGun();
                    canReload = false;
                    reloadCooldown = 0;
                }
        reloadCooldown = reloadCooldown + Time.deltaTime;
        if (reloadCooldown > reloadCooldownTime)
            reloadCooldown = reloadCooldownTime;
    }
}

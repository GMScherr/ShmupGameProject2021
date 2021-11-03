using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsControllerScript : MonoBehaviour
{
    [SerializeField] private MachineGunPatternScript MG_Stats;
    [SerializeField] private MachineGunPatternScript Pistol_Stats;
    [SerializeField] private PlayerResourceController Resources;
    [SerializeField] private GameObject PlayerBomb;
    [SerializeField] private float bombCooldown;
    [SerializeField] private float bombCooldownTime = 2;
    private bool fireKey;
    private bool reloadKey;
    private bool bombKey;
    private bool focusKey;
    private float shotInterval;
    private float shotIntervalAux;
    [SerializeField] private float reloadCooldown;
    [SerializeField] private float reloadCooldownTime = 2;
    private bool canReload = true;
    void Start()
    {
        fireKey = false;
        reloadKey = false;
        bombKey = false;
        focusKey = false;
        shotInterval = MG_Stats.getShotInterval();
        shotIntervalAux = shotInterval;
        reloadCooldown = reloadCooldownTime;
        bombCooldown = bombCooldownTime;
    }

    void Update()
    {
        fireKey = Input.GetKey(KeyCode.Z);
        focusKey = Input.GetKey(KeyCode.LeftShift);
        //MG Firing logic
        MG_Stats.setScatterValue(!focusKey);
            
            if ((Resources.getMG_Ammo() > 0) && (fireKey) && (reloadCooldown == reloadCooldownTime))
            {
                MG_Stats.setFiringStatusTrue();
            }
            if ((!fireKey) || (Resources.getMG_Ammo() < 1) || (reloadCooldown != reloadCooldownTime))
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
        //Pistol firing logic
        if((Resources.getMG_Ammo() <= 0)&&(reloadCooldown == reloadCooldownTime))
        {
            if (fireKey)
                Pistol_Stats.setFiringStatusTrue();   
        }
        else
            Pistol_Stats.setFiringSatusFalse();
        if (!fireKey)
            Pistol_Stats.setFiringSatusFalse();

        //Reload logic
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

        //Bomb logic
        bombKey = Input.GetKey(KeyCode.X);
        if (bombKey)
        {
            if ((Resources.getPlayerBombs() > 0) && (bombCooldown == bombCooldownTime))
            {
                GameObject Bomb = Instantiate(PlayerBomb, transform.position, Quaternion.identity);
                Resources.decreasePlayerBombs();
                bombCooldown = 0;
            }
        }
        if (bombCooldown < bombCooldownTime)
            bombCooldown = bombCooldown + Time.deltaTime;
        if (bombCooldown > bombCooldownTime)
            bombCooldown = bombCooldownTime;
    }
}

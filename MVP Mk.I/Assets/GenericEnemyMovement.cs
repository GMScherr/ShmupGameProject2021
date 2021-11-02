using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that determines the movement of an enemy which moves in straight lines in the battlefield. It describes this movement through three phases. These are :
//-Idle Phase : the phase before the enemy is deployed.
//-Entrance Phase : the phase in which the enemy is still entering the field.
//-Field Phase : the phase in which the enemy is engaged in battle. If an Enemy Health Controller has been provided and IsFiring is defaulted to false, this phase will set it to true.
//-Exit Phase : the phase in which the enemy is retreating. If an Enemy Health Controller has been provided, this phase will set IsFiring to false.

//Each phase can be described by the amount of time the enemy stays on that phase and its speed during it. The following list describes each field and what they do :
//-Start Position : the position the enemy will start in. The enemy can be placed anywhere in the scene, such as a dedicated enemy pool for loaded enemies, they will be sent to this position.
//-Time Until Entrance : during this time the enemy will remain idle in the position he was first placed in. Upon expiration of this timer, they'll be sent to their start position, ending the Idle Phase.
//-Speed on Entrance : the enemy's speed during the Entrance Phase.
//-Time on Entrance : the time during which the Entrance Phase takes place. Upon expiration of this timer the Entrance Phase ends and the Field Phase begins.
//-Speed on Field : the enemy's speed during the Field Phase.
//-Time on Field : the time during which the Field Phase takes place. Upon expiration of this timer the Exit Phase begins.
//-Speed After Exit : the enemy's speed during the Exit Phase.
//-Enemy Lifetime : the time during which the Exit Phase takes place. Upon expiration of this timer the enemy leaves the scene completely.

public class GenericEnemyMovement : MonoBehaviour
{
    //Entrance stats
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private float timeUntilEntrance;
    [SerializeField] private Vector2 speedOnEntrance;
    [SerializeField] private float timeOnEntrace;
    private bool entranceTimer = false;
    //Field stats
    [SerializeField] private Vector2 speedOnField;
    [SerializeField] private float timeOnField;
    private bool fieldTimer = false;
    //Exit stats
    [SerializeField] private Vector2 speedAfterExit;
    [SerializeField] private float enemyLifeTime;
    private bool exitTimer = false;

    private Rigidbody2D RB;
    [SerializeField] private EnemyHealthController Resources;
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    private void enterEnemy()
    {
        RB.transform.position = new Vector2(startPosition.x,startPosition.y);
        RB.velocity = new Vector2(speedOnEntrance.x, speedOnEntrance.y);
        entranceTimer = true;
    }

    private void enemyOnField()
    {
        RB.velocity = new Vector2(speedOnField.x, speedOnField.y);
        if (Resources != null)
            Resources.setFiringTrue();
        fieldTimer = true;
    }

    private void exitEnemy()
    {
        RB.velocity = new Vector2(speedAfterExit.x, speedAfterExit.y);
        exitTimer = true;
        if (Resources != null)
            Resources.setFiringFalse();
    }

    void Update()
    {
        timeUntilEntrance = timeUntilEntrance - Time.deltaTime;
        if ((timeUntilEntrance < 0)&&(!entranceTimer))
            enterEnemy();
        if ((entranceTimer)&&(!fieldTimer)&&(!exitTimer))
            timeOnEntrace = timeOnEntrace - Time.deltaTime;
        if ((timeOnEntrace < 0)&&(!fieldTimer))
            enemyOnField();
        if ((fieldTimer)&&(!exitTimer))
            timeOnField = timeOnField - Time.deltaTime;
        if (timeOnField < 0)
            exitEnemy();
        if (exitTimer)
            enemyLifeTime = enemyLifeTime - Time.deltaTime;
        if (enemyLifeTime < 0)
            Destroy(this.gameObject);
        
        
    }
}

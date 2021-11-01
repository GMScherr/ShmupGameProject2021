using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCollision : MonoBehaviour
{
    private int counter;
    private PlayerResourceController Resources;
    private Rigidbody2D RB;

    private void Start()
    {
        counter = 0;
        Resources = GetComponentInChildren<PlayerResourceController>();
        RB = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            counter++;
            Resources.decreasePlayerLives();
            Debug.Log("Player has collided with a bullet!" + counter);
        }
        if (other.tag == "Ammo Crate")
        {
            Resources.addReserveAmmo(other.gameObject.GetComponent<AmmoCrateController>().getAmmoCount());
            Destroy(other.gameObject);
        }
        if (other.tag == "Grenade Crate"){
            for (int i = 0;i < other.gameObject.GetComponent<AmmoCrateController>().getAmmoCount();i++)
                Resources.incrementPlayerBombs();
            Destroy(other.gameObject);
        }
        if (other.tag == "Player Lateral Limit")
        {
            RB.transform.position = new Vector2 (other.GetComponent<Rigidbody2D>().transform.position.x + 5,RB.transform.position.y);
            Debug.Log("Player has hit a lateral Limit!");
        }
        //Animacao de morte
        //Resetar vida do jogador
        //Se vida do jogado < 0 -> Game over
        //Ajustar score
        //Respawn jogador
    }
}

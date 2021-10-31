using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCollision : MonoBehaviour
{
    private int counter;
    private PlayerResourceController Resources;

    private void Start()
    {
        counter = 0;
        Resources = GetComponentInChildren<PlayerResourceController>();
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
        //Animacao de morte
        //Resetar vida do jogador
        //Se vida do jogado < 0 -> Game over
        //Ajustar score
        //Respawn jogador
    }
}

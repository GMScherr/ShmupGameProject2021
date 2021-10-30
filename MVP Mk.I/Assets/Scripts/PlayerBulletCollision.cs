using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCollision : MonoBehaviour
{
    private int counter;

    private void Start()
    {
        counter = 0;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            counter++;
            Debug.Log("Player has collided with a bullet!" + counter);
        }
        //Animacao de morte
        //Resetar vida do jogador
        //Se vida do jogado < 0 -> Game over
        //Ajustar score
        //Respawn jogador
    }
}

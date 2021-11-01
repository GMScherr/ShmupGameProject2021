using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControllerScript : MonoBehaviour
{
    [SerializeField] private float bombLifeTime = 2;
    [SerializeField] private float bombRadius = 0.2f;
    [SerializeField] private float bombFinalRadius = 1.5f;
    [SerializeField] private float bombRadiusGrowthSpeed = 0.1f;
    private CircleCollider2D Col;
    void Start()
    {
        Col = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
            Destroy(other.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        bombLifeTime = bombLifeTime - Time.deltaTime;
        if (bombRadius < bombFinalRadius)
            bombRadius = bombRadius + bombRadiusGrowthSpeed;
        Col.radius = bombRadius;
        if (bombLifeTime < 0)
            Destroy(this.gameObject);
    }
}

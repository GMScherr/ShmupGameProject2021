using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovementScript : MonoBehaviour
{
    public float bulletSpeed = 0.5f;
    public float lifeTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float bulletTrueSpeed = bulletSpeed * Time.deltaTime;
        transform.Translate(0.0f,-bulletTrueSpeed,0.0f);
        lifeTime = lifeTime - Time.deltaTime;
        if (lifeTime < 0)
            Destroy(this.gameObject);
    }
}

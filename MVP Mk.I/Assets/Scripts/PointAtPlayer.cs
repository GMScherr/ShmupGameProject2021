using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtPlayer : MonoBehaviour
{
    [SerializeField] private GameObject trackedPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 angleToPlayer = transform.position - trackedPlayer.transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, angleToPlayer)));
    }
}

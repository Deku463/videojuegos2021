using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFire : MonoBehaviour
{
    public float speed = 5f;
    public LayerMask collisions;
    public Transform[] waypoints;

    private void Start()
    {
        
    }

    private void Update()
    {
        //Mov. hacia el frente
        Vector3 targetMovement = Vector3.right * speed * Time.deltaTime;
        transform.Translate(targetMovement, Space.World);



    }
}

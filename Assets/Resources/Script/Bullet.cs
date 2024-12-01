using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f); 

    }
    private void FixedUpdate()
    {
        rb.AddForce(Vector3.right * 20f);
    }



   
}

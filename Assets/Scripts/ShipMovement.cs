using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotSpeed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddRelativeForce(0, 0, Input.GetAxis("Vertical") * speed);
        rb.AddTorque(0, Input.GetAxis("Horizontal") * rotSpeed, 0);
    }
}

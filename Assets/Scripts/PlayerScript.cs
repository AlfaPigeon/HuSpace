using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    [Header("Movement")]
    private Vector3 velocity ;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector3(speed * Input.GetAxisRaw("Horizontal"), velocity.y, speed * Input.GetAxisRaw("Vertical"));
        rb.velocity = velocity;
    }
}
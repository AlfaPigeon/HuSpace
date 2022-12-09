using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    [Header("Movement")]
    private Vector3 velocity;
    public float speed = 5f;
    public Transform movementReference;
    public GameObject bullet;
    public Transform barrel;
    public Transform gunPivot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = (Input.GetAxisRaw("Horizontal") * movementReference.transform.right + Input.GetAxisRaw("Vertical") * movementReference.forward).normalized * speed;
        rb.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -9.812155f;
        Vector3 dir = mousePos - Camera.main.WorldToScreenPoint(gunPivot.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
       // Debug.Log(dir);
        gunPivot.localRotation = Quaternion.Euler(gunPivot.localRotation.eulerAngles.x, gunPivot.localRotation.eulerAngles.y, angle);
    }

    public void Fire()
    {
        Instantiate(bullet, barrel.position, barrel.rotation);
    }
}
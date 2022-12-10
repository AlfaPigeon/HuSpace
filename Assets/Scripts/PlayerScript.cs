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
    public Transform fakeGunPivot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        velocity = (Input.GetAxisRaw("Horizontal") * movementReference.transform.right + Input.GetAxisRaw("Vertical") * movementReference.forward).normalized * speed;
        rb.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(gunPivot.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        gunPivot.localRotation = Quaternion.Euler(gunPivot.localRotation.eulerAngles.x, gunPivot.localRotation.eulerAngles.y, angle);
        fakeGunPivot.transform.localRotation = Quaternion.Euler(90, 0, angle - 30);
    }

    public void Fire()
    {
        //90 - 30 Fake Gun Pivot
        GameObject lastBullet = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
        lastBullet.transform.GetChild(1).transform.localRotation = Quaternion.Euler(60f, 30f ,gunPivot.transform.rotation.eulerAngles.z);
        lastBullet.GetComponent<Rigidbody>().velocity = fakeGunPivot.right * 12.5f;
    }
}
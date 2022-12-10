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
        UpdatePlayerMovement();
        UpdateGunRotation();

        //Check for fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void UpdatePlayerMovement()
    {
        velocity = (Input.GetAxisRaw("Horizontal") * movementReference.transform.right + Input.GetAxisRaw("Vertical") * movementReference.forward).normalized * speed;
        rb.velocity = velocity;
    }

    private void UpdateGunRotation()
    {
        //Get direction between mouse and gun
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(gunPivot.position);

        //Get angle between mouse and gun from direction
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //Rotate gun by angle
        gunPivot.localRotation = Quaternion.Euler(gunPivot.localRotation.eulerAngles.x, gunPivot.localRotation.eulerAngles.y, angle);

        //Using fake pivot because of bullet movement problem
        fakeGunPivot.transform.localRotation = Quaternion.Euler(90, 0, angle - 30);
    }

    public void Fire()
    {
        //Spawn a bullet
        GameObject lastBullet = Instantiate(bullet, barrel.transform.position, Quaternion.identity);

        //Set bullet rotation to gun rotation
        lastBullet.transform.GetChild(1).transform.localRotation = Quaternion.Euler(60f, 30f ,gunPivot.transform.rotation.eulerAngles.z);

        //Move along fake pivot axis
        lastBullet.GetComponent<Rigidbody>().velocity = fakeGunPivot.right * 12.5f;

        //Effects
        ScreenShaker.Instance.ShakeCamera(0.1f, 0.7f, 4f);
    }
}
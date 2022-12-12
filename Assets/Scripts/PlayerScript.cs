using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    private Vector3 velocity;
    public bool Grounded = false;
    public Transform Ground_ref;
    public float speed = 5f;
    public Transform movementReference;
    private Vector3 movementReference_right;
    private Vector3 movementReference_forward;
    public GameObject bullet;
    public Transform barrel;
    public Transform gunPivot;
    public Transform fakeGunPivot;

    public AudioSource playerAudio;
    public AudioClip shootingClip;

    public HealthBar healthBar;
    public float maxHealth;
    public float currentHealth;

    private Animator animator;
    private CharacterController characterController;

    public CinemachineBrain cinemachineBrain;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        
        UpdateMovementReference();
    }

    public void UpdateMovementReference()
    {
        movementReference_right = movementReference.right;
        movementReference_forward = movementReference.forward;
    }
    void Update()
    {
        UpdatePlayerMovement();
        UpdatePlayerDodge();
        UpdateGunRotation();
        UpdateGunFire();
    }

    
    private void FixedUpdate()
    {

        if(WorldGravity.instance != null) Grounded = Physics.Raycast(transform.position, -WorldGravity.instance.Active_vector_up, 14f + 0.1f);


        
    }
    private void UpdateGunFire()
    {
        //Check for fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
    private void UpdatePlayerDodge()
    {/*
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Shift is down");
            Vector3 move = (Input.GetAxis("Horizontal") * movementReference_right + Input.GetAxis("Vertical") * movementReference_forward).normalized;
            characterController.enabled = false;
            rb.isKinematic = false;
            if (move == Vector3.zero)
            {
                move = transform.forward;            
            }

            rb.velocity = move*5f;
           
            rb.isKinematic = true;
            characterController.enabled = true;
        }*/
    
    }
        private void UpdatePlayerMovement()
    {

        //velocity = (Input.GetAxisRaw("Horizontal") * movementReference.transform.right + Input.GetAxisRaw("Vertical") * movementReference.forward).normalized * speed;

        Vector3 move = (Input.GetAxis("Horizontal") * WorldGravity.instance.Active_vector_right+ Input.GetAxis("Vertical") * WorldGravity.instance.Active_vector_forward).normalized;

        /*
        Vector3 move =( Input.GetAxis("Horizontal") * movementReference_right + Input.GetAxis("Vertical") * movementReference_forward).normalized;

       
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        characterController.Move(move * Time.deltaTime * speed);
        */
        
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move.normalized;
            rb.velocity = transform.forward * speed;
            if(!Grounded )rb.velocity += Physics.gravity;
        }
   

        animator.SetFloat("Velocity", rb.velocity.magnitude);

       
        }

    public Vector3 target;
    private void UpdateGunRotation()
    {

        //Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);


        Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(transform.position, hit.point);
            target =   new Vector3(hit.point.x, hit.point.y, hit.point.z);
            gunPivot.LookAt(target);
        }

        //gunPivot.LookAt(new Vector3(target.x, barrel.position.y, target.z));
        /*
        //Position

        Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;


        Debug.DrawLine(transform.position,dir);

        gunPivot.transform.localPosition = dir;
        fakeGunPivot.transform.localPosition = dir;

        //Rotation

        //Get direction between mouse and gun
        dir = Input.mousePosition - Camera.main.WorldToScreenPoint(gunPivot.position);

        //Get angle between mouse and gun from direction
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //Rotate gun by angle
        gunPivot.localRotation = Quaternion.Euler(gunPivot.localRotation.eulerAngles.x, gunPivot.localRotation.eulerAngles.y, angle);

        //Using fake pivot because of bullet movement problem
        fakeGunPivot.transform.localRotation = Quaternion.Euler(90, 0, angle - 30);

        */
    }

    public void Fire()
    {
        //Spawn a bullet
        GameObject lastBullet = Instantiate(bullet, barrel.transform.position, Quaternion.identity);

        //Set bullet rotation to gun rotation
        lastBullet.transform.GetChild(1).transform.localRotation = Quaternion.Euler(60f, 30f ,gunPivot.transform.rotation.eulerAngles.z);

        //Move along fake pivot axis


        lastBullet.GetComponent<Rigidbody>().velocity = barrel.forward * 12.5f;

        //lastBullet.GetComponent<Rigidbody>().velocity = fakeGunPivot.right * 12.5f;

        //Effects
        ScreenShaker.Instance.ShakeCamera(0.1f, 0.7f, 4f);
        playerAudio.PlayOneShot(shootingClip);
    }

    public void OnDamaged(float amount)
    {
        if (currentHealth - amount <= 0)
        {
            currentHealth = 0;
            OnKilled();
            return;
        }

        else currentHealth -= amount;

        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void OnHealed(float amount)
    {
        if (currentHealth + amount > maxHealth) currentHealth = maxHealth;
        else currentHealth += amount;

        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void OnKilled()
    {
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
}
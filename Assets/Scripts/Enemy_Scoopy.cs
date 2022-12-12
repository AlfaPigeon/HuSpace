using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy_Scoopy : Enemy
{
    [Header("Scoopy")]
    public Rigidbody scoopyRigidbody;
    public float scoopySpeed;
    private Transform playerTransform;

    public bool aggressive;

    bool damagedToPlayer;
    public bool kiteGoBacking;
    public GameObject bullet;
    public override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }



    private IEnumerator Attack()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            //Spawn a bullet
            GameObject lastBullet = Instantiate(bullet,transform.position, transform.rotation);

            //Set bullet rotation to gun rotation
            lastBullet.transform.GetChild(1).transform.forward = hit.transform.position - transform.position;

            //Move along fake pivot axis


            lastBullet.GetComponent<Rigidbody>().velocity = (hit.transform.position - transform.position) * 22.5f;

            lastBullet.tag = "Enemy";
            lastBullet.GetComponent<Light>().color = Color.red;

        }


        yield return new WaitForSeconds(0.5f);
    }



    private void Update()
    {
        if (playerTransform != null && aggressive && !kiteGoBacking) scoopyRigidbody.velocity = (playerTransform.position - transform.position).normalized * scoopySpeed;
    }

    public IEnumerator KiteGoBack(float secs)
    {
        float kiteSeconds = 0;
        kiteGoBacking = true;

        while(kiteSeconds < secs)
        {
            kiteSeconds += Time.deltaTime;
            scoopyRigidbody.velocity = -(playerTransform.position - transform.position).normalized * scoopySpeed;
            yield return null;
        }

        kiteGoBacking = false;
    }



    private void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("Collided to " + trigger.gameObject.name);

        if (trigger.CompareTag("Player") )
        {
          
            if (!kiteGoBacking)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().OnDamaged(damage);
                StartCoroutine(KiteGoBack(.5f));
            }
        }
    }
}

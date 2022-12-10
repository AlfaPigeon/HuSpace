using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Scoopy : Enemy
{
    public Rigidbody scoopyRigidbody;
    public float scoopySpeed;
    private Transform playerTransform;

    public bool aggressive;

    bool damagedToPlayer;
    bool kiteGoBacking;

    public override void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (aggressive && !kiteGoBacking) scoopyRigidbody.velocity = (playerTransform.position - transform.position).normalized * scoopySpeed;
    }

    IEnumerator KiteGoBack(float secs)
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
}

using System.Collections;
using System.Collections.Generic;
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

    public override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
}

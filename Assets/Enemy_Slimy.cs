using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Slimy : Enemy
{
    public bool Agrasive = false;

    public float Attack_range = 10f;

    public override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        if (Agrasive && Vector3.Distance(player.transform.position, transform.position) < Attack_range)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, player.transform.position, out hit,Attack_range))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }

    public void Attack()
    {

    }
}

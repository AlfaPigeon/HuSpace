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

            if (Physics.Raycast(transform.position, player.transform.position-transform.position, out hit, Attack_range))
            {
                Debug.DrawRay(transform.position, (player.transform.position - transform.position) * hit.distance, Color.green);
                Debug.Log("Did Hit");
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.tag == "Player") Debug.Log("Player is hit");
            }
            else
            {
                Debug.DrawRay(transform.position, (player.transform.position - transform.position) * hit.distance, Color.red);
                Debug.Log("Did not Hit");
            }
        }
    }

    public void Attack()
    {

    }
}

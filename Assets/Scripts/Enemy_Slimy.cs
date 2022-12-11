using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class Enemy_Slimy : Enemy
{
    public bool Agrasive = true;

    public float Attack_range = 10f;

    public float Attack_Cooldown = 4f;

    public GameObject Slime_Attack;
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

                if (hit.collider.tag == "Player")
                {
                    Agrasive = false;
                    Attack(hit.collider.transform);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, (player.transform.position - transform.position) * hit.distance, Color.red);
                Debug.Log("Did not Hit");
            }
        }
    }


    public void WaitForAttack()
    {
        StartCoroutine(WaitForAttack(Attack_Cooldown));
    }
    private IEnumerator WaitForAttack(float cd)
    {



            yield return new WaitForSeconds(cd);
        

        Agrasive = true;
    }
    public void Attack(Transform enemy)
    {
        GameObject gameObject =  Instantiate(Slime_Attack, transform.position,transform.rotation);
        gameObject.GetComponent<SlimeAttack>().enemy = this;
        gameObject.transform.LookAt(enemy);
        gameObject.transform.Rotate(0, 90, 0);
    }
}

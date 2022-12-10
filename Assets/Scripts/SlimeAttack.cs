using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeAttack : MonoBehaviour
{
    public float maxvalue = 1f;

    public Transform sprite;

    public Enemy_Slimy enemy;
 

    public float value;

    public float _rotate = 0f;
    private void Start()
    {
       StartCoroutine(Attack());
    }

    private void Update()
    {

       
    }

    private IEnumerator Attack()
    {
        while (maxvalue> value)
        {
            value += 0.2f;
            sprite.localScale = new Vector3(value, 0.05f, 1f);

            yield return new WaitForSeconds(0.1f);
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemy.GetComponent<Rigidbody>().velocity = 10*(other.transform.position - enemy.transform.position).normalized;
            Destroy(gameObject);
        }

    }

  
}

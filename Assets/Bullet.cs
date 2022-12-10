using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.transform.parent.GetComponent<Enemy>().Damage(20f);
            Destroy(transform.parent.gameObject);
        }
    }
}

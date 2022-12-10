using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(transform.parent.gameObject, 4f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.transform.parent.GetComponent<Enemy>().OnDamaged(20f);
            Destroy(transform.parent.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.CompareTag("Player"))
        {
            Debug.Log("col");
        }
    }
}

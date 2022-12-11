using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderController : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("Collided to " + trigger.gameObject.name);

        if (trigger.CompareTag("Player") && enemy.enemyType == Enemy.EnemyType.Scoopy)
        {
            Enemy_Scoopy scoopy = enemy as Enemy_Scoopy;
            if (!scoopy.kiteGoBacking)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().OnDamaged(enemy.damage);
                scoopy.StartCoroutine(scoopy.KiteGoBack(.5f));
            }
        }
    }
}
